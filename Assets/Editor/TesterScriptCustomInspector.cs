// using System;
// using UnityEditor;
// using UnityEngine;

// [CustomEditor(typeof(TesterScript))]
// public class TesterScriptCustomInspector : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();

//         TesterScript tester = (TesterScript)target;

//         if (GUILayout.Button("Reset Factories"))
//             tester.ResetFactoryValues();

//         if (GUILayout.Button("Reset Currency"))
//             tester.ResetCurrency();

//         if (GUILayout.Button("Reset Timer"))
//             tester.ResetTimer();

//         if (GUILayout.Button("Reset Everything"))
//             tester.ResetEverything();
//     }
// }
