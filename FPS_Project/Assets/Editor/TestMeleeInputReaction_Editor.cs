using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestMeleeInputReaction))]
public class TestMeleeInputReaction_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        TestMeleeInputReaction targetScript = (TestMeleeInputReaction)target;

        DrawDefaultInspector();
        GUILayout.Label("");
        if (GUILayout.Button("Attack"))
        {
            targetScript.Attack();
        }
        if (GUILayout.Button("Inspect"))
        {
            targetScript.Inspect();
        }
    }
}
