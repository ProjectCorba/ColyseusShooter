using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _inputH;
    private float _inputV;

    public void SetInput(float h, float v)
    {
        _inputH = h;
        _inputV = v;
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(_inputH, 0, _inputV).normalized;
        transform.position += direction * Time.deltaTime * _speed;
    }
}
