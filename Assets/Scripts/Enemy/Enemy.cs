using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
protected Rigidbody2D rb;
[HideInInspector]public Animator anim;
[HideInInspector]public Vector3 faceDir;
[HideInInspector] public PhysicCheck _physicCheck;
[HideInInspector] public Transform attacker;
 private PlayeController _playeController;
 [Header("基本参数")] 
  public float walkSpeed;
  public float runSpeed;
 [HideInInspector] public float currentSpeed;
  public float hurtForce;
  [Header("计时器")] 
  public float waitTime;
  public float waitTimeCounter;
  public bool wait;
  [Header("状态")]
  public bool isHurt;
  public bool isDead;
  public bool isFly;

  protected BaceState currentState;
  protected BaceState XunLuoState;
  protected BaceState chaseState;
  
 protected virtual void Awake()
  {
      _playeController = GetComponent<PlayeController>();
      _physicCheck = GetComponent<PhysicCheck>();
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      currentSpeed = walkSpeed;
  }

  private void OnEnable()
  {
      currentState = XunLuoState;
      currentState.OnEnter(this);
  }


  private void Update()
  {
      faceDir = new Vector3(transform.localScale.x, 0, 0);
     
      TimeCounter();
      currentState.LogicUpdate();
  }

  private void FixedUpdate()
  {
      if((!isHurt&&!isDead)&&!wait)
          Move();
      currentState.PhysicsUpdate();
  }

  private void OnDisable()
  {
      currentState.OnExit();
  }

  public virtual void Move()
  {
      rb.velocity = new Vector2(currentSpeed * -faceDir.x * Time.deltaTime, rb.velocity.y);
  }
  public void TimeCounter()
  {
      if (wait)
          {
              waitTimeCounter -= Time.deltaTime;
              if (waitTimeCounter <0)
              {
                  wait = false;
                  waitTimeCounter = waitTime;
                  transform.localScale = new Vector3(-faceDir.x, 1, 1);
              }
          }
  }

  public void OnTakeDamge(Transform attackerTrans)
  {
      attacker = attackerTrans;
      if (attackerTrans.position.x - transform.position.x > 0)
      {
          transform.localScale = new Vector3(-1, 1, 1);
      }

      if (attackerTrans.position.x - transform.position.x < 0)
      {
          transform.localScale = new Vector3(1, 1, 1);
      }
      //受伤击退
      isHurt = true;
      anim.SetTrigger("Hurt");
      Vector2 dir = new Vector2(transform.position.x - attackerTrans.position.x, 0).normalized;

      StartCoroutine(OnHurt(dir) );

  }

  
  public IEnumerator OnHurt(Vector2 dir)
  {
      rb.AddForce(dir*hurtForce,ForceMode2D.Impulse);
      yield return new WaitForSeconds(0.5f);
      isHurt = false;
  }
  //死亡
  public void OnDie()
  {
      gameObject.layer = 2;
      anim.SetBool("Dead",true);
      isDead = true;
  }

  public void DeadDestory()
  {
      Destroy(this.gameObject);
  }
  
}
