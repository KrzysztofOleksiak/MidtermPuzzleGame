using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private Camera _cam;
    [SerializeField] private float _interactionDisctance;
    [SerializeField] private LayerMask _interactionLayerMask;

    //Raycast
    private RaycastHit _raycastHit;
    private ISelectable _selectable;
    [Header("Pick and Drop")]
    [SerializeField] LayerMask _pickupLayerMask;
    [SerializeField] float _pickupDisctance;
    [SerializeField] Transform _attachTransform;

    // Pick and Drop
    private bool _isPicked = false;
    private IPickable _pickable;
    private PlayerInput _input;
    // Start is called before the first frame update
    void Start()
    {
        _input = PlayerInput.GetInstance();
    }
    // Update is called once per frame
    void Update()
    {
        Interact();
        PickAndDrop();
    }
    void Interact()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out _raycastHit, _interactionDisctance, _interactionLayerMask))
        {
            _selectable = _raycastHit.transform.GetComponent<ISelectable>();
            if (_selectable != null)
            {
                _selectable.OnHoverEnter();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _selectable.OnSelect();
                }
            }
        }
        if (_selectable != null && _raycastHit.transform == null)
        {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    }
    void PickAndDrop()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out _raycastHit, _pickupDisctance, _pickupLayerMask))
        {
            if (_input._activatedPress && !_isPicked)
            {
                _pickable = _raycastHit.transform.GetComponent<IPickable>();
                if (_pickable != null)
                {
                    _pickable.OnPicked(_attachTransform);
                    _isPicked = true;
                    return;
                }
            }
            if (_input._activatedPress && _isPicked)
            {
                _pickable.OnDropped();
                _isPicked = false;
            }
        }
    }
}
