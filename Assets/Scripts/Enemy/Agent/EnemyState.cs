using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState 
{
    protected AgentsController _enemy;

    public EnemyState(AgentsController enemy)
    {
        this._enemy = enemy;
    }


    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStateExit();
    
}
