using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound.BasicRandomizer;

public class PlaySoundAtLocation : MonoBehaviour
{
    [TextArea]
    public string comment;

    public BasicRandomizer audioRandomizer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BasicRandomizer.Trigger(audioRandomizer);
        }
    }
}
