using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Archer,Castle
}
public class TowerManager : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private GameObject _archerTower;
    [SerializeField] private GameObject _castleTower;
    private GameObject _selectedTower;


    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            PlaceTower(mousePosition);
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                PlaceTower(touch.position);
            }
        } 
#endif
    }

    void PlaceTower(Vector3 cursorPos)
    {
        if (_selectedTower == null) return;
        Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(cursorPos);
        Vector3Int cellPos = _gridManager.GetTilePosition(touchWorldPos);
        if (!_gridManager.IsPlaceable(cellPos))
        {
            Debug.LogError("Cannot place tower at " + cellPos);
            return;
        }

        SpawnTower(cellPos);
    }

    public void SpawnTower(Vector3Int tilePosition)
    {
        var position = _gridManager.GetTileCenterPosition(tilePosition);
        Instantiate(_selectedTower, position, Quaternion.identity);
        _selectedTower = null;

    }

    public void SelectTower(int type)
    {
        if (_selectedTower != null) return;
        _selectedTower = type == 1 ? _archerTower : _castleTower;
    }

    public GameObject GetTower()
    {
        return _selectedTower;
    }
}
