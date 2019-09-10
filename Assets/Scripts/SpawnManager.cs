using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private GameObject[] powerUpList;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnPowers());
    }

    public void StartSpawnRoutines()
    {
        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnPowers());
    }

    private IEnumerator SpawnWaves()
    {
        while (!_gameManager.gameOver)
        {
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(enemyPrefab, new Vector3(Random.Range(-9.8f, 9.73f), 8f, 0), spawnRotation);
            // Wait for 2 seconds
            yield return new WaitForSeconds(2);
        }
    }

    private IEnumerator SpawnPowers()
    {
        while (!_gameManager.gameOver)
        {
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(powerUpList[Random.Range(0, 3)], new Vector3(Random.Range(-9.8f, 9.73f), 8f, 0),
                spawnRotation);
            // Wait for 10 seconds
            yield return new WaitForSeconds(10);
        }
    }
}