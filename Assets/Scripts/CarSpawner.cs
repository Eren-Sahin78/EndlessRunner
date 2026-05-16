using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] GameObject[] carPrefabs; // üretilecek olan arabalar.

    //Minimum ve maksimum üretme aralýðý
    [SerializeField] private float minSpawnTime = 3f;
    [SerializeField] float maxSpawnTime = 5f;

    // Start is called before the first frame update
    void Start()

      
    {


        StartCoroutine(routine: SpawnCars());

    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            // Rast gele bir süre bekletmeliyiz
            float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomTime);

            //Rast gele bir referans noktasý seçelim
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            //Arabayý üretmek
            Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);

        }
    }
}