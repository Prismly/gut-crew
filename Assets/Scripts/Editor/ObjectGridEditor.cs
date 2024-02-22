using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectGrid))]
public class ObjectGridEditor : GridEditor
{
    public override void OnInspectorGUI()
    {
        ObjectGrid grid = (ObjectGrid)target;

        base.OnInspectorGUI();

    }
}
