using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DessertImageHandler : MonoBehaviour
{
    [SerializeField]
    private Texture2D[] _desserts;
    [SerializeField]
    RawImage image;
    public void RandomChangeDessert()
    {
        int dessertsLength = _desserts.Length; // 25
        int randomIndex = Random.Range(0, dessertsLength); //0 to 24
        ChangeDessert(randomIndex);
    }

    public void ChangeDessert(int index)
    {
        image.texture = _desserts[index];
    }
}
