using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkyboxSwitcher : MonoBehaviour
{
    public Material daySkybox;   // 白天天空盒材质
    public Material duskSkybox;  // 黄昏天空盒材质
    public Material nightSkybox; // 夜晚天空盒材质

    public TMP_InputField commandInput; // 输入框，用于输入命令

    public bool entering = false;     // 标志位，输入时禁用wasd操控玩家运动
    public bool enterPressed = false; // 标志位，判断按下 Enter 键时是显示输入框还是执行命令，隐藏输入框

    private void Start()
    {
        // 初始化时禁用命令输入框
        commandInput.gameObject.SetActive(false);

        // 启动时不需要激活输入框
        commandInput.onEndEdit.AddListener(OnCommandEntered);
    }

    private void Update()
    {
        // 如果按下 Enter 键
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Press Enter"); 
            if (!enterPressed)
            {
                // 第一次按 Enter 时，显示输入框并激活
                commandInput.gameObject.SetActive(true);
                commandInput.ActivateInputField(); // 激活输入框，准备输入命令
                enterPressed = true;  // 标记为第一次按下 Enter
                entering = true;      //开启输入模式
            }
            else
            {
                // 第二次按 Enter 时，执行命令并切换天空盒
                ExecuteCommand(commandInput.text);
                ClearInputField();  // 清空输入框并隐藏
                enterPressed = false; // 重置标志位，等待下一次输入
                entering = false;     //退出输入模式，玩家可以活动
            }
        }
    }

    // 处理输入的命令
    private void ExecuteCommand(string input)
    {
        // 去除前后空格
        input = input.Trim();

        // 根据命令切换天空盒
        if (input == "/time set day")
        {
            SwitchSkybox(daySkybox); // 切换到白天天空盒
        }
        else if (input == "/time set dusk")
        {
            SwitchSkybox(duskSkybox); // 切换到黄昏天空盒
        }
        else if (input == "/time set night")
        {
            SwitchSkybox(nightSkybox); // 切换到夜晚天空盒
        }
        else
        {
            Debug.Log("未知命令: " + input); // 如果命令未知，输出调试信息
        }
    }

    // 切换天空盒的通用方法
    private void SwitchSkybox(Material skybox)
    {
        RenderSettings.skybox = skybox;  // 切换天空盒
        DynamicGI.UpdateEnvironment();  // 更新环境光
    }

    // 清空输入框并隐藏
    private void ClearInputField()
    {
        commandInput.text = "";  // 清空输入框
        commandInput.gameObject.SetActive(false); // 隐藏输入框
    }

    // 监听命令输入结束
    private void OnCommandEntered(string input)
    {
        // 由于按 `Enter` 键会调用这个方法，在 `Update` 中已经处理了执行命令逻辑，这里不再需要额外处理
    }
}
