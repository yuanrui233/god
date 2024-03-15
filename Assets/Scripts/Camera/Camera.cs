using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;


public class Camera : MonoBehaviour
{
    
    private CinemachineConfiner2D confiner2D;
    public CinemachineImpulseSource impulseSource;
    public EventSO cameraShake;
    private void Awake()
    {
       
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        cameraShake.OnEventRised += OncameraShakeEvent;
    }

    private void OnDisable()
    {
        cameraShake.OnEventRised -= OncameraShakeEvent;
    }

    private void OncameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }


    private void Start()
    {
        GetNewBounds();
    }

    private  void GetNewBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null)
        {
            return;
        }

        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        //缓存
        confiner2D.InvalidateCache();
    }
    
}
