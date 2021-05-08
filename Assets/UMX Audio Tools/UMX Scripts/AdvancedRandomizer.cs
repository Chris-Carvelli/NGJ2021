using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace Sound.AdvancedRandomizer
{


public class AdvancedRandomizer : MonoBehaviour
{

        [Header("---------------------Layer One------------------------")]
        public AudioClip[] layerOneAudio;
        public AudioSource layerOneSource;
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
        public AudioSource layerTwoSource;
        [Header("Element 1 Retriggering")]
        public bool retriggerTwoPrevention = true;
        [Header("Element 1 Pitch Randomization Amount")]
        public float pitchTwoMin = 1f;
        public float pitchTwoMax = 1f;
        [Header("Elemenet 1 Volume Randomization Amount")]
        public float volumeTwoMin = 1f;
        public float volumeTwoMax = 1f;

        [Header("---------------------Layer Three------------------------")]
        public AudioClip[] layerThreeAudio;
        public AudioSource layerThreeSource;
        [Header("Element 1 Retriggering")]
        public bool retriggerThreePrevention = true;
        [Header("Element 1 Pitch Randomization Amount")]
        public float pitchThreeMin = 1f;
        public float pitchThreeMax = 1f;
        [Header("Elemenet 1 Volume Randomization Amount")]
        public float volumeThreeMin = 1f;
        public float volumeThreeMax = 1f;


        // Start is called before the first frame update
        void Start()
        {
            layerOneSource = gameObject.AddComponent<AudioSource>();
            layerTwoSource = gameObject.AddComponent<AudioSource>();
            layerThreeSource = gameObject.AddComponent<AudioSource>();

            layerOneSource.playOnAwake = false;
            layerTwoSource.playOnAwake = false;
            layerThreeSource.playOnAwake = false;

        }

        void PlayAll()
        {
            LayerOnePlay();
            LayerTwoPlay();
            LayerThreePlay();
        }

        void LayerOnePlay()
        {
            float pitch = Random.Range(pitchOneMin, pitchOneMax);
            layerOneSource.pitch = pitch;

            float volume = Random.Range(volumeOneMin, volumeOneMax);
            layerOneSource.volume = volume;

            // Chooses a number between 1 and the length of audiofiles array. Moves chosen audioclip to source and plays it once.
            int n = Random.Range(1, layerOneAudio.Length);
            layerOneSource.clip = layerOneAudio[n];
            layerOneSource.PlayOneShot(layerOneSource.clip);

            if (retriggerOnePrevention)
            {
                layerOneAudio[n] = layerOneAudio[0];
                layerOneAudio[0] = layerOneSource.clip;
            }
        }

        void LayerTwoPlay()
        {
            float pitch = Random.Range(pitchTwoMin, pitchTwoMax);
            layerTwoSource.pitch = pitch;

            float volume = Random.Range(volumeTwoMin, volumeTwoMax);
            layerTwoSource.volume = volume;

            // Chooses a number between 1 and the length of audiofiles array. Moves chosen audioclip to source and plays it once.
            int n = Random.Range(1, layerTwoAudio.Length);
            layerTwoSource.clip = layerTwoAudio[n];
            layerTwoSource.PlayOneShot(layerTwoSource.clip);

            if (retriggerTwoPrevention)
            {
                layerTwoAudio[n] = layerTwoAudio[0];
                layerTwoAudio[0] = layerTwoSource.clip;
            }
        }

        void LayerThreePlay()
        {
            float pitch = Random.Range(pitchThreeMin, pitchThreeMax);
            layerThreeSource.pitch = pitch;

            float volume = Random.Range(volumeThreeMin, volumeThreeMax);
            layerThreeSource.volume = volume;

            // Chooses a number between 1 and the length of audiofiles array. Moves chosen audioclip to source and plays it once.
            int n = Random.Range(1, layerThreeAudio.Length);
            layerThreeSource.clip = layerThreeAudio[n];
            layerThreeSource.PlayOneShot(layerThreeSource.clip);

            if (retriggerThreePrevention)
            {
                layerThreeAudio[n] = layerThreeAudio[0];
                layerThreeAudio[0] = layerThreeSource.clip;
            }
        }

        public static void Trigger(AdvancedRandomizer random)
        {
            if (random != null) random.PlayAll();
        }
    }
}
