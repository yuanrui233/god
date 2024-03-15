using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        XunLuoState = new XunLuo();
    }
}
