using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentsController : MonoBehaviour
{
    //Agent Values
    private EnemyState _currentState;
    public NavMeshAgent navMeshAgent;
    public Transform[] targetPoints;
    public Transform player;
    public float playerCheckDistance;
    public float damagePerSecond;
    public Health playerHealth;
    [SerializeField] private Transform _eye;
    [SerializeField] private MeshRenderer _agentMesh;
    [SerializeField] private Material _regularMaterial, _angryMaterial;
    //Gun Parts
    public bool gun, canShoot;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _gunDelay, _shootForce;
    [SerializeField] private GameObject _bullet;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _currentState = new EnemyIdleState(this);
        _currentState.OnStateEnter();
    }
    void Update()
    {
        _currentState.OnStateUpdate();  
    }
    public void ChangeStateTo(EnemyState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }
    public bool IsPlayerInLineOfSight()
    {
        if (player == null)return false; // Player not found
        Vector3 directionToPlayer = player.position - _eye.position;
        // Perform a raycast from the enemy to the player
        RaycastHit hit;
        Debug.DrawRay(_eye.position, directionToPlayer);
        if (Physics.Raycast(_eye.position, directionToPlayer, out hit, playerCheckDistance))
        {
            // Check if the raycast hit the player
            if (hit.collider.CompareTag("Player")) return true; // Player is in line of sight
        }
        return false; // Player is not in line of sight
    }
    public IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(_gunDelay);
        canShoot = true;
    }
    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            StartCoroutine(FireDelay());
            GameObject projectile = Instantiate(_bullet, _shootPoint.transform.position, _shootPoint.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(-_shootPoint.transform.forward * _shootForce);
            Destroy(projectile, 5f);
        }
    }
    public void SetMeshAngry(bool angry)
    {
        if (angry) _agentMesh.material = _angryMaterial;
        else _agentMesh.material = _regularMaterial;
    }
}


