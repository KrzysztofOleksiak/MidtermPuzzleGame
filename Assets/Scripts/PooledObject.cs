using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour

{
    private float _timer;

    private bool _setToDestroy = false;

    private float _destroyTime = 0;


    ObjectPool _associatedObjectPool;


    [SerializeField] private UnityEvent OnReset;

    // Start is called before the first frame update
   public void SetdObjectPool(ObjectPool pool)
    {
        _associatedObjectPool = pool;
        _timer = 0;
        _destroyTime = 0;
        _setToDestroy = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (_setToDestroy)
        {
            _timer += Time.deltaTime;
        }

        if(_timer >= _destroyTime)
        {
            _setToDestroy = false;
            _timer = 0;
            Destroy();


        }

        
    }

    public void ResteObject()
    {
        OnReset?.Invoke();
    }


    public void Destroy()
    {
        if(_associatedObjectPool != null)
        {
            _associatedObjectPool.RestoreObject(this);
        }
      
    }

    public void Destroy(float time)
    {
        _setToDestroy = true;
        _destroyTime = time;
    }

   
    public void ResetObject()
    {
        OnReset?.Invoke();
    }
}
