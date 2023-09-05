using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. Picks random dessert
/// 2. Floats down screen
/// 3. Destroys off screen
/// </summary>
public class RainingDessert : MonoBehaviour
{
    [SerializeField]
    private float _fallSpeed;
    [SerializeField]
    private DessertImageHandler _imageHandler;

    private float _yMax, _yMin, _spinSpeed;
    private int _maxChanges, _changes;
    private bool _changing, _spin;
    // Start is called before the first frame update
    void Start()
    {
        _imageHandler.RandomChangeDessert();
        _changes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation
        Vector3 rotation = new Vector3();   //Rotation
        rotation.z = _spinSpeed * Time.deltaTime;
        if(_spin)transform.Rotate(rotation);

        //Transform
        transform.position += new Vector3(0, -_fallSpeed * Time.deltaTime, 0);
        float yPos = transform.localPosition.y;
        if (_fallSpeed == 0 || yPos <= _yMin)
        {
            Destroy(gameObject);
        }
        else if (_changing && _changes < _maxChanges && (_yMax - yPos) > ((_changes + 1) * ((_yMax - _yMin)/_maxChanges)))
        {
            _imageHandler.RandomChangeDessert();
            _changes++;
        }
    }
    public void SetValues(bool spin, float spinSpeed, bool changing, int changes, float yMax, float yMin)
    {
        _spin = spin;
        _spinSpeed = spinSpeed;
        _changing = changing;
        _maxChanges = changes;
        _yMax = yMax;
        _yMin = yMin;
    }
}