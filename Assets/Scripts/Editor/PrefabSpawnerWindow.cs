using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor tool (Tools ▸ Prefab Spawner) for editing the scene with the mouse:
/// in <see cref="EditMode.Place"/>, right-click drops the selected
/// <see cref="LevelTilePalette"/> prefab on the grid; in <see cref="EditMode.Erase"/>,
/// right-click deletes the object under the cursor. Both actions are undoable, and the
/// tool shares the palette with the level builder so the two stay in sync.
/// </summary>
public class PrefabSpawnerWindow : EditorWindow
{
    private enum EditMode { Place, Erase }

    private const string DefaultParentName = "SpawnedTiles";

    private LevelTilePalette _palette;
    private EditMode _mode = EditMode.Place;
    private int _selectedIndex;
    private Transform _parent;

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
        _mode = (EditMode)EditorGUILayout.EnumPopup("Mode", _mode);

        if (_mode == EditMode.Erase)
        {
            EditorGUILayout.HelpBox("Right-click an object in the Scene to delete it.", MessageType.Info);
            return;
        }

        if (_palette == null || _palette.Tiles.Count == 0)
        {
            EditorGUILayout.HelpBox("Assign a Tile Palette that has at least one prefab.", MessageType.Info);
            return;
        }

        string[] names = PrefabNames();
        _selectedIndex = Mathf.Clamp(_selectedIndex, 0, names.Length - 1);
        _selectedIndex = EditorGUILayout.Popup("Prefab", _selectedIndex, names);
        _parent = (Transform)EditorGUILayout.ObjectField("Parent", _parent, typeof(Transform), true);
        EditorGUILayout.HelpBox($"Right-click in the Scene to place the selected prefab. Placed tiles nest under \"{(_parent != null ? _parent.name : DefaultParentName)}\".", MessageType.Info);
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        if (e.type != EventType.MouseDown || e.button != 1)
            return;

        if (_mode == EditMode.Place)
            PlaceAt(e.mousePosition);
        else
            EraseAt(e.mousePosition);

        e.Use(); // consume the click so it doesn't also open the context menu / change selection
    }

    private void PlaceAt(Vector2 mousePosition)
    {
        GameObject prefab = SelectedPrefab();
        if (prefab == null)
            return;

        Vector3 world = HandleUtility.GUIPointToWorldRay(mousePosition).origin;
        Vector3 position = new Vector3(Mathf.RoundToInt(world.x), Mathf.RoundToInt(world.y), 0f);

        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, ResolveParent());
        instance.transform.position = position;
        Undo.RegisterCreatedObjectUndo(instance, "Spawn " + prefab.name);
    }

    /// <summary>
    /// Returns the container to parent placed tiles under: the user-assigned parent if
    /// set, otherwise a shared "<see cref="DefaultParentName"/>" object that is created
    /// (once) so tiles never end up loose at the hierarchy root.
    /// </summary>
    private Transform ResolveParent()
    {
        if (_parent != null)
            return _parent;

        GameObject container = GameObject.Find(DefaultParentName);
        if (container == null)
        {
            container = new GameObject(DefaultParentName);
            Undo.RegisterCreatedObjectUndo(container, "Create " + DefaultParentName);
        }

        _parent = container.transform;
        return _parent;
    }

    private void EraseAt(Vector2 mousePosition)
    {
        GameObject picked = PickWorldObject(mousePosition);
        if (picked == null)
            return;

        // Delete the whole prefab instance, not just the child that was clicked.
        GameObject target = PrefabUtility.GetOutermostPrefabInstanceRoot(picked);
        Undo.DestroyObjectImmediate(target != null ? target : picked);
    }

    /// <summary>
    /// Picks the front-most world (non-UI) object under the cursor. UI lives on a Canvas
    /// and usually sits in front of the scene, so any Canvas hit is skipped — its whole
    /// subtree is ignored and the search continues with the object behind it — leaving
    /// only real scene objects erasable.
    /// </summary>
    private static GameObject PickWorldObject(Vector2 mousePosition)
    {
        var ignored = new List<GameObject>();
        for (int guard = 0; guard < 10; guard++) // cap in case of many stacked UI layers
        {
            GameObject picked = HandleUtility.PickGameObject(mousePosition, false, ignored.ToArray());
            if (picked == null)
                return null;

            Canvas canvas = picked.GetComponentInParent<Canvas>();
            if (canvas == null)
                return picked; // a world object — safe to erase

            // Ignore the entire UI canvas so the next pick sees what's behind it.
            foreach (Transform child in canvas.rootCanvas.GetComponentsInChildren<Transform>(true))
                ignored.Add(child.gameObject);
        }

        return null;
    }

    private string[] PrefabNames()
    {
        return _palette.Tiles
            .Select(entry => entry.prefab != null ? entry.prefab.name : "(missing prefab)")
            .ToArray();
    }

    private GameObject SelectedPrefab()
    {
        if (_palette == null || _selectedIndex < 0 || _selectedIndex >= _palette.Tiles.Count)
            return null;

        return _palette.Tiles[_selectedIndex].prefab;
    }

    private static LevelTilePalette FindPalette()
    {
        string guid = AssetDatabase.FindAssets("t:LevelTilePalette").FirstOrDefault();
        return guid == null ? null : AssetDatabase.LoadAssetAtPath<LevelTilePalette>(AssetDatabase.GUIDToAssetPath(guid));
    }
}
