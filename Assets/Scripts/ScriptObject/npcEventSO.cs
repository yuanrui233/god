using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Event/npcEventSO")]


public class npcEventSO : ScriptableObject
{

    public UnityAction<npcs> OnEventRasied;

    public void RaiseEvent(npcs _npcs)
    {
        OnEventRasied?.Invoke(_npcs);
    }


}
