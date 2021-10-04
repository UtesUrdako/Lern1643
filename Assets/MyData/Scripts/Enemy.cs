using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public Transform head;
    public float speedRotationHead = 20f;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = player.transform.position - head.position;
        Vector3 forwardDirection = new Vector3(direction.x, 0, direction.z);
        Vector3 stepDirection = Vector3.RotateTowards(head.forward, forwardDirection, speedRotationHead * Time.fixedDeltaTime, 0.0f);
        head.rotation = Quaternion.LookRotation(stepDirection);
    }
}
