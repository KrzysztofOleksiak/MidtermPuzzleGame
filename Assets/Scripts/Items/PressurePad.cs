using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{
    [SerializeField] private MeshRenderer _plateRenderer;
    [SerializeField] private Material _plateEmmissionLocked, _plateEmmissionUnlocked;
    [SerializeField] private Animator _plateAnimator;
    [SerializeField] private string _triggerTag;
    public UnityEvent onCubePlaced;
    public UnityEvent onCubeRemoved;
    private void OnTriggerEnter(Collider other)
    {
        if (_triggerTag == "" || other.tag == _triggerTag)
        {
            onCubePlaced?.Invoke();
            _plateAnimator.SetBool("Down", true);
            Material[] newMaterials = _plateRenderer.sharedMaterials; // Create a new material based on the original
            newMaterials[1] = _plateEmmissionUnlocked;
            _plateRenderer.sharedMaterials = newMaterials;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_triggerTag == "" || other.tag == _triggerTag)
        {
            onCubeRemoved?.Invoke();
            _plateAnimator.SetBool("Down", false);
            Material[] newMaterials = _plateRenderer.sharedMaterials; // Create a new material based on the original
            newMaterials[1] = _plateEmmissionLocked;
            _plateRenderer.sharedMaterials = newMaterials;
        }
    }
}
