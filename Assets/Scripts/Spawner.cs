using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _rainingDessertPrefab;
    [SerializeField]
    private float _rainingDessertxMin, _rainingDessertxMax, _rainingDessertyMax, _rainingDessertyMin;
    [SerializeField]
    private bool _spin;
    [SerializeField]
    private float _spinSpeed;
    [SerializeField]
    private int _changes;
    [SerializeField]
    private bool _changing;
    [SerializeField]
    private float _spawnDelay;
    [Range(0f, 1f)]
    [SerializeField]
    private float _spawnChance;
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _spawnDelay)
        {
            _timer = 0;
            if (Random.value <= _spawnChance)SpawnRainingDessertPrefab();
        }
        //My Pendulum Code
        //if (pendulum & Mathf.Sin(Time.time * _muffinFrequency) < 0)
        //{
        //    SpawnRainingDessertPrefab();
        //    pendulum = false;
        //}
        //if (!pendulum & Mathf.Sin(Time.time * _muffinFrequency) > 0)
        //{
        //    SpawnRainingDessertPrefab();
        //    pendulum = true;
        //}
    }

    public void SpawnRainingDessertPrefab()
    {
        GameObject rainingDessertClone = Instantiate(_rainingDessertPrefab, transform);//Clone
        Vector2 randomVector = GetRandomVector2(_rainingDessertxMin, _rainingDessertxMax, _rainingDessertyMax, _rainingDessertyMax);//RandomPosition
        rainingDessertClone.transform.localPosition = randomVector;
        RainingDessert RDH = rainingDessertClone.GetComponent<RainingDessert>();
        RDH.SetValues(_spin, _spinSpeed, _changing, _changes, _rainingDessertyMax, _rainingDessertyMin);
        //Get TMP Component - try to avoid getcomponent
        //TextMeshProUGUI textRewardPrefab = textRewardClone.GetComponent<TextMeshProUGUI>();
        //rainingDessertClone.GetComponent<RainingDessertHandler>().SetChangingDessert(true);//this is the only way I know how to do this
    }
    public static Vector2 GetRandomVector2(float xMin, float xMax, float yMin, float yMax)
    {
        Vector2 value = new Vector2();
        value.x = Random.Range(xMin, xMax);
        value.y = Random.Range(yMin, yMax);
        return value;
    }
}
