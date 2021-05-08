using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGateStingerHandler : MonoBehaviour
{

    private AudioSource audioSource;

    public AudioClip enterStinger;
    public AudioClip exitStinger;

    private bool enterStingerReady = false;
    private bool exitStingerReady = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        enterStingerReady = !enterStingerReady;
        exitStingerReady = !exitStingerReady;

        if (other.CompareTag("Player") && enterStingerReady == true && exitStingerReady == false)
        {
            audioSource.PlayOneShot(enterStinger);
        }

        if (other.CompareTag("Player") && enterStingerReady == false && exitStingerReady == true)
        {
            audioSource.PlayOneShot(exitStinger);
        }
    }
}
