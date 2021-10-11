using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestScropt : MonoBehaviour
{
    public UnityEvent OnStart;
    private void Start()
    {
        for (int i = 0; i < 10000; i++)
        {
            var obj = new GameObject();
            obj.AddComponent<Rotator>();
        }

        //OnStart.Invoke();
        //StartCoroutine(TestRandom(5));
        //StopCoroutine(TestRandom(5));
    }

    private IEnumerator TestRandom(int newAngle)
    {
        Debug.Log("Start coroutine");
        for (int i = 0; i < 5; i++)
        {
            Random.seed = 0;
            for (int j = 0; j < 5; j++)
            {
                Debug.Log(Random.Range(1, 101));
                yield return new WaitForSeconds(0.01f);
            }
            Debug.Log("_________________");
        }
        Debug.Log("_________________");
        for (int i = 0; i < 5; i++)
        {
            Random.seed = (int)Time.time;
            for (int j = 0; j < 5; j++)
            {
                Debug.Log(Random.Range(1, 101));
                yield return new WaitForSeconds(2);
            }
            Debug.Log("_________________");
        }
        Debug.Log("Exit coroutine");
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.localScale = Vector3.one * 0.1f;
        go.transform.position = collision.GetContact(0).point;
        Destroy(go.GetComponent<SphereCollider>());
    }

    private void OnCollisionStay(Collision collision)
    {
        List<GameObject> walls = new List<GameObject>();
        Instantiate(walls[Random.Range(0, walls.Count)]);
        var percent = Random.value;
        if (percent > 0.75f)
            Debug.Log("Crit!");
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}
