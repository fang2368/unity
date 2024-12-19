using UnityEngine;
using UnityEngine.UI;  // 如果使用 Text 组件的话
using TMPro;  // 如果使用 TextMeshPro 的话

//分数显示
public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;  // 如果使用 TextMeshPro，用这个来显示UI
    private int score = 0;      // 分数变量

    void Start()
    {
        ResetScore();
    }

    void UpdateScoreDisplay()
    {
        // 更新 UI 中的文本显示分数
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreDisplay();
    }
}
