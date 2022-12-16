using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LastTimerUpdateScriptableObject))]
public class LastTimerUpdateCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LastTimerUpdateScriptableObject lastUpdate = (LastTimerUpdateScriptableObject)target;

        if(GUILayout.Button("Set to DateTime.Now"))
        {
            lastUpdate.Value = DateTime.Now;
        }
    }
}
