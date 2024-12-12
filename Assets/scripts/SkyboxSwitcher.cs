using UnityEngine;
using System.Collections;  // 引入协程

public class SkyboxSwitcher : MonoBehaviour
{
    public Material daySkybox;   // 白天天空盒材质
    public Material duskSkybox; // 黄昏天空盒材质
    public float delayTime = 10f; // 延迟时间（10秒）

    private void Start()
    {
        // 启动协程，在 10 秒后切换天空盒
        StartCoroutine(SwitchSkyboxAfterDelay());
    }

    private IEnumerator SwitchSkyboxAfterDelay()
    {
        // 等待 10 秒
        yield return new WaitForSeconds(delayTime);

        // 切换天空盒（这里假设先是白天天空盒）
        if (RenderSettings.skybox == daySkybox)
        {
            RenderSettings.skybox = duskSkybox; // 切换到夜晚天空盒
        }
        else
        {
            RenderSettings.skybox = daySkybox; // 切换回白天天空盒
        }

        // 更新天空盒后，刷新场景中的光照
        DynamicGI.UpdateEnvironment();
    }
}
