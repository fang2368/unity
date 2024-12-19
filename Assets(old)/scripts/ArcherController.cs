using UnityEngine;
using TMPro;  // 如果使用 TextMeshPro 的话
using System.Collections;  // 引入协程所需的命名空间

public class ArcherController : MonoBehaviour
{
    private Animator animator;
    public Transform firePoint;     //箭支射出点
    public int arrowcount;          //箭支数量
    public TMP_Text Arrowcount;     // 如果使用 TextMeshPro，用这个来显示UI
    private bool Loaded = false;    // 是否正在装填
    private AudioSource audioSource;    //音频
    public AudioClip shootSound;

    // 新增变量：箭的速度
    public float arrowSpeed = 10f;

    void Start()
    {
        // 获取 Animator 组件
        animator = GetComponent<Animator>();

        //设置箭初始数量
        arrowcount = 32;

        //获取挂载音频
        audioSource = GetComponent<AudioSource>();  // 获取挂载的 AudioSource
    }

    void Update()
    {
        if (arrowcount > 0)
        {
            // 检查右键点击，进行装填（从 empty 到 fill）
            if (Input.GetMouseButtonDown(1) && !Loaded)
            {
                StartLoading();
            }
        }


        // 检查左键点击，进行射击（从 hold 到 shoot），需要先确保箭矢已装填
        if (Input.GetMouseButtonDown(0) && Loaded)
        {
            AudioSource.PlayClipAtPoint(shootSound, Vector3.zero);  //播放音频

            StartShooting();
        }
    }

    // 开始装填：从 empty 到 fill到 hold
    void StartLoading()
    {
        Loaded = true;
        animator.SetBool("isLoading", true);
        animator.SetBool("isShooting", false); // 播放填充动画
        arrowcount--;
        UpdateArrowDisplay();
    }

    // 开始射击：从 hold 到 shoot到 empty
    void StartShooting()
    {
        Loaded = false;
        animator.SetBool("isLoading", false);
        animator.SetBool("isShooting", true); // 播放射击动画

        GameObject arrowPrefab = Resources.Load<GameObject>("RyuGiKen/Crossbow/Prefabs/Arrow");
        if (arrowPrefab != null)
        {
            // 实例化箭头预设（从Resources文件夹中加载的箭头预设）
            GameObject arrow = Instantiate(arrowPrefab);

            // 为新创建的箭头对象动态添加ArrowController组件
            arrow.AddComponent<ArrowController>();

            // 获取箭头的ArrowController组件，以便后续操作
            ArrowController arrowController = arrow.GetComponent<ArrowController>();

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

    // 使用协程实现匀速移动
    private IEnumerator MoveArrow(GameObject arrow)
    {
        float distanceTraveled = 0f;
        Vector3 startPosition = arrow.transform.position;
        Vector3 direction = arrow.transform.forward;

        while (distanceTraveled < 100f)  // 假设箭的最大飞行距离是 100 单位
        {
            float moveDistance = 30f * Time.deltaTime;
            arrow.transform.Translate(direction * moveDistance, Space.World);  // 让箭矢匀速前进

            distanceTraveled += moveDistance;
            yield return null;
        }

        // 飞行结束后，可以销毁箭矢
        Destroy(arrow);
    }

    void UpdateArrowDisplay()
    {
        // 更新 UI 中的文本显示分数
        Arrowcount.text = "" + arrowcount.ToString();
    }
}
