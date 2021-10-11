using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private float _speed;
    private float _damage;
    private Transform _target;
    private Rigidbody _rb;
    private Vector3 _targetPosition;

    public void Initialization(float damage, float lifeTime, float speed, Transform target)
    {
        _rb = GetComponent<Rigidbody>();

        _speed = speed;
        _target = target;
        _targetPosition = _target.position;
        _damage = damage;
        Destroy(this.gameObject, lifeTime);

        _rb.AddForce(transform.forward * _speed);
        _rb.AddTorque(transform.right * 100f);
    }

    private void FixedUpdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, _targetPosition + Vector3.up, _speed * Time.fixedDeltaTime);
    }
}
