using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float _maxHealth;
    [SerializeField] private bool _player;

    public Action<float> OnHealthUpdate;

    private float _health;

    [SerializeField] GameObject dropItem;
    [SerializeField] Animator _cameraAnimator;
    [SerializeField] MeshRenderer _mesh;
    [SerializeField] private Material _deadColor;
    [SerializeField] private UIManager _UIManager;

    void Start()
    {
        _health = _maxHealth;
        if(_player)OnHealthUpdate(_maxHealth);
    }
    public void DeductHealth(float value)
    {
        _health -= value;
        //Debug.Log(_health);
        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
        else if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        if(_player)OnHealthUpdate(_health);
    }
    void Die()
    {
        if (_player)
        {
            _mesh.material = _deadColor;
            _cameraAnimator.SetBool("Dead", true);
            _UIManager.GameOverScreen();
            gameObject.GetComponent<PlayerController>().SetGameOver(true);
        }
        else
        {
            if (dropItem != null)
            {
                dropItem.SetActive(true);
                dropItem.transform.position = new Vector3(gameObject.transform.position.x, dropItem.transform.position.y, gameObject.transform.position.z);
            }
            Destroy(gameObject);
        }
        
    }
}
