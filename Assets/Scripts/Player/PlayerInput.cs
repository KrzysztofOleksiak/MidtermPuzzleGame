using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    // Movement
    public float _horizontal { get; private set; }

    public float _vertical { get; private set; }

    public float _mouseX { get; private set; }

    public float _mouseY { get; private set; }

    public bool _jumpActivated { get; private set; }


    //Interact
    public bool _activatedPress { get; private set; }

    public bool _mousePress { get; private set; }

    //public bool _luanchPress2 { get; private set; }

    public bool _luancher1Pressed { get; private set; }

    public bool _luancher2Pressed { get; private set; }





    /// Singleton Pattern

    private static PlayerInput instance;


    /// Insures there is only one Instance of PlayerInput
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
            return;
        }
        instance = this;
    }

    ///Giving other classes access to this instance
    public static PlayerInput GetInstance()
    {
        return instance;
    }


    ///End Singleton

    private void Start()
    {
        //Hide Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }




    // Update is called once per frame
    void Update()
    {
        ProccessInputs();
    }

    void ProccessInputs()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _jumpActivated = Input.GetButtonDown("Jump");
        _activatedPress = Input.GetKeyDown(KeyCode.E);

        _mousePress = Input.GetButtonDown("Fire1");
        // _luanchPress2 = Input.GetButtonDown("Fire2");


        //Assign luancher based on number key pressed
        _luancher1Pressed = Input.GetKeyDown(KeyCode.Alpha1);
        _luancher2Pressed = Input.GetKeyDown(KeyCode.Alpha2);

    }
}
