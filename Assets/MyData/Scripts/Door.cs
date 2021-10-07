using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Transform door;
    public bool isOpen;
    public float speedRotationHead = 2f;

    private Transform _player;

    private void Update()
    {
        if (_player != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = !isOpen;

                
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOpen)
        {
            Vector3 stepDirection = Vector3.RotateTowards(door.forward, Vector3.right, speedRotationHead * Time.fixedDeltaTime, 0.0f);
            door.rotation = Quaternion.LookRotation(stepDirection);
        }
        else
        {
            Vector3 stepDirection = Vector3.RotateTowards(door.forward, Vector3.forward, speedRotationHead * Time.fixedDeltaTime, 0.0f);
            door.rotation = Quaternion.LookRotation(stepDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.transform;
            //Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = null;
        }
    }
}
