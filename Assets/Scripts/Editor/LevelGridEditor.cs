using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGrid))]
public class LevelGridEditor : GridEditor
{
    public override void OnInspectorGUI()
    {
        LevelGrid grid = (LevelGrid)target;

        base.OnInspectorGUI();
        
    }
}
