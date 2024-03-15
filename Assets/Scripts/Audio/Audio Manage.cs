using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManage : MonoBehaviour
{
    [Header("音源组件")]
    public AudioSource BGMSource;
    public AudioSource FXSource;
    [Header("事件监听")] 
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;

    private void OnEnable()
    {
        FXEvent.OnEventRaised += OnFXEvent;
        BGMEvent.OnEventRaised += OnBGMEvent;
    }

    private void OnDisable()
    {
        FXEvent.OnEventRaised -= OnFXEvent;
        BGMEvent.OnEventRaised -= OnBGMEvent;
    }

    private void OnBGMEvent(AudioClip cilp)
    {
        BGMSource.clip = cilp;
        BGMSource.Play();
    }

    private void OnFXEvent(AudioClip cilp)
    {
        FXSource.clip = cilp;
        FXSource.Play();
    }
}
