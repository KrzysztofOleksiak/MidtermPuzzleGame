using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCube : MonoBehaviour, IPickable
{
    //FixedJoint _joint;

    Rigidbody _cubeRB;


    // Start is called before the first frame update
    void Start()
    {
        _cubeRB = GetComponent<Rigidbody>();
    }

    public void OnPicked(Transform attachTransform)
    {
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;

        transform.SetParent(attachTransform);

        _cubeRB.isKinematic = true;
        _cubeRB.useGravity = false;

    }


    public void OnDropped() {


        _cubeRB.isKinematic = false;
        _cubeRB.useGravity = true;
        transform.SetParent(null);

    }

}
