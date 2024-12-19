using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDamageController : MonoBehaviour
{
    public int maxHealth = 5;    // 玩家最大生命值
    public int currentHealth = 5; // 当前生命值
    public GameObject[] hearts;   // 存储心形图标的数组
    public TextMeshProUGUI gameOverText;  // 死亡文本

    void Start()
    {
        // 初始化心形图标的显示
        UpdateHearts();

        // 游戏开始时隐藏死亡信息
        gameOverText.gameObject.SetActive(false); 
    }

    void Update()
    {
        // 检查玩家是否死亡
        if (currentHealth <= 0 && !gameOverText.gameObject.activeSelf)
        {
            GameOver(); // 触发游戏结束
        }
    }

    // 处理玩家受伤
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;  // 生命值不能小于0
        }
        UpdateHearts();  // 更新显示的心形图标
    }

    // 更新心形图标的显示
    void UpdateHearts()
    {
        for (int i = 1; i <= maxHealth; i++)
        {
            // 显示当前生命值颗心
            if (i <= currentHealth)
            {
                hearts[i-1].SetActive(true); // 显示心形图标
            }
            else
            {
                hearts[i-1].SetActive(false); // 隐藏心形图标
            }
        }
    }

    // 游戏结束时调用此方法
    void GameOver()
    {
        gameOverText.text = "Wasted";  // 设置显示文本为 "Wasted"
        gameOverText.gameObject.SetActive(true); // 显示文本

        // 启动协程，在2秒后结束游戏
        StartCoroutine(EndGameWithDelay(2f));
    }

    // 协程：延迟2秒后结束游戏
    IEnumerator EndGameWithDelay(float delay)
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
