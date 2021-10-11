using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject _enemyPrefab;
    public float timeSpawn = 3f;
    public int maxCountEnemy = 3;

    private int countEnemy;

    void Start()
    {
        InvokeRepeating("Spawn", timeSpawn * 1.5f, timeSpawn);
    }

    private void Update()
    {
        if (countEnemy == maxCountEnemy)
            CancelInvoke("Spawn");
    }

    private void Spawn()
    {
        var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.Initialization(transform);
        countEnemy++;
    }
}
