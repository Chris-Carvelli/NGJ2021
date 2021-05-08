using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFade : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 3f;

    private AudioSource fadeSource;

    public float fadeTarget = 1f;


    private void Start()
    {
        fadeSource = GetComponent<AudioSource>();
        fadeSource.volume = 0f;
    }


    private void Update()
    {
        if (fadeSource.volume < fadeTarget)
        {
            fadeSource.volume = fadeSource.volume + (Time.deltaTime / (fadeTime + 1));
        }

        else
        {
            return;
        }
    }

}
