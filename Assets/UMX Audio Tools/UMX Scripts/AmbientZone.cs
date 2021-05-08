using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientZone : MonoBehaviour
{
    [SerializeField]
    private float fadeInTime = 3f;

    [SerializeField]
    private float fadeOutTime = 3f;

    private AudioSource fadeSource;

    public float fadeInTarget = 1f;
    public float fadeOutTarget = 0f;

    private bool InsideTrigger = false;


    private void Start()
    {
        fadeSource = GetComponent<AudioSource>();
        fadeSource.volume = 0f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!fadeSource.isPlaying)
            {
                fadeSource.Play();
            }
            InsideTrigger = true;
            Debug.Log("Player Inside");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeSource.isPlaying)
            {
                
            }
            InsideTrigger = false;
            Debug.Log("Player Outside");
        }
    }


    private void Update()
    {
        if (InsideTrigger == true)
        {
            if (fadeSource.volume < fadeInTarget)
            {
                fadeSource.volume = fadeSource.volume + (Time.deltaTime / (fadeInTime + 1));
            }
        }

        else if (InsideTrigger == false)
        {
            fadeSource.volume = fadeSource.volume - (Time.deltaTime / (fadeOutTime + 1));
        }
    }

  
}
