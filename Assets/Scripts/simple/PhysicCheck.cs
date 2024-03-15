using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;
public class PhysicCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    [Header("检测范围")]//层级
    public bool isGrand;
    [Header("检测参数")] 
    public bool manual;
    public float checkRaduis;
    public Vector2 rightOffset;
    public Vector2 leftOffset;
    public LayerMask grandLayer;
    public Vector2 bottomOffest;
    [Header("状态")] 
    public bool touchRightWall;
    public bool touchLeftWall;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2f, coll.bounds.size.y / 2f);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }

    void Update()
    {
        Check();
    }

    private void Check()
    
    {   //地面判断
        
        //墙体判断                                                                                                                            
        touchLeftWall= Physics2D.OverlapCircle                                                                                        
            ((Vector2)transform.position+new Vector2(leftOffset.x*transform.localScale.x,leftOffset.y), checkRaduis, grandLayer);
        touchRightWall= Physics2D.OverlapCircle                                                                                       
        ((Vector2)transform.position+new Vector2(rightOffset.x*transform.localScale.x,rightOffset.y), checkRaduis, grandLayer);   
        if (gameObject.CompareTag("Fly"))
        {
            isGrand = true;
            return;
        }
    
        isGrand= Physics2D.OverlapCircle
            ((Vector2)transform.position+new Vector2(bottomOffest.x*transform.localScale.x,bottomOffest.y), checkRaduis, grandLayer);
    
    
    
    
    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position+new Vector2(bottomOffest.x*transform.localScale.x,bottomOffest.y), checkRaduis);
       Gizmos.DrawWireSphere((Vector2)transform.position+new Vector2(leftOffset.x*transform.localScale.x,leftOffset.y), checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position+new Vector2(rightOffset.x*transform.localScale.x,rightOffset.y), checkRaduis);
    }


    
}
