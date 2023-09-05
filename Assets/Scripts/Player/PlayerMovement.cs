using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField]
    private float _turnSpeed;

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private Transform _cameraTransform;

    [SerializeField]
    private bool _invertedMouse;

    [SerializeField]
   public float _gravity = -9.8f;

    [SerializeField]
    private float _jumpForce;


    private CharacterController _characterController;

    private float _cameraXRotation;

    private Vector3 _playerVelocity;

    [SerializeField]
    private PlayerInput _input;




    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        MovePlayer();
        Jump();
    }


    void Jump()
    {

        if (_characterController.isGrounded)
        {
            if (_input._jumpActivated)
            {

                _playerVelocity.y = _jumpForce;
            }
        }

    }


    void MovePlayer()
    {

        _characterController.Move(((transform.forward * _input._vertical) + (transform.right * _input._horizontal)) * _moveSpeed * Time.deltaTime); // (0,0,1)+ (1,0,0) = (1,0,1)


        if (_characterController.isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }



        _playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);

    }


    void RotatePlayer()
    {
        //Turn Player side to side
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _input._mouseX);

        //Turn Player head up and down
        _cameraXRotation += Time.deltaTime * _input._mouseY * _turnSpeed * (_invertedMouse ? 1 : -1);

        _cameraXRotation = Mathf.Clamp(_cameraXRotation, -85, 85);

        _cameraTransform.localRotation = Quaternion.Euler(_cameraXRotation, 0, 0);

    }

}
