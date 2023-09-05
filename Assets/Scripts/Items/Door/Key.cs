using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPickup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _onPickup?.Invoke();
            Destroy(gameObject);
        }
    }
}
