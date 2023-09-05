using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoubleDoor : MonoBehaviour
{
    [SerializeField] private MeshRenderer _doorLRenderer, _doorRRenderer, _doorPadRenderer;
    [SerializeField] private Material _defualtDoorColor, _detectedDoorColor, _padEmmissionLocked, _padEmmissionUnlocked;
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private TMP_Text _padText;
    private float timer = 0;
    [SerializeField] private float waitTime;
    private bool _unlocked;

    private void Awake()
    {
        Lock();
    }
    public void Unlock()
    {
        _unlocked = true;
        Material[] newMaterials = _doorPadRenderer.sharedMaterials; // Create a new material based on the original
        newMaterials[1] = _padEmmissionUnlocked;
        _doorPadRenderer.sharedMaterials = newMaterials;
        _padText.color = Color.green;
        _padText.text = "Unlocked";
    }
    public void Lock()
    {
        _unlocked = false;
        Material[] newMaterials = _doorPadRenderer.sharedMaterials; // Create a new material based on the original
        newMaterials[1] = _padEmmissionLocked;
        _doorPadRenderer.sharedMaterials = newMaterials;
        _padText.color = Color.red;
        _padText.text = "Locked";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            timer = 0;
            _doorLRenderer.material = _detectedDoorColor;
            _doorRRenderer.material = _detectedDoorColor;
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
                _doorAnimator.SetBool("Open", true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _doorAnimator.SetBool("Open", false);
            _doorLRenderer.material = _defualtDoorColor;
            _doorRRenderer.material = _defualtDoorColor;
        }
    }
}
