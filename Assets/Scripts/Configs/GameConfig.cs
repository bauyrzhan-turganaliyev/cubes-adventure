using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config", order = 1)]
    public class GameConfig : ScriptableObject
    {
        [Header("General Settings")] 
        public bool IsMoveWhileShoot;
        
        [Header("Cube Settings")]
        public int MinSpawnCount;
        public int MaxSpawnCount;
    
        [Range(1, 5)]
        public int CubeSpeed;
    
        [Header("Shoot Settings")] 
        public bool AutoShoot;

        [Range(10, 30)]
        public int BulletSpeed;
    
        [Range(1, 5)]
        public int BulletLifetime;
    }
}