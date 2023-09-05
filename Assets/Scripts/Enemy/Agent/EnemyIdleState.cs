using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    int _currentTarget = 0;
    public EnemyIdleState(AgentsController enemy): base(enemy)
    {

    }
    public override void OnStateEnter()
    {
        _enemy.SetMeshAngry(false);
        //Debug.Log("Idle");
        _enemy.navMeshAgent.destination = _enemy.targetPoints[_currentTarget].position;
    }
    public override void OnStateExit()
    {
    }
    public override void OnStateUpdate()
    {
        if (_enemy.navMeshAgent.remainingDistance < 0.1f)
        {
            _currentTarget++;
            if (_currentTarget >= _enemy.targetPoints.Length)
            {
                _currentTarget = 0;
            }
            _enemy.navMeshAgent.destination = _enemy.targetPoints[_currentTarget].position;
        }
        if (_enemy.IsPlayerInLineOfSight())
        {
            _enemy.navMeshAgent.destination = _enemy.player.position;
            _enemy.ChangeStateTo(new EnemyFollowState(_enemy));
        }
    }
}
