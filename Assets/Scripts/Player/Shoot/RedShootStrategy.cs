using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedShootStrategy : IShootStrategy
{

    PlayerLaunchProjectile _luanchInteractor;

    Transform _shootPoint;

    //Constructor
    public RedShootStrategy(PlayerLaunchProjectile luanchInteractor)
    {
        Debug.Log("Changed To Red Projectile Mode");
        _luanchInteractor = luanchInteractor;
        _shootPoint = luanchInteractor._spawnPoint.transform;

        //change color
        _luanchInteractor._spawnPoint.gameObject.GetComponentInParent<MeshRenderer>().material.color = _luanchInteractor._redProjectile.GetComponent<MeshRenderer>().sharedMaterial.color;



    }


    public void Shoot()
    {
        PooledObject projectile = _luanchInteractor._bulletPool.GetPooledObject();
        projectile.gameObject.SetActive(true);
        projectile.transform.position = _luanchInteractor._spawnPoint.transform.position;
        projectile.transform.rotation = _luanchInteractor._spawnPoint.transform.rotation;
        projectile.GetComponent<Rigidbody>().AddForce(_luanchInteractor._spawnPoint.transform.forward * 1000);
       _luanchInteractor._bulletPool.DestroyPooledObject(projectile, 5.0f);
    }
}
