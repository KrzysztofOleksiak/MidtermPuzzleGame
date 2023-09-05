using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaserState : TurretState
{    
    float _distanceToPlayer;
    //Health _playerHealth;

    public TurretLaserState(TurretsController enemy) : base(enemy)
    {
        //_playerHealth = enemy._player.GetComponent<Health>();
    }

    public override void OnStateEnter()
    {
        _turret._LaserRenderer.enabled = true;
        _turret.ChangeEyeColor(Color.red);
        _turret.SetEyesAngry();
    }

    public override void OnStateUpdate()
    {
        _distanceToPlayer = Vector3.Distance(_turret.transform.position, _turret._player.position);
        if (_turret._player != null)
        {
            if (_distanceToPlayer > _turret._laserRadius)
            {
                _turret.ChangeStateTo(new TurretBallState(_turret));
            }
            _turret.ShootLaser();
        }
        else
        {
            _turret.ChangeStateTo(new TurretIdleState(_turret));
        }
    }

    public override void OnStateExit()
    {
        _turret._LaserRenderer.enabled = false;
    }
}
