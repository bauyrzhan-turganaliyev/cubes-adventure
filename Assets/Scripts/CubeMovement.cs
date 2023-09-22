using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость перемещения кубов
    private Dictionary<int, Cube> _cubes;
    
    public void Init()
    {
        _cubes = new Dictionary<int, Cube>();
    }
    
    public void PrepareToMove(Dictionary<int, Cube> cubes)
    {
        _cubes = cubes;
    }
    
    public void Move()
    {
        foreach (var cube in _cubes)
        {
            cube.Value.Move(moveSpeed);
        }
    }
}