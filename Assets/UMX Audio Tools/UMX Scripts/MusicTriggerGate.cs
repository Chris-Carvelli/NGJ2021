using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicTriggerGate : MonoBehaviour
{
    public AudioMixerSnapshot transitionSnap;

    public float transitionTime = 0.5f;
    

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
        if (other.CompareTag("Player"))
        {
            transitionSnap.TransitionTo(transitionTime);
        }
    }
}

