using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource musicSource;

    public AudioClip startMusic;

    public AudioClip transitionMusic;

    public AudioClip musicStopStinger;

    public static MusicManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        musicSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.clip = startMusic;
            musicSource.Play();
            musicSource.loop = true;
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
            musicSource.clip = musicStopStinger;
            musicSource.Play();
            musicSource.loop = false;
        }
    }

    public void TransitionMusic()
    {
        musicSource.clip = transitionMusic;
        musicSource.Play();
        musicSource.loop = true;
    }
}
