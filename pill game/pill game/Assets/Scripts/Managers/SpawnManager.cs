using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{

    
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private GameObject _zombie;

    //todo change spawnrate to match with time
    [SerializeField] private float _spawnRate = 3.0f;


    private void Awake()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy(_zombie, _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform);
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    void SpawnEnemy(GameObject type, Transform place)
    {
        var Enemy = Instantiate(type, place.transform.position, Quaternion.identity);
    }
    
}        
