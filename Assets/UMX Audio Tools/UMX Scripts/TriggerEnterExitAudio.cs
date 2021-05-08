using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class TriggerEnterExitAudio : MonoBehaviour
{

    [Header("Enter Audio Files")]
    public AudioClip[] enterAudioFiles;
    private AudioSource audioSource;
    [Header("Enter Retriggering")]
    public bool enterRetriggerPrevention = true;
    [Header("Enter Pitch Randomization Amount")]
    public float enterPitchMin = 1f;
    public float enterPitchMax = 1f;
    [Header("Enter Volume Randomization Amount")]
    public float enterVolumeMin = 1f;
    public float enterVolumeMax = 1f;


    [Header("Exit Audio Files")]
    public AudioClip[] exitAudioFiles;
    [Header("Enter Retriggering")]
    public bool exitRetriggerPrevention = true;
    [Header("Enter Pitch Randomization Amount")]
    public float exitPitchMin = 1f;
    public float exitPitchMax = 1f;
    [Header("Enter Volume Randomization Amount")]
    public float exitVolumeMin = 1f;
    public float exitVolumeMax = 1f;




    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void EnterRandomAudio()
    {
        float pitch = Random.Range(enterPitchMin, enterPitchMax);
        audioSource.pitch = pitch;

        float volume = Random.Range(enterVolumeMin, enterVolumeMax);
        audioSource.volume = volume;

        // Chooses a number between 1 and the length of audiofiles array. Moves chosen audioclip to source and plays it once.
        int n = Random.Range(1, enterAudioFiles.Length);
        audioSource.clip = enterAudioFiles[n];
        audioSource.PlayOneShot(audioSource.clip);

        if (enterRetriggerPrevention)
        {
            enterAudioFiles[n] = enterAudioFiles[0];
            enterAudioFiles[0] = audioSource.clip;
        }
    }

    private void ExitRandomAudio()
    {
        float pitch = Random.Range(exitPitchMin, exitPitchMax);
        audioSource.pitch = pitch;

        float volume = Random.Range(exitVolumeMin, exitVolumeMax);
        audioSource.volume = volume;

        // Chooses a number between 1 and the length of audiofiles array. Moves chosen audioclip to source and plays it once.
        int n = Random.Range(1, exitAudioFiles.Length);
        audioSource.clip = exitAudioFiles[n];
        audioSource.PlayOneShot(audioSource.clip);

        if (exitRetriggerPrevention)
        {
            exitAudioFiles[n] = exitAudioFiles[0];
            exitAudioFiles[0] = audioSource.clip;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnterRandomAudio();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ExitRandomAudio();
        }
    }
}
