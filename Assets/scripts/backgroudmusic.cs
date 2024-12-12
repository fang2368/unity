using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroudmusic : MonoBehaviour
{
    private AudioSource audioSource;    //音频
    public AudioClip backgroundmusic;

    // Start is called before the first frame update
    void Start()
    {
        //获取挂载音频
        audioSource = GetComponent<AudioSource>();  // 获取挂载的 AudioSource
        AudioSource.PlayClipAtPoint(backgroundmusic, Vector3.zero);  //播放音频
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
