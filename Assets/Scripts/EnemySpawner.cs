using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject Enemy;
    public float spawnInterval = 2.0f; // Set in Inspector

    void Start()
    {
        StartCoroutine(SpawnEnemies());     // start the SpawnEnemies coroutine when the game starts
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);      // choose a random spawn point from the spawnPoints array
            Instantiate(Enemy, spawnPoints[randSpawnPoint].position, Quaternion.identity);   // spawn an enemy at the chosen spawn point with a random enemy prefab
            yield return new WaitForSeconds(spawnInterval);               // wait for the specified spawn interval before spawning another enemy
        }
    }
}