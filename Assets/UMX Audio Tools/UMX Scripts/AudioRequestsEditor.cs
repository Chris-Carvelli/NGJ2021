using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioRequestSystem))]
public class AudioRequestsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AudioRequestSystem myScript = (AudioRequestSystem)target;
        if (GUILayout.Button("Add Request"))
        {
            myScript.AddRequest();
        }
    }
}
