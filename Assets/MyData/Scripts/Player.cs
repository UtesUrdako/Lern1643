using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnBulletPoint;
    public float speed = 2;

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

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _isFire = true;

        _isSprint = Input.GetButton("Sprint");

        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");



        //if (Input.GetKey(KeyCode.W))
        //    _direction.z = 1;
        //else if (Input.GetKey(KeyCode.S))
        //    _direction.z = -1;
        //else
        //    _direction.z = 0;

        //if (Input.GetKey(KeyCode.D))
        //    _direction.x = 1;
        //else if (Input.GetKey(KeyCode.A))
        //    _direction.x = -1;
        //else
        //    _direction.x = 0;
    }

    private void FixedUpdate()
    {
        if (_isFire)
        {
            _isFire = false;
            Fire();
        }

        Move();
    }

    private void Move()
    {
        Vector3 direction = _direction * ((_isSprint) ? speed * 2 : speed) * Time.fixedDeltaTime;
        //transform.Translate(direction);
        transform.position += direction;
    }

    private void Fire()
    {
        GameObject bulletObject = Instantiate(_bulletPrefab, _spawnBulletPoint.position, Quaternion.identity);
        Bullet bullet = bulletObject.transform.gameObject.GetComponent<Bullet>();
        bullet.Initialization(damage, 3f);
    }
}
