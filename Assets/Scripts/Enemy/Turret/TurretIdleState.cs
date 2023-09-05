using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : TurretState
{
    public TurretIdleState(TurretsController enemy): base(enemy)
    {

    }
    public override void OnStateEnter()
    {
        _turret.ChangeEyeColor(Color.black);
        _turret.SetEyesCalm();
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateExit()
    {

    }
}
