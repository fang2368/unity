using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    public Transform player;  // 玩家的位置 
    public float detectionRange = 15f;  // 怪物的追击范围
    public float attackRange = 2f;  // 怪物的攻击范围
    public float moveSpeed = 3.5f;  // 怪物移动速度
    public float damage = 1;  // 怪物攻击伤害
    public float knockbackForce = 12f;  // 击退力
    public float attackInterval = 1f;  // 攻击间隔时间

    public PlayerMovementController playerMovementController;   // 玩家移动组件，用于击退玩家
    public PlayerDamageController playerDamageController;   // 玩家受到伤害组件
    private NavMeshAgent navAgent;  // 怪物的 NavMeshAgent 组件
    private Animator animator;  // 怪物的动画控制器

    private float lastAttackTime = 0f;  // 上次攻击的时间

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

            // 如果怪物距离玩家很近，并且达到了攻击间隔（1s），进行攻击
            if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackInterval)
            {
                //Debug.Log("怪物和玩家距离：" + distanceToPlayer);
                //Debug.Log("Attacked!");

                // 更新上次攻击时间
                lastAttackTime = Time.time;

                // 进行攻击
                AttackPlayer();
            }
        }
    }

    // 攻击玩家的逻辑
    private void AttackPlayer()
    {
        //Debug.Log("Attacked+!");

        // 计算击退方向（怪物到玩家的方向）
        Vector3 knockbackDirection = player.position - transform.position;

        // 调用玩家移动控制器来处理击退
        playerMovementController.PlayerDamaged(knockbackDirection, knockbackForce);
    }
}
