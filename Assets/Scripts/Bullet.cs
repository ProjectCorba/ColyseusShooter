using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _liveTime = 5f;
    [SerializeField] private Rigidbody _rb;
    private int _damage;
    public void Init(Vector3 velocity, int damage = 0)
    {
        _rb.velocity = velocity;
        _damage = damage;

        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSecondsRealtime(_liveTime);
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out EnemyCharacter enemy))
        {
            enemy.ApplyDamage(_damage);
        }
        Destroy();
    }
}
