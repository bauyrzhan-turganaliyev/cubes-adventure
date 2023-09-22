using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab; // Префаб куба
    public Transform spawnZone; // Зона спавна
    public Transform parent;

    public int numberOfCubes = 10; // Количество кубов, которые нужно создать

    void Start()
    {
        SpawnCubes();
    }

    void SpawnCubes()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            // Создаем куб из префаба
            GameObject cube = Instantiate(cubePrefab, parent);

            // Устанавливаем позицию куба в случайном месте в зоне спавна
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnZone.position.x - spawnZone.localScale.x / 2, spawnZone.position.x + spawnZone.localScale.x / 2),
                Random.Range(1, 3),
                Random.Range(spawnZone.position.z - spawnZone.localScale.z / 2, spawnZone.position.z + spawnZone.localScale.z / 2)
            );

            cube.transform.position = spawnPosition;
        }
    }
}
