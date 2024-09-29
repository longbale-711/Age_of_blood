using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pattern.Singleton;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap; // Reference to your Tilemap
    public GameObject towerPrefab; // The tower prefab you want to instantiate

    // Get tile base information
    private TileBase GetTile(Vector3Int tilePosition)
    {
        return tilemap.GetTile(tilePosition);
    }

    public Vector3Int GetTilePosition(Vector3 touchPos)
    {
        return tilemap.WorldToCell(touchPos);
    }

    public Vector3 GetTileCenterPosition(Vector3Int tilePosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(tilePosition);

        Vector3 centerPosition = tilemap.GetCellCenterWorld(cellPosition);
        Debug.Log("Mouse clicked at grid position: " + centerPosition);

        return centerPosition;


    }

    public bool IsPlaceable(Vector3Int cellPosition)
    {
        var tile = GetTile(cellPosition);
        return tile == null;
    }

    
}
