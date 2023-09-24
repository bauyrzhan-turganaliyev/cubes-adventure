using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actions.CubeActions
{
    public class CubeEatService : MonoBehaviour
    {
        public Action<int> OnAteCube;
        public Action OnStopEating;
    
        private Dictionary<int, Cube> _cubes;
        private Cube _eaterCube;

        public void SetCubes(Dictionary<int, Cube> cubes)
        {
            _cubes = cubes;
        }
        public void SetEaterCube(Cube cube)
        {
            _eaterCube = cube;
            _eaterCube.OnAte += SetTarget;
            _eaterCube.SwitchGrow(true);
        }
        public void StopEating()
        {
            if (_eaterCube == null) return;
            
            _eaterCube.OnAte = null;
            _eaterCube.SwitchGrow(false);
            _eaterCube.SetTarget(null);
            _eaterCube = null;
        }
        public void SetTarget(Cube ateCube = null)
        {
            if (ateCube != null)
                OnAteCube?.Invoke(ateCube.GetID());
        
            var targetCube = FindClosestCubeTo();
            if (targetCube == null)
            {
                _eaterCube.SetTarget(null);
                _eaterCube.Stop();
                OnStopEating?.Invoke();
                return;
            }
            
            _eaterCube.SetTarget(targetCube);
        }
        
        private Cube FindClosestCubeTo()
        {
            Cube closestCube = null;
            float closestDistance = float.MaxValue;
            foreach (var pair in _cubes)
            {
                if (pair.Value.transform != _eaterCube.transform)
                {
                    float distance = Vector3.Distance(_eaterCube.transform.position, pair.Value.transform.position);
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
}