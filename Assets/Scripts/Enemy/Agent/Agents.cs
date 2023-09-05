using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agents : MonoBehaviour
{
   public NavMeshAgent _agent;

    public Transform _targetPoint;

    [SerializeField] public Transform[] _targetPoints;

    [SerializeField] public Transform _agentEye;


    [SerializeField] public float _playerCheckDistance;

    [SerializeField] public float _checkRadius = .04f;


    int currentTarget = 0;


    public bool isIdle = true;


    public bool isPlayerFound;


    public bool isCloseToPlayer;


    public Transform _player;



    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _targetPoints[currentTarget].position;


    }


    void Update()
    {
        // _agent.destination = _targetPoint.position;

        if (isIdle)
        {
            Idle();
        }else if (isPlayerFound)
        {
            if (isCloseToPlayer)
            {
                AttackPlayer();
            }
            else
            {
                FollowPlayer();
            }
        }

    }

    void Idle() {

        if(_agent.remainingDistance < 0.1f)
        {
           
            currentTarget++;
            if(currentTarget >= _targetPoints.Length)
            {
                currentTarget = 0;
              
            }
            _agent.destination = _targetPoints[currentTarget].position;
        }

        if(Physics.SphereCast(_agentEye.position,_checkRadius,transform.forward,out RaycastHit hit, _playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player Found!!!!");
                isIdle = false;
                isPlayerFound = true;
                _player = hit.transform;
                _agent.destination = _player.position;

            }
        }

    }

    void FollowPlayer()
    {
        if (_player != null)
        {
            if(Vector3.Distance(transform.position, _player.position) < 2)
            {
                isCloseToPlayer = true;
            }
            else
            {
                isCloseToPlayer = false;
            }
            _agent.destination = _player.position;
        }
        else
        {
            isPlayerFound = false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }

    void AttackPlayer()
    {

        Debug.Log("PLAYER ATTACKKED!!");


        if (Vector3.Distance(transform.position,_player.position) > 2)
        {
            isCloseToPlayer = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_agentEye.position, _checkRadius);
        Gizmos.DrawWireSphere(_agentEye.position + _agentEye.forward * _playerCheckDistance, _checkRadius);

        Gizmos.DrawLine(_agentEye.position, _agentEye.position + _agentEye.forward * _playerCheckDistance);
    }

}
