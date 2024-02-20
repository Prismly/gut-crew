using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Grid grid = (Grid)target;

        base.OnInspectorGUI();
        if (GUILayout.Button("Setup Level"))
        {
            if (grid.IsSceneBound())
            {
                grid.CreateNewGrid();
            }
        }
    }
}

