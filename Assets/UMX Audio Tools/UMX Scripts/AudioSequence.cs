using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound.Sequencer
{

    [RequireComponent(typeof(AudioSource))]

    public class AudioSequence : MonoBehaviour
{

        [Header("---------------------Element 1 Audio Files------------------------")]
        public AudioClip[] element1AudioFiles;
        private AudioSource audioSource;
        [Header("Element 1 Retriggering")]
        public bool retrigger1Prevention = true;
        [Header("Element 1 Pitch Randomization Amount")]
        public float pitch1Min = 1f;
        public float pitch1Max = 1f;
        [Header("Elemenet 1 Volume Randomization Amount")]
        public float volume1Min = 1f;
        public float volume1Max = 1f;
        [Header("Element 2 Delay Time")]
        public float element2Delay = 1f;

        [Header("---------------------Element 2 Audio Files------------------------")]
        public AudioClip[] element2AudioFiles;
        [Header("Element 2 Retriggering")]
        public bool retrigger2Prevention = true;
        [Header("Element 2 Pitch Randomization Amount")]
        public float pitch2Min = 1f;
        public float pitch2Max = 1f;
        [Header("Elemenet 2 Volume Randomization Amount")]
        public float volume2Min = 1f;
        public float volume2Max = 1f;
        [Header("Element 3 Delay Time")]
        public float element3Delay = 1f;

        [Header("---------------------Element 3 Audio Files------------------------")]
        public AudioClip[] element3AudioFiles;
        [Header("Element 3 Retriggering")]
        public bool retrigger3Prevention = true;
        [Header("Element 3 Pitch Randomization Amount")]
        public float pitch3Min = 1f;
        public float pitch3Max = 1f;
        [Header("Elemenet 3 Volume Randomization Amount")]
        public float volume3Min = 1f;
        public float volume3Max = 1f;






        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Element1Random()
        {
            float pitch = Random.Range(pitch1Min, pitch1Max);
            audioSource.pitch = pitch;

            float volume = Random.Range(volume1Min, volume1Max);
            audioSource.volume = volume;

            // Chooses a number between 1 and the length of audiofiles array. Moves chosen audioclip to source and plays it once.
            int n = Random.Range(1, element1AudioFiles.Length);
            audioSource.clip = element1AudioFiles[n];
            audioSource.PlayOneShot(audioSource.clip);

            if (retrigger1Prevention)
            {
                element1AudioFiles[n] = element1AudioFiles[0];
                element1AudioFiles[0] = audioSource.clip;
            }

            Invoke("Element2Random", element2Delay);
        }

        private void Element2Random()
        {
            float pitch = Random.Range(pitch2Min, pitch2Max);
            audioSource.pitch = pitch;

            float volume = Random.Range(volume2Min, volume2Max);
            audioSource.volume = volume;

            // Chooses a number between 1 and the length of audiofiles array. Moves chosen audioclip to source and plays it once.
            int n = Random.Range(1, element2AudioFiles.Length);
            audioSource.clip = element2AudioFiles[n];
            audioSource.PlayOneShot(audioSource.clip);

            if (retrigger2Prevention)
            {
                element2AudioFiles[n] = element2AudioFiles[0];
                element2AudioFiles[0] = audioSource.clip;
            }

            Invoke("Element3Random", element3Delay);
        }

        private void Element3Random()
        {
            float pitch = Random.Range(pitch3Min, pitch3Max);
            audioSource.pitch = pitch;

            float volume = Random.Range(volume3Min, volume3Max);
            audioSource.volume = volume;

            // Chooses a number between 1 and the length of audiofiles array. Moves chosen audioclip to source and plays it once.
            int n = Random.Range(1, element3AudioFiles.Length);
            audioSource.clip = element3AudioFiles[n];
            audioSource.PlayOneShot(audioSource.clip);

            if (retrigger3Prevention)
            {
                element3AudioFiles[n] = element3AudioFiles[0];
                element3AudioFiles[0] = audioSource.clip;
            }
        }

        public static void Trigger(AudioSequence sequence)
        {
            if (sequence != null) sequence.Element1Random();
        }
    }
}
