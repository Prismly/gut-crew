using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tile))]
public class TileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Tile tile = (Tile)target;

        base.OnInspectorGUI();
        if (GUILayout.Button("Spawn Tile Left"))
        {
            if (tile.IsSceneBound())
            {
                tile.SpawnLeft();
            }
        }
        if (GUILayout.Button("Spawn Tile Right"))
        {
            if (tile.IsSceneBound())
            {
                tile.SpawnRight();
            }
        }
        if (GUILayout.Button("Spawn Tile Above"))
        {
            if (tile.IsSceneBound())
            {
                tile.SpawnAbove();
            }
        }
        if (GUILayout.Button("Spawn Tile Below"))
        {
            if (tile.IsSceneBound())
            {
                tile.SpawnBelow();
            }
        }
        GUILayout.Space(10);
        if (GUILayout.Button("DestroyTile"))
        {
            if (tile.IsSceneBound())
            {
                tile.DeleteTile(true);
            }
        }
    }
}

