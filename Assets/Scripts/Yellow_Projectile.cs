using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _forceUp, _maxVelocity, _damage;
    [SerializeField] private string _ground;
    private bool push;

    private void Start()
    {
        push = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDestroyable destroyable = collision.gameObject.GetComponent<IDestroyable>();

        if(destroyable != null)
        {
            destroyable.OnCollided();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == _ground) push = true;
        //if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") Attack(collision.gameObject.GetComponent<Health>());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy") Attack(other.gameObject.GetComponent<Health>());
    }
    void Attack(Health health)
    {
        health.DeductHealth(_damage);
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if (push&& System.Math.Abs(_rb.velocity.x)+ System.Math.Abs(_rb.velocity.z)<_maxVelocity) {
            Vector3 force = transform.forward * _forceUp;
            force.y = 0f; // Zero out the vertical component of the force

            _rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
