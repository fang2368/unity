using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // 如果使用 TextMeshPro 的话

public class ArrowCountController : MonoBehaviour
{
    public int arrowcount = 32;          //箭支数量
    public TMP_Text Arrowcount;     // 如果使用 TextMeshPro，用这个来显示UI

    void Start()
    {

    }

    void Update()
    {
        
    }

    //增减箭数
    public void changeArrow(int count)
    {
        arrowcount += count;

        // 更新箭的UI显示数量
        Arrowcount.text = "" + arrowcount.ToString();
    }
}
