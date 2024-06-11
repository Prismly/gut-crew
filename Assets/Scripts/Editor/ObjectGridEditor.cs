using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectGrid))]
public class ObjectGridEditor : GridEditor
{
    public override void OnInspectorGUI()
    {
        //setup base editor
        ObjectGrid grid = (ObjectGrid)target;
        base.OnInspectorGUI();

        //add custom editor functionality for this
        if(GUILayout.Button("Place Object"))
        {
            if (grid.IsSceneBound())
            {
                grid.StopDragging(false);
            }
        }

        if (GUILayout.Button("Disconnect Object"))
        {
            if (grid.IsSceneBound())
            {
                grid.DisconnectGrid();
            }
        }

    }
}
