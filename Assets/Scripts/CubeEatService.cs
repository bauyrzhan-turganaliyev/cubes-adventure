using System.Collections.Generic;
using UnityEngine;

public class CubeEatService : MonoBehaviour
{
    private Dictionary<int, Cube> _cubes;
    public void SetCubes(Dictionary<int, Cube> cubes)
    {
        _cubes = cubes;
    }            
    public void SetTarget(Cube cube)
    {
        var a = FindClosestCube(cube.transform);
        cube.SetTarget(a);
    }
    private Cube FindClosestCube(Transform referenceCube)
    {
        Cube closestCube = null;
        float closestDistance = float.MaxValue;
        foreach (var pair in _cubes)
        {
            if (pair.Value.transform != referenceCube)
            {
                float distance = Vector3.Distance(referenceCube.position, pair.Value.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCube = pair.Value;
                }
            }
        }

        return closestCube;
    }



}