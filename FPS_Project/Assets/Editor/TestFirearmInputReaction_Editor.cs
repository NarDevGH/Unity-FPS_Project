using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestFirearmInputReaction))]
public class TestFirearmInputReaction_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        TestFirearmInputReaction targetScript = (TestFirearmInputReaction)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Fire")) 
        {
            targetScript.FireWeapon();
        }

        if (GUILayout.Button("Reload"))
        {
            targetScript.RealodWeapon();
        }

        if (GUILayout.Button("NextAds"))
        {
            targetScript.NextWeaponAds();
        }
    }
}
