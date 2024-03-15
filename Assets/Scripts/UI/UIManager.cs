using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStateBar playerStateBar;
    [Header("事件监听")] 
    public npcEventSO healthEvent;
    public playerEventSO powerEvent;
    //不能使用awake唤醒

    private void OnEnable()
    {
        healthEvent.OnEventRasied += OnHealthEvent;
        powerEvent.OnEventRasied += PowerEvent;
    }
    
    private void OnDisable()
    {
        healthEvent.OnEventRasied -= OnHealthEvent;
        powerEvent.OnEventRasied += PowerEvent;
    }

    private void PowerEvent(PlayeController pler)
    {
        var persentage = pler.currentPower / pler.MaxPower;
        playerStateBar.OnpowerChange(persentage);
    }

    private void OnHealthEvent(npcs _npcs)
    {
        var persentage = _npcs.CurrentHealth / _npcs.MaxHealth;
        playerStateBar.OnHealthChange(persentage);
    }
}
    
