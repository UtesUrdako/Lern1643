using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;

    public void Initialization(float damage, float lifeTime)
    {
        _damage = damage;
        Destroy(this.gameObject, lifeTime);
    }
}
