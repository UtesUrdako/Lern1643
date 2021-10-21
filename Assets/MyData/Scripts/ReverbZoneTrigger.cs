using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbZoneTrigger : MonoBehaviour
{
    public AudioReverbZone reverbZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            reverbZone.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            reverbZone.enabled = false;
        }
    }
}
