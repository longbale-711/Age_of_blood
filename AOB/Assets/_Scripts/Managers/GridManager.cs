using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pattern.Singleton;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap; // Reference to your Tilemap
    public GameObject towerPrefab; // The tower prefab you want to instantiate

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            SpawnObjectOnTile(mousePosition);
            

        }
    }

    public void SpawnObjectOnTile(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);

        Vector3 spawnPosition = tilemap.GetCellCenterWorld(cellPosition);
        Debug.Log("Mouse clicked at grid position: " + spawnPosition);

        var tile = GetTile(cellPosition);
        if (tile == null)
        {
            Debug.Log("Cant place at that position\nTile is not null!");
            return;
        }

        Instantiate(towerPrefab, spawnPosition, Quaternion.identity);


    }


    // Get tile base information
    private TileBase GetTile(Vector3Int tilePosition)
    {
        return tilemap.GetTile(tilePosition);
    }
}
