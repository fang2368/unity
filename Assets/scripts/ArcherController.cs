using UnityEngine;
using System.Collections;  // 引入协程所需的命名空间

public class ArcherController : MonoBehaviour
{
    private Animator animator;      //动画组件
    public Transform player;        // 玩家对象
    public Transform firePoint;     //箭支射出点
    private bool Loaded = false;    // 是否正在装填
    private AudioSource audioSource;    //音频
    public AudioClip fillSound;
    public AudioClip shootSound;

    float arrowDistance;    //射程
    float minArrowDistance = 10f;  //最短射程
    float maxArrowDistance = 100f;  //最长射程

    float pullDuration = 0f;    //拉弓时间
    public float maxPullDuration = 2f; // 最长拉弓时间

    public float arrowSpeed;  // 箭速
    public float minArrowSpeed = 5f;  // 最小箭速
    public float maxArrowSpeed = 30f;  // 最大箭速

    private float rightDownTime;    //右键按下时间
    private float rightUpTime;      //右键松开时间
    private bool isRightMouseButtonPressed = false;    //右键是否正在按着

    public ArrowCountController arrowCountController;   //引用对象，修改箭的数量
    public IfShootArea ifShootArea;   //引用对象，获得射击条件

    //射杀三个骷髅作为可以在射击位外自由射击的条件

    void Start()
    {
        // 获取 Animator 组件
        animator = GetComponent<Animator>();

        //获取挂载音频
        audioSource = GetComponent<AudioSource>();  // 获取挂载的 AudioSource
    }

    void Update()
    {
        if (ifShootArea.ifShoot())
            shoot();
    }

    //射击动作
    void shoot()
    {
        if (arrowCountController.arrowcount > 0)
        {
            // 右键点击，进行装填
            if (Input.GetMouseButtonDown(1) && !Loaded)
            {
                //重置力度
                pullDuration = 0f;

                //改变动画状态变量
                animator.speed = 1;
                animator.SetBool("isLoading", true);
                animator.SetBool("isShooting", false);
                animator.SetFloat("PullStrength", 0);

                //播放音频
                AudioSource.PlayClipAtPoint(fillSound, Vector3.zero);

                //记录右键按下
                isRightMouseButtonPressed = true;

                //箭数-1
                arrowCountController.changeArrow(-1);
            }

            //右键按压，计算拉弓力度
            if (isRightMouseButtonPressed)
            {
                // 更新拉弓时间
                if (pullDuration < maxPullDuration)
                    pullDuration += Time.deltaTime;

                //线性映射拉弓时间至射速
                arrowSpeed = Mathf.Lerp(minArrowSpeed, maxArrowSpeed, pullDuration / maxPullDuration);

                //线性映射拉弓时间至射程
                arrowDistance = Mathf.Lerp(minArrowDistance, maxArrowDistance, pullDuration / maxPullDuration);

                //映射拉弓时间到动画
                float pullStrength = Mathf.Clamp01(pullDuration / maxPullDuration);
                animator.SetFloat("PullStrength", pullStrength);

                // 输出射速和射程用于调试
                //Debug.Log("Arrow Speed: " + arrowSpeed);
                //Debug.Log("Arrow Distance: " + arrowDistance);
            }

            //右键松开，装填完毕
            if (Input.GetMouseButtonUp(1))
            {
                //记录右键松开
                isRightMouseButtonPressed = false;

                //装填完毕
                Loaded = true;

                //改变动画状态变量
                animator.SetBool("isLoading", false);
            }
        }


        // 左键点击，进行射击
        if (Input.GetMouseButtonDown(0) && Loaded)
        {
            //空膛状态
            Loaded = false;

            //播放音频
            AudioSource.PlayClipAtPoint(shootSound, Vector3.zero);

            // 播放射击动画
            animator.speed = 2;
            animator.SetBool("isLoading", false);
            animator.SetBool("isShooting", true);

            //创建箭头实例
            GameObject arrowPrefab = Resources.Load<GameObject>("RyuGiKen/Crossbow/Prefabs/Arrow");
            if (arrowPrefab != null)
            {
                // 实例化箭头预设（从Resources文件夹中加载的箭头预设）
                GameObject arrow = Instantiate(arrowPrefab);

                // 为新创建的箭头对象动态添加ArrowController组件
                arrow.AddComponent<ArrowHitMonster>();

                // 获取箭头的ArrowController组件，以便后续操作
                ArrowHitMonster arrowHitMonster = arrow.GetComponent<ArrowHitMonster>();

                // 将箭头的位置设置为firePoint的当前位置
                // firePoint应该是发射箭头的起始点，例如弓箭的弦的位置
                arrow.transform.position = firePoint.position;

                // 设置箭头的旋转，使其朝向当前物体（弓）的正前方
                // Quaternion.LookRotation(this.transform.forward) 会将箭头朝向当前物体（弓）的前方方向
                arrow.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);

                // 使用协程让箭矢匀速移动
                StartCoroutine(MoveArrow(arrow));
            }
            else
            {
                Debug.LogError("Arrow prefab could not be loaded from Resources. Please check the file path.");
            }
        }

    }

    // 使用协程实现匀速移动
    private IEnumerator MoveArrow(GameObject arrow)
    {
        float distanceTraveled = 0f;
        Vector3 startPosition = arrow.transform.position;
        Vector3 direction = arrow.transform.forward;

        while (distanceTraveled < arrowDistance)  // 假设箭的最大飞行距离是 100 单位
        {
            float moveDistance = arrowSpeed * Time.deltaTime;
            arrow.transform.Translate(direction * moveDistance, Space.World);  // 让箭矢匀速前进

            distanceTraveled += moveDistance;
            yield return null;
        }

        // 飞行结束后，可以销毁箭矢
        Destroy(arrow);
    }
}