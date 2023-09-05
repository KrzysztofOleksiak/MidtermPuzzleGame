using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDeflector : MonoBehaviour
{
    [SerializeField] public LineRenderer _LaserRenderer;
    [SerializeField] private Transform _laserSpawnPoint;
    [SerializeField] private Renderer _gemRenderer;
    [SerializeField] private Material _gemMaterial;
    [SerializeField] private Color _gemOn, _gemOff;
    bool _laserUp, _turnOff;
    //[SerializeField] private GameObject _hitObject;
    public void ShootLaser(float _laserDamagePerSecond, List<GameObject> deflectChain, Health _playerHealth)
    {
        _laserUp = true;
        _turnOff = false;
        ChangeColor();
        deflectChain.Add(this.gameObject);
        _LaserRenderer.SetPosition(0, _laserSpawnPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(_laserSpawnPoint.position, _laserSpawnPoint.forward*72, out hit))
        {
            if (hit.collider)
            {
                _LaserRenderer.SetPosition(1, hit.point);
                _LaserRenderer.enabled = true;
                if (hit.transform.gameObject.tag == "Player") _playerHealth.DeductHealth(_laserDamagePerSecond * Time.deltaTime);
                if (hit.transform.gameObject.tag == "LaserDeflect" && !deflectChain.Contains(hit.transform.gameObject)) hit.transform.gameObject.GetComponent<LaserDeflector>().ShootLaser(_laserDamagePerSecond, deflectChain, _playerHealth);
                if (hit.transform.gameObject.tag == "Enemy") hit.transform.gameObject.GetComponent<Health>().DeductHealth(_laserDamagePerSecond * Time.deltaTime);

                //_hitObject = hit.transform.gameObject;
            }
        }
        else
        {
            _LaserRenderer.SetPosition(1, _laserSpawnPoint.position + _laserSpawnPoint.forward.normalized * 72);
        }
        
    }
    private void ChangeColor()
    {
        Material[] newMaterials = _gemRenderer.sharedMaterials; // Create a new material based on the original
        Material newGem = new Material(_gemMaterial);
        newGem.color = (_laserUp) ? _gemOn : _gemOff; // Set the new color
        newMaterials[2] = newGem;
        _gemRenderer.sharedMaterials = newMaterials;
    }
    private void Update()
    {
        if (!_laserUp &&_turnOff)
        {
            _LaserRenderer.enabled = false;
            ChangeColor();
        }
        if (_laserUp) {
            _turnOff = true;
            _laserUp = false;
        }
    }
}
