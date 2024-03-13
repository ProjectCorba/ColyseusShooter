using UnityEngine;

public class EnemyGun : Gun
{
    public void Shoot(Vector3 position, Vector3 velocity)
    {
        Instantiate(_bullet, position, Quaternion.identity).Init(velocity);
        shoot?.Invoke();
    }
}
