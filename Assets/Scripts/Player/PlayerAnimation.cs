using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    private Animator am;
    private Rigidbody2D rg;
    private PhysicCheck _physicCheck;
    private PlayeController _playeController;
    private CapsuleCollider2D coll;
    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        am = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
        _physicCheck = GetComponent<PhysicCheck>();
        _playeController = GetComponent<PlayeController>();
    }

    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        am.SetFloat("verityX", Mathf.Abs(rg.velocity.x));
        //按下ctrl转换奔跑和走
        am.SetBool("changeWalk",!_playeController.isRun);
        //设置跳跃动画
        am.SetFloat("verityY",rg.velocity.y);
        am.SetBool("isGrand",_physicCheck.isGrand);
        //设置死亡动画
        am.SetBool("isDie",_playeController.isDie);
        //设置攻击动画
        am.SetBool("isAttack",_playeController.isAttack);
        //设置爬墙动画
        if(coll.sharedMaterial==_playeController.normal&&_playeController.currentPower>0.1f)
            am.SetBool("isQiang",_physicCheck.touchLeftWall||_physicCheck.touchRightWall);
        else
        {
            am.SetBool("isQiang", false);
        }
        
    }
   
    public void PlayHurt()
    {
      am.SetTrigger("isHurt");
    }

    public void PlayerAttack()
    {
        am.SetTrigger("Attack");
    }

    public void Huachan()
    {
        //设置滑铲动画
        am.SetTrigger("isShift");
    }
}
