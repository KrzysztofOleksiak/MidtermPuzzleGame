using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBallState : TurretState
{
    float distancetoPlayer;
    public TurretBallState(TurretsController enemy): base(enemy)
    {

    }
    public override void OnStateEnter()
    {
        _turret.ChangeEyeColor(Color.black);
        _turret.SetEyesAngry();
    }
    public override void OnStateUpdate()
    {
        if (_turret._player != null)
        {
            distancetoPlayer = Vector3.Distance(_turret.transform.position, _turret._player.position);
            _turret.ShootBall(distancetoPlayer);
            if (distancetoPlayer < _turret._laserRadius)_turret.ChangeStateTo(new TurretLaserState(_turret));
        }
        else
        {
            _turret.ChangeStateTo(new TurretIdleState(_turret));
        }
    }
    public override void OnStateExit()
    {
    }
}
