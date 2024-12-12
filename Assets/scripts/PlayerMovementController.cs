using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // 定义角色控制器（用于处理角色的物理碰撞和移动）
    CharacterController playerController;

    // 用于存储角色的移动方向
    Vector3 direction;

    // 角色的基本移动速度
    public float speed = 6;

    // 跳跃的力量（影响跳跃的高度）
    public float jumpPower = 0.0005f;

    // 重力的大小，用于控制跳跃后的下落速度
    public float gravity = 10f;

    // 鼠标的移动速度，用于控制视角旋转的速度
    public float mousespeed = 5f;

    // 鼠标旋转的上下限制角度
    public float minmouseY = -45f;
    public float maxmouseY = 45f;

    // 记录当前视角的X和Y旋转角度
    float RotationY = 0f;
    float RotationX = 0f;

    // 相机的Transform，用于控制相机的旋转
    public Transform agretctCamera;

    // 在游戏开始时获取CharacterController组件
    void Start()
    {
        // 获取胶囊体的 CharacterController 组件
        playerController = this.GetComponent<CharacterController>();
    }

    // 每一帧更新时调用，用于处理玩家的移动和视角控制
    void Update()
    {
        // 获取玩家输入的水平和垂直轴（WASD或箭头键）
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        // 初始化方向为零向量
        direction = Vector3.zero;

        // 只有在玩家按下键时才更新移动方向
        if (_horizontal != 0 || _vertical != 0)
        {
            direction = new Vector3(_horizontal, 0, _vertical);
        }

        // 根据玩家是否按下Shift或Ctrl调整速度
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = 3;  // 按下Shift时减慢速度
        }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            speed = 12;  // 按下Ctrl时加速
        }
        else
        {
            speed = 6;  // 默认速度
        }

        // 如果角色在地面上
        if (playerController.isGrounded)
        {
            // 如果按下空格键，则角色跳跃
            if (Input.GetKeyDown(KeyCode.Space))
                direction.y = jumpPower; // 设置跳跃力度
        }

        // 每帧更新重力，使角色逐渐下落
        direction.y -= gravity * Time.deltaTime;

        // 将计算出来的移动方向转换到世界坐标系下并执行移动
        playerController.Move(playerController.transform.TransformDirection(direction * Time.deltaTime * speed));

        // 获取并更新鼠标输入，控制玩家左右旋转（旋转角度累加）
        RotationX += Input.GetAxis("Mouse X") * mousespeed;

        // 获取鼠标上下移动输入，控制相机的上下旋转（限制在设定的范围内）
        RotationY -= Input.GetAxis("Mouse Y") * mousespeed;
        RotationY = Mathf.Clamp(RotationY, minmouseY, maxmouseY); // 限制相机的上下旋转角度在设定范围内

        // 更新玩家的水平旋转（绕Y轴旋转）
        this.transform.eulerAngles = new Vector3(0, RotationX, 0);

        // 更新相机的旋转，控制相机的上下视角旋转
        agretctCamera.transform.eulerAngles = new Vector3(RotationY, RotationX, 0);

    }
}
