using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum AxisRotate
    {
        X, Y, Z
    }

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnBulletPoint;
    [SerializeField] private Transform _enemy;
    [SerializeField] private Transform _eye;
    [SerializeField] private Transform _head;
    [SerializeField] private LayerMask _eyeMask;
    private Rigidbody _rb;
    private Animator _anim;
    public float speed = 2;
    public float jumpForce = 10;
    public float bulletSpeed = 2;
    public float speedRotation = 20f;
    public float timeCooldawn = 5f;

    public AxisRotate axis;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotation = 0;

    [HideInInspector] public float damage;
    private Vector3 _direction;
    private bool _isFire;
    private bool _isSprint;
    private bool _isCooldown;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _direction = Vector3.zero;
        damage = 4;
        _isCooldown = false;
        Rigidbody body = _head.GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isCooldown && _direction == Vector3.zero)
            _isFire = true;

        if (Input.GetButtonDown("Jump"))
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        _isSprint = Input.GetButton("Sprint");

        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (_isFire)
        {
            _isCooldown = true;
            _isFire = false;
            _anim.SetTrigger("Attack");
        }

        Move();
        Rotate();
        Show();
        HeadRotate();
        //transform.Rotate(0, , 0);
    }

    private void Move()
    {
        Vector3 direction = _direction.normalized * ((_isSprint) ? speed * 2 : speed) * Time.fixedDeltaTime;
        //transform.Translate(direction);
        direction = transform.TransformDirection(direction);
        _rb.MovePosition(transform.position + direction);
        if (_direction != Vector3.zero)
            _anim.SetBool("Move", true);
        else
            _anim.SetBool("Move", false);
    }

    private void Rotate()
    {
        float y = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * speedRotation;
        Quaternion newRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + y, transform.eulerAngles.z);
        _rb.MoveRotation(newRotation);
    }

    private void HeadRotate()
    {
        float input = ClampInputMouseAxis(Input.GetAxis("Mouse Y")) * sensitivityVert;

        _rotation = axis switch
        {
            AxisRotate.X => _head.localEulerAngles.x - input,
            AxisRotate.Z => _head.localEulerAngles.z + input,
            _ => 0
        };

        _rotation = ClampRotation(_rotation, sensitivityVert, minimumVert, maximumVert);

        _head.localEulerAngles = axis switch
        {
            AxisRotate.X => new Vector3(_rotation, _head.localEulerAngles.y, _head.localEulerAngles.z),
            AxisRotate.Z => new Vector3(_head.localEulerAngles.x, _head.localEulerAngles.y, _rotation),
            _ => Vector3.zero
        };
    }

    private float ClampRotation(float value, float sensitivity, float min, float max)
    {
        if (value > max + 1 && value < 360 + min - sensitivity - 1)
            value = max;
        if (value < 360 + min - 1 && value > max + sensitivity + 1)
            value = 360 + min;
        return value;
    }

    private float ClampInputMouseAxis(float input) =>
        (input > 1) ? 1 : (input < -1) ? -1 : input;

    private void Fire()
    {
        GameObject bulletObject = Instantiate(_bulletPrefab, _spawnBulletPoint.position, _spawnBulletPoint.rotation);
        Bullet bullet = bulletObject.transform.gameObject.GetComponent<Bullet>();
        bullet.Initialization(damage, 30f, bulletSpeed, _enemy);
        Invoke("Cooldown", timeCooldawn);
    }

    private void Show()
    {
        if (Physics.Raycast(_eye.position, _eye.forward, out RaycastHit hit, 20f, _eyeMask))
        {
            //Debug.Log(hit.point);
            Debug.DrawRay(_eye.position, _eye.forward * hit.distance, Color.blue, Time.deltaTime);
        }
    }

    private void Cooldown()
    {
        _isCooldown = false;
    }
}
