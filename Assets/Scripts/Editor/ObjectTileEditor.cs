using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectTile))]
public class ObjectTileEditor : TileEditor
{
    public override void OnInspectorGUI()
    {
        ObjectTile grid = (ObjectTile)target;

        base.OnInspectorGUI();

    }
}
