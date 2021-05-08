using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AudioRequestSystem : MonoBehaviour
{

    [TextArea]
    public string audioRequest;

    private void CreateText()
    {
        //Path of the file
        string path = Application.dataPath + "/AudioRequests.txt";

        //Create file if it doesn't exist
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Audio Requests \n\n");
        }

        File.AppendAllText(path, "Request: ");
        File.AppendAllText(path, audioRequest + "\n\n");

        Debug.Log("Request Added: " + audioRequest);
    }

    // Start is called before the first frame update
    void Start()
    {
        //CreateText();
    }

    public void AddRequest()
    {
        CreateText();
    }
}
