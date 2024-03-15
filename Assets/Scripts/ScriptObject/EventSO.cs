using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName  = "Event/EventSO")]

public class EventSO :ScriptableObject
{

    public UnityAction OnEventRised;


    public void RaiseEvent()
    {
        OnEventRised?.Invoke();
    }

}
