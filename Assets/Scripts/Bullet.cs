using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _liveTime = 5f;
    [SerializeField] private Rigidbody _rb;
    public void Init(Vector3 velocity)
    {
        _rb.velocity = velocity;

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
        Destroy();
    }
}
