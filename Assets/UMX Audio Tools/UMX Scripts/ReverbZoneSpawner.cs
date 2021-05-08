using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbZoneSpawner : MonoBehaviour
{

    private AudioReverbZone reverbZone;

    public float reverbOffDelay = 4f;


    // Start is called before the first frame update
    void Start()
    {
        reverbZone = GetComponent<AudioReverbZone>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(("Player")))
        {
            reverbZone.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(("Player")))
        {
            Invoke("TurnOffReverb", reverbOffDelay);
        }
    }

    private void TurnOffReverb()
    {
        reverbZone.enabled = false;
    }
}
