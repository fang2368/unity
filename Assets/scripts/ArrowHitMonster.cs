using UnityEngine;
using System.Collections;  // 引入协程所需的命名空间

public class ArrowHitMonster : MonoBehaviour
{
    public float speed = 30f;  // 箭矢速度
    public float maxDistance = 100f;  // 最大射程
    public Transform firePoint;     // 箭矢射出点
    public Vector3 boxSize = new Vector3(1f, 1f, 1f);  // 设置BoxCast的大小（这可以根据需要调整）

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 箭矢飞行方向
        Vector3 direction = transform.forward;

        // 使用 BoxCast 检测
        RaycastHit hit;
        
        // BoxCast的中心点是箭矢当前的位置，检测方向是箭矢的前方（方向），检测的最大距离是箭矢的速度 * deltaTime
        if (Physics.BoxCast(transform.position, boxSize, direction, out hit, Quaternion.identity, 30f * Time.deltaTime))
        {
            // 检测到碰撞，判断是否击中怪物
            if (hit.collider.transform.parent != null && hit.collider.transform.parent.CompareTag("monsters"))
            {
                Debug.Log("Hit Monster!");

                // 父物体是 Zombies，调用 ZombieDead 脚本
                if (hit.collider.transform.parent.name == "Zombies")
                {
                    //Debug.Log("射中僵尸了!");

                    ZombieDead zombieDead = hit.collider.transform.GetComponent<ZombieDead>();
                    if (zombieDead != null)
                    {
                        Debug.Log("射中僵尸了!");
                        zombieDead.OnHit();  // 调用怪物死亡的逻辑
                    }
                }

                // 父物体是 Skeletons，调用 SkeletonDead 脚本
                if (hit.collider.transform.parent.name == "Skeletons")
                {
                    //Debug.Log("射中小白了!");
                    SkeletonDead skeletonDead = hit.collider.transform.GetComponent<SkeletonDead>();
                    if (skeletonDead != null)
                    {
                        Debug.Log("射中小白了!");
                        skeletonDead.OnHit();  // 调用怪物死亡的逻辑
                    }
                }
            }
        }
    }

    // 可选：绘制调试用的 BoxCast 范围（便于调试和测试）
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward * (speed * Time.deltaTime), boxSize);
    }
}
