using UnityEngine;

public class ShootService : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletParent;
    
    public void ShootBullet(Vector3 startPoint, Vector3 targetPoint)
    {
        // Instantiate a new bullet at the desired position and rotation
        var newBullet = Instantiate(_bulletPrefab, startPoint, Quaternion.identity);
        newBullet.transform.parent = _bulletParent;
        
        // Access the Bullet script on the new bullet to set any specific parameters
        Bullet bulletComponent = newBullet.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.SetLookAtTarget(targetPoint);
            // Set bullet parameters if needed
        }
    }
}