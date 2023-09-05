using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float _distanceToPlayer;
    public EnemyAttackState(AgentsController enemy) : base(enemy)
    {
    }
    public override void OnStateEnter()
    {
        _enemy.SetMeshAngry(true);
        //Debug.Log("Attack");
    }
    public override void OnStateUpdate()
    {
        Attack();
        _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy.player.position);
        if (_enemy.player != null)
        {
            if (_distanceToPlayer > _enemy.playerCheckDistance/5)
            {
                _enemy.ChangeStateTo(new EnemyFollowState(_enemy));
            }
            _enemy.navMeshAgent.destination = _enemy.player.position;
        }
        else
        {
            _enemy.ChangeStateTo(new EnemyIdleState(_enemy));
        }
    }
    public override void OnStateExit()
    {
    }
    void Attack(){
        if(_enemy.playerHealth != null) _enemy.playerHealth.DeductHealth(_enemy.damagePerSecond * Time.deltaTime);
    }
}
