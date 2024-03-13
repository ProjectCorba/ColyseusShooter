
using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _cameraPoint;
    [SerializeField] private float _minHeadAngle = -90;
    [SerializeField] private float _maxHeadAngle = 90;
    [SerializeField] private float _jumpForce = 50f;
    [SerializeField] private CheckFly _checkFly;
    [SerializeField] private float _jumpDelay = .2f;
    private float _inputH;
    private float _inputV;
    private float _rotateY;
    private float _correntRotateX;
    private float _jumpTime;

    private void Start()
    {
        Transform camera = Camera.main.transform;
        camera.parent = _cameraPoint;
        camera.localPosition = Vector3.zero; 
        camera.localRotation = Quaternion.identity;
    }


    public void SetInput(float h, float v, float rotateY)
    {
        _inputH = h;
        _inputV = v;
        _rotateY += rotateY;
    }
    private void FixedUpdate()
    {
        Move();
        RotateY();
    }

    private void Move()
    {
        //Vector3 direction = new Vector3(_inputH, 0, _inputV).normalized;
        //transform.position += direction * Time.deltaTime * _speed;

        Vector3 velocity = (transform.forward * _inputV + transform.right * _inputH).normalized * Speed;
        velocity.y = _rigidbody.velocity.y;
        base.Velocity = velocity;
        _rigidbody.velocity = base.Velocity;
    }

    private void RotateY()
    {
        _rigidbody.angularVelocity = new Vector3(0,_rotateY,0);
        _rotateY = 0;
    }

    public void RotateX(float value)
    {
        _correntRotateX = Mathf.Clamp(_correntRotateX + value, _minHeadAngle, _maxHeadAngle);
        _head.localEulerAngles = new Vector3(_correntRotateX, 0, 0);
        //Debug.Log(_head.localEulerAngles);
    }

    public void GetMoveInfo(out Vector3 position, out Vector3 velocity, out float rotateX, out float rotateY)
    {
        position = transform.position;
        velocity = _rigidbody.velocity;

        rotateY = transform.localEulerAngles.y;
        rotateX = _head.transform.localEulerAngles.x;
    }

    public void Jump()
    {
        if (_checkFly.IsFly) return;
        if (Time.time - _jumpTime < _jumpDelay) return;


        _jumpTime = Time.time;
        _rigidbody.AddForce(0, _jumpForce, 0, ForceMode.VelocityChange);
    }
}
