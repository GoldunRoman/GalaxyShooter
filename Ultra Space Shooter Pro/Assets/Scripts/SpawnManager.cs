using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Debug Tools")]
    [SerializeField]
    private bool _stopSpawning = false;

    [Header("Prefabs to spawn")]
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _asteroidPrefab;


    [Header("Other stuff")]
    [SerializeField]
    private float _courutineDelay = 5f;
    [SerializeField]
    private GameObject _enemyContainer;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine(_courutineDelay));
        StartCoroutine(SpawnPowerupsRoutine());
    }

    IEnumerator SpawnEnemyRoutine(float delay)
    {       
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false) 
        {
            float spawnX = Random.Range(-10, 10);
            float spawnY = 12f;

            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform; 

            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator SpawnPowerupsRoutine()
    {
        yield return new WaitForSeconds(8f);

        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f));

            float spawnX = Random.Range(-10, 10);
            float spawnY = 6.5f;

            int randomPowerup = Random.Range(0, 3); 
            GameObject newPowerup = Instantiate(powerups[randomPowerup], new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
            newPowerup.transform.parent = _enemyContainer.transform;

        }
    }


    public void StopSpawning()
    {
        _stopSpawning = true;
    }
}
