using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("伤害类参数")]
    public float damage;//伤害
    public float attackRange;//攻击范围
    public float attacktimes;//攻击频率
 

    private void OnTriggerStay2D(Collider2D other)
    {
        //?实现检测是否对方有无这个代码
        other.GetComponent<npcs>()?.TakeDamge(this);
    }
}
