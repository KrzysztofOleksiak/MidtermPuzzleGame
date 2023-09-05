using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretsController : MonoBehaviour
{
    [SerializeField] private Transform _ballSpawnPoint, _laserSpawnPoint;
    public bool _canShootBall;

    [SerializeField] public GameObject _Head;
    [SerializeField] private GameObject _bullet;
    [SerializeField] public LineRenderer _LaserRenderer;
    [SerializeField] public float _laserRadius, _bulletDeathTime, _laserDamagePerSecond, _ballShootDelay;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Renderer _eyeLPupil, _eyeRPupil;
    private Material _PupilMaterial;
    [SerializeField] private GameObject _eyebrowL, _eyebrowR;
    [SerializeField] private Vector3 _angryPosition, _calmPosition;
    private TurretState _currentState;
    public Transform _player;
    internal void ShootBall(float distancetoPlayer)
    {
        float shootForce = distancetoPlayer;
        if (_canShootBall)
        {
            _canShootBall = false;
            StartCoroutine(FireDelay());
            GameObject projectile = Instantiate(_bullet, _ballSpawnPoint.position, _ballSpawnPoint.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(_ballSpawnPoint.up *300);
            Destroy(projectile, _bulletDeathTime);
        }
    }
    internal void ShootLaser()
    {
        _LaserRenderer.SetPosition(0, _laserSpawnPoint.position);
        RaycastHit hit;
        if(Physics.Raycast(_laserSpawnPoint.position, _laserSpawnPoint.up*72, out hit))
        {
            if (hit.collider)
            {
                _LaserRenderer.SetPosition(1, hit.point);
                if (hit.transform.gameObject.tag == "Player")_playerHealth.DeductHealth(_laserDamagePerSecond * Time.deltaTime);
                if (hit.transform.gameObject.tag == "LaserDeflect")hit.transform.gameObject.GetComponent<LaserDeflector>().ShootLaser(_laserDamagePerSecond, new List<GameObject>(), _playerHealth);
                if (hit.transform.gameObject.tag == "Enemy") hit.transform.gameObject.GetComponent<Health>().DeductHealth(_laserDamagePerSecond * Time.deltaTime);
            }
        }
        else
        {
            _LaserRenderer.SetPosition(1, _laserSpawnPoint.position + _laserSpawnPoint.up.normalized * 72);
        }
    }
    internal void ChangeEyeColor(Color color)
    {
        Material newMaterial = new Material(_PupilMaterial); // Create a new material based on the original
        newMaterial.color = color; // Set the new color

        _eyeLPupil.material = newMaterial; // Assign the new material to the left eye
        _eyeRPupil.material = newMaterial; // Assign the new material to the right eye
    }
    internal void SetEyesCalm()
    {
        _eyebrowL.transform.localPosition = _calmPosition;
        _eyebrowR.transform.localPosition = _calmPosition;

        _eyebrowL.transform.localEulerAngles = Vector3.zero;
        _eyebrowR.transform.localEulerAngles = Vector3.zero;
    }
    internal void SetEyesAngry()
    {
        _eyebrowL.transform.localPosition = _angryPosition;
        _eyebrowR.transform.localPosition = _angryPosition;

        _eyebrowL.transform.localEulerAngles = new Vector3(0, 0, -25);
        _eyebrowR.transform.localEulerAngles = new Vector3(0, 0, 25);
    }
    void Start()
    {
        _PupilMaterial = _eyeLPupil.material;// doesnt matter which eye you take material from
        _currentState = new TurretIdleState(this);
        _canShootBall = true;
        _currentState.OnStateEnter();
    }
    void Update()
    {
        _currentState.OnStateUpdate();
    }

    public void ChangeStateTo(TurretState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }
    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(_ballShootDelay);
        _canShootBall = true;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Player Found!!!!");
            _player = other.transform;
            ChangeStateTo(new TurretBallState(this));
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 lookAtPosition = other.transform.position;
            lookAtPosition.y = _Head.transform.position.y; // Lock rotation vertically

            _Head.transform.LookAt(lookAtPosition);
            _player = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Player Left");

            _player = null;
            ChangeStateTo(new TurretIdleState(this));
        }
    }
}


