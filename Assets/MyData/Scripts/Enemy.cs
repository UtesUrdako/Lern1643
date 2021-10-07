using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Player player;
    public Transform head;
    public float speedRotationHead = 20f;
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    private int m_CurrentWaypointIndex;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }

        Vector3 direction = player.transform.position - head.position;
        Vector3 forwardDirection = new Vector3(direction.x, 0, direction.z);
        Vector3 stepDirection = Vector3.RotateTowards(head.forward, forwardDirection, speedRotationHead * Time.fixedDeltaTime, 0.0f);
        head.rotation = Quaternion.LookRotation(stepDirection);
    }
}
