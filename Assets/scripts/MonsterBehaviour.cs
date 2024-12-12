using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    public Transform player;  // 玩家的位置
    public float detectionRange = 10f;  // 怪物的追击范围
    public float attackRange = 1f;  // 怪物的攻击范围
    public float moveSpeed = 3.5f;  // 怪物移动速度

    private NavMeshAgent navAgent;  // 怪物的 NavMeshAgent 组件
    private Animator animator;  // 怪物的动画控制器

    void Start()
    {
        // 获取NavMeshAgent组件
        navAgent = GetComponent<NavMeshAgent>();

        // 设置 NavMeshAgent 的移动速度
        if (navAgent != null)
        {
            navAgent.speed = moveSpeed;
        }
    }

    void Update()
    {
        if (player == null) return;  // 如果没有指定玩家对象，跳过更新

        // 计算玩家与怪物之间的距离
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 如果玩家在追击范围内，怪物追击玩家
        if (distanceToPlayer <= detectionRange)
        {
            // 设置 NavMeshAgent 的目标为玩家的位置
            navAgent.SetDestination(player.position);

            // 如果怪物距离玩家很近，进行攻击动作
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            
        }
    }

    // 攻击玩家的逻辑
    private void AttackPlayer()
    {

        // 可以在这里加入攻击逻辑，比如减少玩家的生命值等

        // 如果攻击完成后，继续追击玩家
        navAgent.isStopped = false;
    }
}
