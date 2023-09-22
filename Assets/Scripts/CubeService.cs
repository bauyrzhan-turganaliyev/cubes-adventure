using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class CubeService : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private CubeMovement _cubeMovement;
    [SerializeField] private CubeEatService _cubeEatService;
    
    public Dictionary<int, Cube> Cubes;

    public void Init()
    {
        Cubes = new  Dictionary<int, Cube>();

        _cubeSpawner.Init();
        _cubeMovement.Init();
    }

    private void RemoveCubeByIndex(int id)
    {
        foreach (var pair in Cubes)
        {
            print($"Index: {pair.Key} | Cube ID: {pair.Value.GetID()} | To remove {id}");
        }
        Cubes.Remove(id);
        print($"------------------------------------------------------------------------");
        foreach (var pair in Cubes)
        {
            print($"Index: {pair.Key} | Cube ID: {pair.Value.GetID()} | To remove {id}");
        }
    }

    public void Spawn()
    {
        Cubes = _cubeSpawner.SpawnCubes();

        foreach (var pair in Cubes)
        {
            pair.Value.OnDestroyed += RemoveCubeByIndex;
            pair.Value.OnAte += (id) => _cubeEatService.SetTarget(Cubes[id]);
        }
        
        _cubeMovement.SetCubes(Cubes);
    }

    public void Move()
    {
        _cubeMovement.Move();
    }

    public void Eat()
    {
        _cubeEatService.SetCubes(Cubes);
        var cube = GetRandomCube();
        cube.Grow();
        _cubeEatService.SetTarget(cube);
    }

    public Cube GetRandomCube()
    {
        Random rand = new Random();
        return Cubes.ElementAt(rand.Next(0, Cubes.Count)).Value;
    }
}