using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public Transform point;
    public float spawnTime = 2.5f;
    public GameObject[] rubbish;

    void Start()
    {
        InvokeRepeating("SpawnRubbish", 0, spawnTime);
    }

    void SpawnRubbish()
    {
        int rubbishIndex = Random.Range(0, rubbish.Length);
        Instantiate(rubbish[rubbishIndex], point.position, Quaternion.identity);
    }
}
