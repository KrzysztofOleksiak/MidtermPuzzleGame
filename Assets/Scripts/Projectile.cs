using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _damage;
    private void OnCollisionEnter(Collision collision)
    {
        IDestroyable destroyable = collision.gameObject.GetComponent<IDestroyable>();

        if(destroyable != null)destroyable.OnCollided();
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") collision.gameObject.GetComponent<Health>().DeductHealth(_damage);
        Destroy(gameObject);
    }
}
