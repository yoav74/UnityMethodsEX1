using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor tool (Tools ▸ Level Creator) that reads a Tiled JSON level and spawns the
/// matching prefabs from a <see cref="LevelTilePalette"/>. Because it runs at edit
/// time it references prefabs directly through the palette — no Resources folder and
/// no string lookups.
/// </summary>
public class BuildLevel : EditorWindow
{
    private TextAsset _curLevel;
    private LevelTilePalette _palette;
    private GameObject _world;

    [MenuItem("Tools/Level Creator")]
    public static void ShowWindow()
    {
        GetWindow<BuildLevel>("Level Creator");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Level File (Tiled JSON):");
        _curLevel = EditorGUILayout.ObjectField(_curLevel, typeof(TextAsset), false) as TextAsset;

        EditorGUILayout.LabelField("Tile Palette:");
        _palette = EditorGUILayout.ObjectField(_palette, typeof(LevelTilePalette), false) as LevelTilePalette;

        EditorGUILayout.LabelField("Parent Transform:");
        _world = EditorGUILayout.ObjectField(_world, typeof(GameObject), true) as GameObject;

        using (new EditorGUI.DisabledScope(_curLevel == null || _palette == null || _world == null))
        {
            if (GUILayout.Button("Create Level"))
                CreateLevel();
        }
    }

    private void CreateLevel()
    {
        try
        {
            Dictionary<string, object> gameData = MiniJSON.Json.Deserialize(_curLevel.text) as Dictionary<string, object>;
            int height = int.Parse(gameData["height"].ToString());
            int width = int.Parse(gameData["width"].ToString());

            List<object> layers = (List<object>)gameData["layers"];
            foreach (object layer in layers)
            {
                Dictionary<string, object> layerData = (Dictionary<string, object>)layer;
                if (!layerData.ContainsKey("data"))
                    continue;

                List<object> tiles = (List<object>)layerData["data"];
                for (int i = 0; i < tiles.Count; i++)
                {
                    if (!int.TryParse(tiles[i].ToString(), out int tileId) || tileId == 0)
                        continue; // 0 = empty cell

                    GameObject prefab = _palette.GetPrefab(tileId);
                    if (prefab != null)
                        CreateTile(prefab, i, height, width);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Level Creator failed: " + e.Message);
        }
    }

    private void CreateTile(GameObject prefab, int index, int height, int width)
    {
        GameObject tile = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        Undo.RegisterCreatedObjectUndo(tile, "Create Level Tile");

        int col = index % width;
        int row = (height - 1) - (index / width);

        tile.name = row.ToString("00") + col.ToString("00");
        tile.transform.SetParent(_world.transform);
        tile.transform.localPosition = new Vector3(col, row, 0);
    }
}
