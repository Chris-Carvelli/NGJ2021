using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonFootsteps : MonoBehaviour
{
    public AudioClip[] grassSteps;
    public AudioClip[] dirtSteps;
    public AudioClip[] concreteSteps;
    public AudioClip[] waterSteps;

    public AudioSource footstepSource;

    // Start is called before the first frame update
    void Start()
    {
        footstepSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Footsteps()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        int g = Random.Range(0, grassSteps.Length);
        int d = Random.Range(0, dirtSteps.Length);
        int c = Random.Range(0, concreteSteps.Length);
        int w = Random.Range(0, waterSteps.Length);

        

        if (Physics.Raycast(ray, out hit, 1f))
        {
            switch (hit.transform.tag)
            {
                case "Grass":
                    footstepSource.PlayOneShot(grassSteps[g]);
                    break;

                case "Dirt":
                    footstepSource.PlayOneShot(dirtSteps[d]);
                    break;

                case "Concrete":
                    footstepSource.PlayOneShot(concreteSteps[c]);
                    break;

                case "Water":
                    footstepSource.PlayOneShot(waterSteps[w]);
                    break;

                default:
                    footstepSource.PlayOneShot(grassSteps[g]);
                    break;
            }
        }
    }
}
