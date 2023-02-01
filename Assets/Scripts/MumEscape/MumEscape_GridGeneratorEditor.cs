using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MumEscape_GenerationProcedural), true)]
public class MumEscape_GridGeneratorEditor : Editor
{
    MumEscape_GenerationProcedural generator;

    private void Awake()
    {
        generator = (MumEscape_GenerationProcedural)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Create Grid"))
        {
            generator.GenerateRoom();
        }

    }
}
