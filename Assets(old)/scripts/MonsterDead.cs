using UnityEngine;
using System.Collections;

public class MonsterDead : MonoBehaviour
{
    public ScoreManager scoreManager; // 引用 ScoreManager 脚本，用于增加分数

    private bool isHit = false; // 判断怪物是否被击中

    private AudioSource audioSource;    //音频
    public AudioClip deadSound;

    public void OnHit()
    {
        if (isHit) return; // 如果怪物已经被击中，则不再执行任何操作
        isHit = true;

        //获取挂载音频
        audioSource = GetComponent<AudioSource>();  // 获取挂载的 AudioSource
        AudioSource.PlayClipAtPoint(deadSound, Vector3.zero);  //播放音频

        // 判断父物体名称并增加分数
        int scoreToAdd = 1; // 默认加1分

        // 判断父物体的名称来决定加多少分
        if (transform.parent != null)
        {
            // 根据父物体的名称来判断怪物类型
            string parentName = transform.parent.name;

            if (parentName == "Zombies")  // 如果父物体的名称是 "Zombies"
            {
                scoreToAdd = 2; // 如果是 Zombie，加2分
            }
            else if (parentName == "Skeletons")  // 如果父物体的名称是 "Skeletons"
            {
                scoreToAdd = 1; // 如果是 Skeleton，加1分
            }
        }

        // 增加分数
        scoreManager.AddScore(scoreToAdd);

        // 销毁怪物
        Destroy(gameObject, 0.2f);
    }
    
}
