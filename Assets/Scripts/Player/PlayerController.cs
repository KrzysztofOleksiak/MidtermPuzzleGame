using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera/Looking")]
    //Camera
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private bool _invertedMouse;
    [SerializeField] private Animator _cameraAnimator;
    private float _mouseX, _mouseY;
    private float _cameraXRotation;


    [Header("Movement")]
    //Movement
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce, _gravity = -9.8f;
    private float _horizontal, _vertical;
    private CharacterController _characterController;
    private Vector3 _playerVelocity;


    [Header("Gun")]
    //Gun
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _shootPoint1, _gunMesh;
    [SerializeField] private float _delay, _shootForce;
    private bool _canShoot, _gameOver;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _canShoot = false;
        _gameOver = true;
    }


    void Update()
    {
        if (!_gameOver)
        {
            GetInput();
            RotatePlayer();
            MovePlayer();
            Jump();
            LaunchProjectile();
        }
    }


    void LaunchProjectile() {

        if (Input.GetButton("Fire1")&&_canShoot)
        {
            _canShoot = false;
            StartCoroutine(FireDelay());
            GameObject projectile = Instantiate(_projectile, _shootPoint1.transform.position, _shootPoint1.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(-_shootPoint1.transform.forward * _shootForce);
            Destroy(projectile, 5f);
        }

    }

    public void SetShoot(bool active)
    {
        StopCoroutine(FireDelay());
        _canShoot = active;
        _gunMesh.SetActive(active);
    }

    public IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(_delay);
        _canShoot = true;
    }

    void Jump()
    {

        if (_characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {

                _playerVelocity.y = _jumpForce;
            }
        }

    }


    void MovePlayer()
    {

        _characterController.Move(((transform.forward * _vertical) + (transform.right * _horizontal)) * _moveSpeed * Time.deltaTime); // (0,0,1)+ (1,0,0) = (1,0,1)


        if(_characterController.isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }



        _playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);

    }


    void RotatePlayer()
    {
        //Turn Player side to side
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _mouseX);

        //Turn Player head up and down
        _cameraXRotation += Time.deltaTime * _mouseY * _turnSpeed * (_invertedMouse ? 1 : -1);

        _cameraXRotation = Mathf.Clamp(_cameraXRotation, -85, 85);

        _cameraTransform.localRotation = Quaternion.Euler(_cameraXRotation, 0, 0);

    }

    void GetInput()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    public void SetGameOver(bool over)
    {
        _gameOver = over;
        _cameraAnimator.enabled = over;
        if (over) SetShoot(false);
    }


    // Turnary Operator Longhand (_invertedMouse ? 1 : -1); Used in RotatePlayer - left as commented to remind me.
    /*int InvertMouseCheck()
    {
        if (_invertedMouse)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }*/
}

