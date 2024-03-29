
using System;
using UnityEngine;

public class PlayerGun : Gun
{
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootDelay;
    [SerializeField] private int _damage;
    private float _lastShootTime;
    
    public bool TryShoot(out ShootInfo info)
    {
        info = new ShootInfo();
        if (Time.time - _lastShootTime < _shootDelay) return false;

        Vector3 position = _bulletPoint.position;
        Vector3 velocity = _bulletPoint.forward * _bulletSpeed;

        _lastShootTime = Time.time;
        Instantiate( _bullet, position, _bulletPoint.rotation ).Init(velocity, _damage);
        shoot?.Invoke();

        info.pX = position.x;
        info.pY = position.y;
        info.pZ = position.z;
        info.dX = velocity.x;
        info.dY = velocity.y;
        info.dZ = velocity.z;

        return true;
    }
}
