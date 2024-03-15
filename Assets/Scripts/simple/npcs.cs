using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class npcs : MonoBehaviour
{
    //伤害UI
    public UnityEvent<npcs> OnHealthChange;
    [Header("基础数值")] 
    public float MaxHealth;
    public float CurrentHealth;
    //计时器算法
    [Header("受伤无敌")] 
    public float nodieTime;//无敌的全部时间
    private float gapTime;//无敌时间缩减（couter）
    public bool isNb=false;//是否无敌


    public UnityEvent<Transform> OnTakeDamge;
    public UnityEvent Ondie;


    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnHealthChange?.Invoke(this);
    }

    private void Update()
    {
        if (isNb)
        {
            gapTime -= Time.deltaTime;
        }

        if (gapTime <= 0)
        {
            isNb = false;
        }
        
    }

    public void TakeDamge(Attack attacker)
    { 
        //判断
        if (isNb) return;
        if (CurrentHealth - attacker.damage > 0)
        {
            CurrentHealth -= attacker.damage;
            TriggerNoDie();
            //执行受伤
            OnTakeDamge?.Invoke(attacker.transform);
        }
        else
        {
            CurrentHealth = 0;
         //执行死亡
         Ondie?.Invoke();
        }
        OnHealthChange?.Invoke(this);
    }
    
    private void TriggerNoDie()
    {
        if (!isNb)
        {
            isNb = true;
            gapTime = nodieTime;
        }
    }
}
