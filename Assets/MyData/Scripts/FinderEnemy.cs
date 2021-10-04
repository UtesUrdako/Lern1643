using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderEnemy : MonoBehaviour
{
    public List<Transform> enemys;

    private void Awake()
    {
        enemys = new List<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemys.Contains(other.transform))
                enemys.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (enemys.Contains(other.transform))
                enemys.Remove(other.transform);
        }
    }
}
