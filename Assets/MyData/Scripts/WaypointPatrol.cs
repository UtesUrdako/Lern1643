using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace My
{
    public class WaypointPatrol : MonoBehaviour
    {
        [SerializeField] private Transform _eye;
        [SerializeField] private LayerMask _eyeMask;
        private float hitDistanse;
        [SerializeField] private bool _patrule;
        public NavMeshAgent navMeshAgent;
        public Transform[] waypoints;
        int m_CurrentWaypointIndex;

        private Transform _pursuitPoint;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            _patrule = true;
        }
        void Start()
        {
            navMeshAgent.SetDestination(waypoints[0].position);
        }

        void FixedUpdate()
        {
            checkPlayer();

            if (_patrule)
                patrule();
            else
                Pursuit();
        }
        void checkPlayer()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up, 50f, _eyeMask, QueryTriggerInteraction.Ignore);
            
            foreach (Collider collider in colliders)
                if (collider.tag == "Player")
                {
                    var direction = collider.transform.position - transform.position;

                    if (Physics.Raycast(transform.position + Vector3.up, direction + Vector3.up, out RaycastHit hit, 50f, _eyeMask, QueryTriggerInteraction.Ignore))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            _pursuitPoint = collider.transform;
                            _patrule = false;
                            return;
                        }
                        else
                        {
                            _patrule = true;
                        }
                        break;
                    }
                }

            _patrule = true;
        }

        void patrule()
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
        }

        private void Pursuit()
        {
            navMeshAgent.SetDestination(_pursuitPoint.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + transform.up * 0.9f + transform.forward * hitDistanse, 0.75f);
        }
    }
}