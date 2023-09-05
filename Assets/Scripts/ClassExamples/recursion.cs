using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recursion : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] int N;
    [SerializeField] private float _delay;
    private int i;
    [SerializeField] GameObject Object;
    void Start()
    {
        StartCoroutine(MakeObject(Object));
    }

    IEnumerator MakeObject(GameObject prevObject)
    {
        if (i < N)
        {
            float newY = prevObject.transform.position.y + prevObject.transform.localScale.y / 2;

            GameObject currObject = Instantiate(prevObject);
            currObject.transform.localScale = prevObject.transform.localScale * .5f;
            currObject.transform.position = new Vector3(0, newY, 0);

            currObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();

            yield return new WaitForSeconds(_delay);
            i++;
            StartCoroutine(MakeObject(currObject));
        }
    }
}
