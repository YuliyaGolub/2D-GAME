using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;
    [SerializeField] private float minSpawnInterval = 1.0f;
    [SerializeField] private float maxSpawnInterval = 10.0f;
    private float nextSpawnTime = 0f;

    private void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = Random.value > 0.5f ? leftSpawnPoint : rightSpawnPoint;
        int enemyIndex = GetRandomPrefabIndex();
        GameObject enemyPrefab = enemyPool.enemyGroups[enemyIndex].enemy.gameObject;
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    private int GetRandomPrefabIndex()
    {
        float totalWeight = enemyPool.enemyGroups.Sum(e => e.weight);
        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;
        for (int i = 0; i < enemyPool.enemyGroups.Count; i++)
        {
            cumulativeWeight += enemyPool.enemyGroups[i].weight;
            if (randomValue <= cumulativeWeight)
                return i;
        }
        return 0;
    }
}
