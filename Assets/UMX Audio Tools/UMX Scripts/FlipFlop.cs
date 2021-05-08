using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FlipFlop : MonoBehaviour
{

    public AudioSource audioSource;

    private bool flip = false;
    private bool flop = true;

    [Header("---------------------Layer One------------------------")]
    public AudioClip[] layerOneAudio;
    [Header("Element 1 Retriggering")]
    public bool retriggerOnePrevention = true;
    [Header("Element 1 Pitch Randomization Amount")]
    public float pitchOneMin = 1f;
    public float pitchOneMax = 1f;
    [Header("Elemenet 1 Volume Randomization Amount")]
    public float volumeOneMin = 1f;
    public float volumeOneMax = 1f;

    [Header("---------------------Layer Two------------------------")]
    public AudioClip[] layerTwoAudio;
    [Header("Element 1 Retriggering")]
    public bool retriggerTwoPrevention = true;
    [Header("Element 1 Pitch Randomization Amount")]
    public float pitchTwoMin = 1f;
    public float pitchTwoMax = 1f;
    [Header("Elemenet 1 Volume Randomization Amount")]
    public float volumeTwoMin = 1f;
    public float volumeTwoMax = 1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            flip = !flip;
            flop = !flop;

            if(flip == true && flop == false)
            {
                PlayLayerOne();
            }

            if(flip == false && flop == true)
            {
                PlayLayerTwo();
            }
        }
    }

    private void PlayLayerOne()
    {
        float pitch = Random.Range(pitchOneMin, pitchOneMax);
        audioSource.pitch = pitch;

        float volume = Random.Range(volumeOneMin, volumeOneMax);
        audioSource.volume = volume;

        // Chooses a number between 1 and the length of audiofiles array. Moves chosen audioclip to source and plays it once.
        int n = Random.Range(1, layerOneAudio.Length);
        audioSource.clip = layerOneAudio[n];
        audioSource.PlayOneShot(audioSource.clip);

        if (retriggerOnePrevention)
        {
            layerOneAudio[n] = layerOneAudio[0];
            layerOneAudio[0] = audioSource.clip;
        }
    }

    private void PlayLayerTwo()
    {
        float pitch = Random.Range(pitchTwoMin, pitchTwoMax);
        audioSource.pitch = pitch;

        float volume = Random.Range(volumeTwoMin, volumeTwoMax);
        audioSource.volume = volume;

        // Chooses a number between 1 and the length of audiofiles array. Moves chosen audioclip to source and plays it once.
        int n = Random.Range(1, layerTwoAudio.Length);
        audioSource.clip = layerTwoAudio[n];
        audioSource.PlayOneShot(audioSource.clip);

        if (retriggerTwoPrevention)
        {
            layerTwoAudio[n] = layerTwoAudio[0];
            layerTwoAudio[0] = audioSource.clip;
        }
    }
}
