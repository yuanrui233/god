using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Event/playerEventSO")]




public class playerEventSO : ScriptableObject
{
   public UnityAction<PlayeController> OnEventRasied;
   public void RaiseEvent(PlayeController pler)
   {
      OnEventRasied?.Invoke(pler);
   }

}
