using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class XunLuo :BaceState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        
    }

    public override void LogicUpdate()
    {
        bool isGround;
        bool isFaceWall = (currentEnemy._physicCheck.touchLeftWall && currentEnemy.faceDir.x > 0) ||
                          (currentEnemy.faceDir.x < 0 && currentEnemy._physicCheck.touchRightWall);
        if (currentEnemy.isFly)
        {
            if ((currentEnemy._physicCheck.touchLeftWall ) ||
                ( currentEnemy._physicCheck.touchRightWall))
            {
                currentEnemy. wait = true;
                currentEnemy. anim.SetBool("Walk",false);
            }
            else
            {
                currentEnemy.anim.SetBool("Walk",true);
            }
           
        }
        else
        {
            if (currentEnemy._physicCheck.isGrand)
            {
                if ((currentEnemy._physicCheck.touchLeftWall ) ||
                    ( currentEnemy._physicCheck.touchRightWall))
                {
                    currentEnemy. wait = true;
                    currentEnemy. anim.SetBool("Walk",false);
                }
                else
                {
                    currentEnemy.anim.SetBool("Walk",true);
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
       
    }

    public override void OnExit()
    {
        currentEnemy. anim.SetBool("Walk",false);
    }
}
