using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config", order = 1)]
public class GameConfig : ScriptableObject
{
    [Header("Cube Settings")] 
    [Range(1, 10)]
    public int SpawnCount;
    
    [Range(1, 5)]
    public int CubeSpeed;


    [Header("Shoot Settings")] 
    public bool AutoShoot;

    [Range(10, 30)]
    public int BulletSpeed;
    
    [Range(1, 5)]
    public int BulletLifetime;
}