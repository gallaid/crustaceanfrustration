using System;
using System.Collections.Generic;

using UnityEngine;

class StackableSpawner : MonoBehaviour
{
    public List<GameObject> SpawnableObjects = new List<GameObject>();

    public int _spawnCount = 50;

    private const float _minX = -45;
    private const float _maxX = 45;

    private const float _minZ = -20;
    private const float _maxZ = 25;

    void Start()
    {
        for (int i = 0; i < _spawnCount; i += 1)
        {
            int randomIndex = UnityEngine.Random.Range(0, SpawnableObjects.Count);
            float randomX = UnityEngine.Random.Range(_minX, _maxX);
            float randomZ = UnityEngine.Random.Range(_minZ, _maxZ);
            Instantiate<GameObject>(SpawnableObjects[randomIndex], new Vector3(randomX, 2, randomZ), Quaternion.identity);
        }
    }

    void Update()
    {

    }
}
