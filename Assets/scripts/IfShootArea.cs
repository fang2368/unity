using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfShootArea : MonoBehaviour
{
    public Transform player;        // 玩家对象
    public bool InShootArea = false; // 是否在射击位
    public Vector3 targetPosition = new Vector3(63f, 30f, -45f);  // 射击位圆心
    public float shootingAreaRadius = 2f;  // 射击位范围半径
    public bool FinishedShootArea = false; // 是否在射击位击杀三个骷髅
    public int SkeletonDeadCount = 3;          //骷髅数量   

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //判断是否在射击位内
    public void CheckIfInArea()
    {
        float distanceToTarget = Vector3.Distance(player.position, targetPosition);
        //Debug.Log("距离是？" + distanceToTarget);

        if (distanceToTarget <= shootingAreaRadius)
        {
            InShootArea = true;
        }
        else
            InShootArea = false;
    }

    //检查是否射杀三个骷髅
    void CheckIfShootSkeleton()
    {
        if (SkeletonDeadCount == 0)
            FinishedShootArea = true;
    }

    //骷髅被击杀
    public void changeDeadCount(int count)
    {
        SkeletonDeadCount -= count;
    }

    //判断能否射击
    public bool ifShoot()
    {
        CheckIfInArea();
        CheckIfShootSkeleton();

        //Debug.Log("可以射击吗？" + (InShootArea || FinishedShootArea));
        return (InShootArea || FinishedShootArea);
    }
}
