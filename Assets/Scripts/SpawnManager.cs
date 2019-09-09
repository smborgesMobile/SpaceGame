using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private GameObject[] powerUpList;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        while (true)
        {
//            // Wait for five seconds
//            yield return new WaitForSeconds(5.0f);
        }
    }
    
    // method to exeu
    IEnumerator Fade() 
    {
    }
}