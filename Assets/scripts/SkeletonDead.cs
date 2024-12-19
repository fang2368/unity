using UnityEngine;
using System.Collections;

public class SkeletonDead : MonoBehaviour
{
    public ScoreManager scoreManager; // 引用 ScoreManager 脚本，用于增加分数
    public ParticleSystem smokeEffect;  // 引入烟雾粒子系统

    //引用 ArrowController 脚本,用于修改箭数
    public ArrowCountController arrowCountController;

    //引用 IfShootArea 脚本,用于修改骷髅击杀数
    public IfShootArea ifShootArea;

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

        //小白+1分
        int scoreToAdd = 1;

        // 增加分数
        scoreManager.AddScore(scoreToAdd);

        //增加箭数
        arrowCountController.changeArrow(2);

        //小白击杀+1
        ifShootArea.changeDeadCount(1);

        // 销毁怪物
        Destroy(gameObject, 0.2f);
    }

}
