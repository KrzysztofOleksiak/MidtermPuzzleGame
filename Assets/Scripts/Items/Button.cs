using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, ISelectable
{
    [SerializeField]
    private Material _defaultColor;

    [SerializeField]
    private Material _hoverColor;

    [SerializeField]
    private MeshRenderer _renderer;
    [SerializeField] private Animator _buttonAnimator;

    public UnityEvent _onButtonPush;

    public void OnHoverEnter()
    {
        _renderer.material = _hoverColor;
    }

    public void OnHoverExit()

    {
        _renderer.material = _defaultColor;

    }

    public void OnSelect()
    {
        _onButtonPush?.Invoke();
        _buttonAnimator.SetTrigger("Push");
    }

}
