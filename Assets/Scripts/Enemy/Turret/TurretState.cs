using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretState
{
    protected TurretsController _turret;

    public TurretState(TurretsController enemy)
    {
        this._turret = enemy;
    }


    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStateExit();
    
}
