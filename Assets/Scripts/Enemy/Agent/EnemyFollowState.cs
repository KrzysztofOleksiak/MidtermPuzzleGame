using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyState
{
    float distancetoPlayer;
    public EnemyFollowState(AgentsController enemy): base(enemy)
    {

    }
    public override void OnStateEnter()
    {
        //Debug.Log("follow");
        _enemy.SetMeshAngry(true);
        _enemy.canShoot = true;
    }
    public override void OnStateUpdate()
    {
        if (_enemy.player != null)
        {
            distancetoPlayer = Vector3.Distance(_enemy.transform.position, _enemy.player.position);
            if (distancetoPlayer > _enemy.playerCheckDistance)
            {
                _enemy.ChangeStateTo(new EnemyIdleState(_enemy));
            }
            if (distancetoPlayer < _enemy.playerCheckDistance / 5)
            {
                _enemy.ChangeStateTo(new EnemyAttackState(_enemy));
            }

            _enemy.navMeshAgent.destination = _enemy.player.position;
            if (_enemy.gun)_enemy.Shoot();
        }
        else
        {
            _enemy.ChangeStateTo(new EnemyIdleState(_enemy));
        }
    }
    public override void OnStateExit()
    {
        _enemy.StopAllCoroutines();
        _enemy.canShoot = false;
    }
}
