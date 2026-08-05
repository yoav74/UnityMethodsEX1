using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuildLevel : EditorWindow
{
    private TextAsset _curLevel;
    private GameObject _world;

    [MenuItem("Tools/Level Creator")]
    public static void ShowWindow()
    {
        Debug.Log("Level Creator");
        GetWindow<BuildLevel>("Level Creator");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Assign Level File:");
        _curLevel = EditorGUILayout.ObjectField(_curLevel, typeof(TextAsset), false) as TextAsset;

        EditorGUILayout.LabelField("Assign Parent Transform");
        _world = EditorGUILayout.ObjectField(_world, typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("Create Level") && _curLevel != null && _world != null)
        {
            CreateLevel();
        }
    }

    private void CreateLevel()
    {
        try
        {
            Debug.Log("Creating Level: " + _curLevel.name);
            string jsonData = _curLevel.text;
            Dictionary<string, object> gameData = MiniJSON.Json.Deserialize(jsonData) as Dictionary<string, object>;
            int height = int.Parse(gameData["height"].ToString());
            int width = int.Parse(gameData["width"].ToString());
            Debug.Log(height + " " + width);

            List<object> layers = (List<object>)gameData["layers"];
            foreach (object obj in layers)
            {
                Dictionary<string, object> layerData = (Dictionary<string, object>)obj;
                if (layerData.ContainsKey("data"))
                {
                    List<object> levelTiles = (List<object>)layerData["data"];
                    Debug.Log(levelTiles.Count);
                    for (int i = 0; i < levelTiles.Count; i++)
                    {
                        // Debug.Log(levelTiles[i].ToString());
                        switch (levelTiles[i].ToString())
                        {
                            case "1": CreateGameObject("Prefab_Floor", i, height, width); break;
                            case "2": CreateGameObject("Prefab_Mario", i, height, width); break;
                            case "3": CreateGameObject("Prefab_PickableAxe", i, height, width); break;
                            case "4": CreateGameObject("Prefab_Coin", i, height, width); break;
                            case "5": CreateGameObject("Prefab_Floor", i, height, width); break;
                            case "6": CreateGameObject("Prefab_Mario", i, height, width); break;
                            case "11": CreateGameObject("Prefab_Door", i, height, width); break;
                            case "12": CreateGameObject("Prefab_Goomba", i, height, width); break;
                            case "13": CreateGameObject("Prefab_Key", i, height, width); break;
                            case "15": CreateGameObject("Prefab_Flower", i, height, width); break;
                            case "16": CreateGameObject("Prefab_Spikes", i, height, width); break;



                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void CreateGameObject(string prefabName, int index, int height, int width)
    {
        try
        {
            Debug.Log("Creating GameObject: " + prefabName);
            GameObject temp = Instantiate(Resources.Load(prefabName)) as GameObject;
            int colCalc = index % width;
            string col = colCalc.ToString();
            if (colCalc < 10)
                col = "0" + colCalc;

            int rowCalc = (int)((height - 1) - ((int)(index / width)));
            string row = rowCalc.ToString();
            if (rowCalc < 10)
                row = "0" + rowCalc;

            temp.name = row + col;
            temp.transform.localPosition = new Vector3(colCalc, rowCalc, 0);
            temp.transform.SetParent(_world.transform);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
