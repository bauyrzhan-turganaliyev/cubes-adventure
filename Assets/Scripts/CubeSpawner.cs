using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    public Cube cubePrefab; // Префаб куба
    public Transform spawnZone; // Зона спавна
    public Transform parent;

    public int numberOfCubes = 10; // Количество кубов, которые нужно создать

    public Action<int> OnCubeDestroys;
    public void Init()
    {
    }
    public Dictionary<int, Cube> SpawnCubes()
    {
        var cubes = new Dictionary<int, Cube>();
        for (int i = 0; i < numberOfCubes; i++)
        {
            // Создаем куб из префаба
            var cube = Instantiate(cubePrefab, parent);
            cube.SetNumber(i);

            // Устанавливаем позицию куба в случайном месте в зоне спавна
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnZone.position.x - spawnZone.localScale.x / 2, spawnZone.position.x + spawnZone.localScale.x / 2),
                Random.Range(1, 3),
                Random.Range(spawnZone.position.z - spawnZone.localScale.z / 2, spawnZone.position.z + spawnZone.localScale.z / 2)
            );

            cube.transform.position = spawnPosition;
            
            cubes.Add(i, cube);
        }

        return cubes;
    }


}
