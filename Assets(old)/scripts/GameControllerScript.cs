using UnityEngine;
using TMPro;  // 如果使用 TextMeshPro 的话
using UnityEngine.SceneManagement;  // 引入场景管理
using System.Collections;  // 引入协程所需的命名空间

public class GameControllerScript : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(-34f, 25f, 51f);  // 目标位置
    public Transform player;         // 玩家对象
    public TMP_Text win;        // 显示胜利文本的UI组件

    public float winDistance = 10f;    // 到达目标位置的距离阈值

    void Start()
    {
        // 在游戏开始时确保胜利文本不可见
        win.enabled = false;

        // 隐藏鼠标
        Cursor.visible = false;

        // 锁定鼠标到屏幕中心
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 检查玩家与目标位置之间的距离
        float distanceToTarget = Vector3.Distance(player.position, targetPosition);

        // 如果距离小于设定的阈值，显示胜利消息并结束游戏
        if (distanceToTarget <= winDistance)
        {
            ShowWinMessage();
            // 启动协程
            if (!isGameOver) // 防止重复启动协程
            {
                StartCoroutine(EndGameWithDelay());
                isGameOver = true;
            }
        }
    }

    // 显示胜利文本
    void ShowWinMessage()
    {
        win.enabled = true;  // 显示文本
    }

    private bool isGameOver = false;

    // 协程：等待 2 秒后结束游戏
    IEnumerator EndGameWithDelay()
    {
        // 等待 2 秒
        yield return new WaitForSeconds(2f);

        // 退出游戏
        Debug.Log("Game Over! You Win!");
        Application.Quit();  // 退出游戏

        // 在编辑器中退出
        UnityEditor.EditorApplication.isPlaying = false;  // 仅在编辑器中有效
    }
}
