using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maps level tile IDs (as they appear in the Tiled map data) to the prefab to
/// spawn for each. Lets the level builder reference prefabs directly instead of
/// loading them from a Resources folder by name, so prefabs can live anywhere and
/// adding a new tile type is a data edit rather than a code change (Open/Closed).
/// </summary>
[CreateAssetMenu(fileName = "LevelTilePalette", menuName = "Level/Tile Palette")]
public class LevelTilePalette : ScriptableObject
{
    [Serializable]
    public struct TileEntry
    {
        public int id;
        public GameObject prefab;
    }

    [SerializeField] private List<TileEntry> tiles = new List<TileEntry>();

    /// <summary>Returns the prefab mapped to <paramref name="id"/>, or null if none.</summary>
    public GameObject GetPrefab(int id)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].id == id)
                return tiles[i].prefab;
        }

        return null;
    }
}
