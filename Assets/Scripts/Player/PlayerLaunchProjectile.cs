using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunchProjectile : MonoBehaviour
{


    [SerializeField]
    public GameObject _redProjectile;


    [SerializeField]
    public GameObject _yellowProjectile;

    [SerializeField]
    public  GameObject _spawnPoint;


    private PlayerInput _input;


    //Added refernce to objectpool
    [SerializeField] public ObjectPool _bulletPool;

    //Added refernce to objectpool
    [SerializeField] public ObjectPool _bulletPool2;


    [SerializeField] private float _shootvelocity;


    [SerializeField] private float _finalShootvelocity;


    private IShootStrategy _currentShootStratagey;


    // Start is called before the first frame update
    void Start()
    {
        _input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public void Interact()
    {

        if(_currentShootStratagey == null)
        {
            _currentShootStratagey = new RedShootStrategy(this);
        }
        if (_input._luancher1Pressed)
        {
            _currentShootStratagey = new RedShootStrategy(this);
        }
        if (_input._luancher2Pressed)
        {
            _currentShootStratagey = new YellowShootStrategy(this);
        }


        //
        if(_input._mousePress && _currentShootStratagey != null)
        {
            _currentShootStratagey.Shoot();
        }
        

         
    }





    


    


    
}
