using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityPitch : MonoBehaviour
{
    public AudioSource audioSource;

    public Rigidbody rigidbodyComponent;

    public float smoothTime = 3f;

    public float multiplier = 0.25f;

    public bool useSmoothening = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (useSmoothening)
        {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, rigidbodyComponent.velocity.magnitude * multiplier, Time.deltaTime * smoothTime);
        }

        else
        {
            audioSource.pitch = rigidbodyComponent.velocity.magnitude * multiplier;
        }
    }
}
