using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private MeshRenderer _doorRenderer;
    [SerializeField] private Material defualtDoorColor, detectedDoorColor;
    [SerializeField] private Animator doorAnimator;
    private float timer = 0;
    private float waitTime = 0.27f;
    private bool _unlocked;

    private void Awake()
    {
        _unlocked = false;
    }
    public void Unlock()
    {
        _unlocked = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            timer = 0;
            _doorRenderer.material = detectedDoorColor;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;
        if (other.tag == "Player" && _unlocked)
        {
            if (timer >= waitTime)
            {
                timer = waitTime;
                doorAnimator.SetBool("Open", true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        doorAnimator.SetBool("Open", false);
        _doorRenderer.material = defualtDoorColor;
    }
}
