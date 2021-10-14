using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnBulletPoint;
    [SerializeField] private Transform _enemy;
    [SerializeField] private Transform _eye;
    [SerializeField] private Rigidbody _head;
    [SerializeField] private LayerMask _eyeMask;
    private Rigidbody _rb;
    private Animator _anim;
    public float speed = 2;
    public float jumpForce = 10;
    public float bulletSpeed = 2;
    public float speedRotation = 20f;
    public float timeCooldawn = 5f;

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
            Debug.Log(hit.point);
            Debug.DrawRay(_eye.position, _eye.forward * hit.distance, Color.blue, Time.deltaTime);
        }
    }

    private void Cooldown()
    {
        _isCooldown = false;
    }
}
