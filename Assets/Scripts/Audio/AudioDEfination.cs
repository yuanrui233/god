using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDEfination : MonoBehaviour
{

    public PlayAudioEventSO playAudioEvent;
    public AudioClip audioClip;
    public bool playOnEnable;

    private void OnEnable()
    {
        if(playOnEnable)
            PlayAudioClip();
    }

    private void PlayAudioClip()
    {
        playAudioEvent.OnEventRaised(audioClip);
    }
}
