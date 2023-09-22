using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class CubeService : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private CubeMovement _cubeMovement;
    public Dictionary<int, Cube> Cubes;

    public void Init()
    {
        Cubes = new  Dictionary<int, Cube>();

        _cubeSpawner.OnCubeDestroys += RemoveCubeByIndex;
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
        _cubeMovement.PrepareToMove(Cubes);
    }

    public void Move()
    {
        _cubeMovement.Move();
    }

    public Vector3 GetRandomCube()
    {
        Random rand = new Random();
        return Cubes.ElementAt(rand.Next(0, Cubes.Count)).Value.gameObject.transform.position;
    }
}