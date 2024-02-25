using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelTile))]
public class LevelTileEditor : TileEditor
{
    public override void OnInspectorGUI()
    {
        LevelTile grid = (LevelTile)target;

        base.OnInspectorGUI();

    }
}
