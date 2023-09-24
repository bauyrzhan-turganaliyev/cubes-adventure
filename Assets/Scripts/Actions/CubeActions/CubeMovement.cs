using System.Collections.Generic;
using UnityEngine;

namespace Actions.CubeActions
{
    public class CubeMovement : MonoBehaviour
    {
        public float MoveSpeed { get; set; }
    
        private Dictionary<int, Cube> _cubes;
    
        public void Init()
        {
            _cubes = new Dictionary<int, Cube>();
        }
    
        public void SetCubes(Dictionary<int, Cube> cubes)
        {
            _cubes = cubes;
        }
    
        public void StartMoving()
        {
            foreach (var cube in _cubes)
            {
                cube.Value.Move(MoveSpeed);
            }
        }

        public void StopMoving()
        {
            foreach (var cube in _cubes)
            {
                cube.Value.Stop();
            }
        }
    }
}