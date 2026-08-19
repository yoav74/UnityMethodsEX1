using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor tool (Tools ▸ Prefab Spawner) that places any tile from the shared
/// <see cref="LevelTilePalette"/> into the scene with a right-click in the Scene
/// view. It reads the same palette the level builder uses, so the available prefabs
/// always stay in sync. Placed tiles are real prefab instances and are undoable.
/// </summary>
public class PrefabSpawnerWindow : EditorWindow
{
    private LevelTilePalette _palette;
    private bool _spawningEnabled = true;
    private int _selectedIndex;

    [MenuItem("Tools/Prefab Spawner")]
    public static void ShowWindow()
    {
        GetWindow<PrefabSpawnerWindow>("Prefab Spawner");
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
        if (_palette == null)
            _palette = FindPalette();
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnGUI()
    {
        _palette = (LevelTilePalette)EditorGUILayout.ObjectField("Tile Palette", _palette, typeof(LevelTilePalette), false);

        if (_palette == null || _palette.Tiles.Count == 0)
        {
            EditorGUILayout.HelpBox("Assign a Tile Palette that has at least one prefab.", MessageType.Info);
            return;
        }

        string[] names = PrefabNames();
        _selectedIndex = Mathf.Clamp(_selectedIndex, 0, names.Length - 1);
        _selectedIndex = EditorGUILayout.Popup("Prefab", _selectedIndex, names);

        _spawningEnabled = EditorGUILayout.ToggleLeft("Right-click in the Scene to place", _spawningEnabled);
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (!_spawningEnabled || _palette == null)
            return;

        Event e = Event.current;
        if (e.type != EventType.MouseDown || e.button != 1)
            return;

        GameObject prefab = SelectedPrefab();
        if (prefab == null)
            return;

        Vector3 world = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
        Vector3 position = new Vector3(Mathf.RoundToInt(world.x), Mathf.RoundToInt(world.y), 0f);

        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        instance.transform.position = position;
        Undo.RegisterCreatedObjectUndo(instance, "Spawn " + prefab.name);

        e.Use(); // consume the click so it doesn't also change the selection
    }

    private string[] PrefabNames()
    {
        return _palette.Tiles
            .Select(entry => entry.prefab != null ? entry.prefab.name : "(missing prefab)")
            .ToArray();
    }

    private GameObject SelectedPrefab()
    {
        if (_selectedIndex < 0 || _selectedIndex >= _palette.Tiles.Count)
            return null;

        return _palette.Tiles[_selectedIndex].prefab;
    }

    private static LevelTilePalette FindPalette()
    {
        string guid = AssetDatabase.FindAssets("t:LevelTilePalette").FirstOrDefault();
        return guid == null ? null : AssetDatabase.LoadAssetAtPath<LevelTilePalette>(AssetDatabase.GUIDToAssetPath(guid));
    }
}
