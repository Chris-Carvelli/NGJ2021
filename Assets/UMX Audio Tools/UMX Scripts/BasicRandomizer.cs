using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound.BasicRandomizer {

[RequireComponent(typeof(AudioSource))]

public class BasicRandomizer : MonoBehaviour
    {
        [Header("Audio Files")]
        public AudioClip[] audioFiles;
        private AudioSource audioSource;
        [Header("Retriggering")]
        public bool retriggerPrevention = true;
        [Header("Pitch Randomization Amount")]
        public float pitchMin = 1f;
        public float pitchMax = 1f;
        [Header("Volume Randomization Amount")]
        public float volumeMin = 1f;
        public float volumeMax = 1f;

        // Start is called before the first frame update
        void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayRandomAudio()
    {

            float pitch = Random.Range(pitchMin, pitchMax);
            audioSource.pitch = pitch;

            float volume = Random.Range(volumeMin, volumeMax);
            audioSource.volume = volume;

            // Chooses a number between 1 and the length of audiofiles array. Moves chosen audioclip to source and plays it once.
            int n = Random.Range(1, audioFiles.Length);
            audioSource.clip = audioFiles[n];
            audioSource.PlayOneShot(audioSource.clip);

            if (retriggerPrevention)
            {
                audioFiles[n] = audioFiles[0];
                audioFiles[0] = audioSource.clip;
            }
    }
    
        public static void Trigger(BasicRandomizer random)
        {
            if (random != null) random.PlayRandomAudio();
        }

}
}

