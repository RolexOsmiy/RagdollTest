using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject birdPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnCooldown = 3f;

    private float lastSpawnTime;

    private void Start()
    {
        lastSpawnTime = Time.time;
        InvokeRepeating("SpawnBird",0,spawnCooldown);
    }

    private void SpawnBird()
    {
        GameObject bullet = ObjectPool.Instance.GetObject("Bird", birdPrefab);

        if (bullet != null)
        {
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = spawnPoint.rotation;

            bullet.SetActive(true);
        }
    }
}
