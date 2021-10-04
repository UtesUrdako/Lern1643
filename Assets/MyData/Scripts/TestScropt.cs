using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScropt : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.localScale = Vector3.one * 0.1f;
        go.transform.position = collision.GetContact(0).point;
        Destroy(go.GetComponent<SphereCollider>());
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}
