using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnBulletPoint;
    [SerializeField] private Transform _enemy;
    public float speed = 2;
    public float bulletSpeed = 2;
    public float speedRotation = 20f;

    [HideInInspector] public float damage;
    private Vector3 _direction;
    private bool _isFire;
    private bool _isSprint;

    private void Awake()
    {
        _direction = Vector3.zero;
        damage = 4;
        Debug.Log(transform.GetChild(2).name);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _isFire = true;

        _isSprint = Input.GetButton("Sprint");

        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (_isFire)
        {
            _isFire = false;
            Fire();
        }

        Move();

        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.fixedDeltaTime * speedRotation, 0);
    }

    private void Move()
    {
        Vector3 direction = _direction.normalized * ((_isSprint) ? speed * 2 : speed) * Time.fixedDeltaTime;
        //transform.Translate(direction);
        transform.position += direction;
    }

    private void Fire()
    {
        GameObject bulletObject = Instantiate(_bulletPrefab, _spawnBulletPoint.position, _spawnBulletPoint.rotation);
        Bullet bullet = bulletObject.transform.gameObject.GetComponent<Bullet>();
        bullet.Initialization(damage, 30f, bulletSpeed, _enemy);
    }
}
