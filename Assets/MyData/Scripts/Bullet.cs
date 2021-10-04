using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;
    private float _damage;
    private Transform _target;
    private Vector3 _targetPosition;

    public void Initialization(float damage, float lifeTime, float speed, Transform target)
    {
        _speed = speed;
        _target = target;
        _targetPosition = _target.position;
        _damage = damage;
        Destroy(this.gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition + Vector3.up, _speed * Time.fixedDeltaTime);
    }
}
