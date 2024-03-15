using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class PlayeController : MonoBehaviour
{
    public God inputControl;
    public Vector2 inputDirection;
    private Rigidbody2D rg;
    private PlayerAnimation _playerAnimation;
    public bool isAttack;
    //体力条设置值
    public UnityEvent<PlayeController> OnPowerChange;
    public float MaxPower;
    public float currentPower;
    //走路跑步切换
    public bool isRun=false;
    //初始化x的翻转，保证输入系统x为0时，不会影响全局
    private int faceDir = 1;
    private PhysicCheck physicCheck;
    //实现二段跳
    private bool againJump = false;
    private bool banSpace;
    //实现滑铲
    public bool isShift;
    public float originalSpeed;
    [Header("基本参数")] 
    public float jumpForse=16f;
    public float speed=290f;
    [Header("受伤反馈参数")]
    public bool isgetHurt;
    public float hurtForce;
    [Header("物理材质")]
    private CapsuleCollider2D coll;
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D noPower;
    //死亡参数
    public bool isDie;
    private void Awake()
    {   float temp = speed;
        _playerAnimation = GetComponent<PlayerAnimation>();
        physicCheck = GetComponent<PhysicCheck>();
        inputControl = new God();
        rg = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        //输入系统控制
        inputControl.GamePlayer.Jump.started += Jump;
        inputControl.GamePlayer.Attack.started += PlayerAttack;
        inputControl.GamePlayer.HuaChan.started += HuaChan;
        inputControl.GamePlayer.Walk.started += WalkChange;
    }

  

    private void Start()
    {
        originalSpeed = speed;
        currentPower = MaxPower;
        OnPowerChange?.Invoke(this);
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        PaQiang();
        //读取设备输入的关于xy的值
        //inputsysteam中的移动组读取二维向量数值
        inputDirection = inputControl.GamePlayer.Move.ReadValue<Vector2>();
        if (!banSpace) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            againJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isgetHurt&&!isAttack)
        {
            Move();
        }
        //体力条
        if( (physicCheck.touchLeftWall || physicCheck.touchRightWall)&&currentPower>0.1f)
        {
            currentPower -= 2.5f * Time.deltaTime;
            OnPowerChange?.Invoke(this);
        }
        if (!(Input.GetKeyDown(KeyCode.LeftShift)) 
            && (currentPower<MaxPower) &&(!physicCheck.touchLeftWall) &&(!physicCheck.touchRightWall))
        {
            currentPower += 2f*Time.deltaTime;
            OnPowerChange?.Invoke(this);
        }

        
        
    }
//爬墙体力检测
    private void PaQiang()
    {
        coll.sharedMaterial = (currentPower > 0) ? normal : noPower;
    }
    
    //控制移动翻转和跳跃
    private void Move( )
    {
        rg.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rg.velocity.y);
        
        if (inputDirection.x>0)
        {
            faceDir = 1;
        }
        if(inputDirection.x<0)
        {
            faceDir = -1;
        }       
        transform.localScale = new Vector3(faceDir,1,1);
    }

    //输入系统类函数
    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicCheck.isGrand)
        {   banSpace = true;
            rg.AddForce(transform.up * jumpForse, ForceMode2D.Impulse);
            
        }
        else  if (againJump||physicCheck.touchLeftWall||physicCheck.touchRightWall)
        {   
            rg.AddForce(transform.up * (jumpForse/(2f)), ForceMode2D.Impulse);
            banSpace = false;
            againJump = false;
        }
    }
//攻击
    private void PlayerAttack(InputAction.CallbackContext obj)
    {
    _playerAnimation.PlayerAttack();
    isAttack = true;
    }
//滑铲
    private void HuaChan(InputAction.CallbackContext obj)
    {
        //设置滑铲动画
        if (currentPower > 10)
        {
            _playerAnimation.Huachan();
            currentPower -= 10f;
            OnPowerChange?.Invoke(this);
        }
    }

//跑步和走路切换
    private void WalkChange(InputAction.CallbackContext obj)
    {
        isRun = !isRun;
    }
    //事件类函数
    public void GetHurt(Transform attacker)
    {
        isgetHurt = true;
       rg.velocity=Vector2.zero;
       Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
       rg.AddForce(hurtForce*dir,ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDie = true;
        inputControl.GamePlayer.Disable();
    }
    
}
