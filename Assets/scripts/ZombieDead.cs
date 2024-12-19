using UnityEngine;
using System.Collections;

public class ZombieDead : MonoBehaviour
{
    public ScoreManager scoreManager; // 引用 ScoreManager 脚本，用于增加分数
    public ParticleSystem smokeEffect;  // 引入烟雾粒子系统

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

        //播放烟雾粒子效果
        smokeEffect.transform.position = transform.position;  // 将烟雾位置设置为怪物位置
        smokeEffect.Play();  // 播放烟雾粒子效果

        //僵尸+2分
        int scoreToAdd = 2; 

        // 增加分数
        scoreManager.AddScore(scoreToAdd);

        // 销毁怪物
        Destroy(gameObject, 0.2f);
    }
    
}
