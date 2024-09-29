using System;
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
    [SerializeField] private Tower _archerTower;
    [SerializeField] private Tower _castleTower;
    private Tower _selectedTower;
    private GameManager _manager;
    private List<Tower> _lstTower;
    private bool _canPlace;

    private void Start()
    {
        _manager = GameManager.Instance;
        _gridManager = _manager.GridManager;
        _manager.PlayerController.OnPurchaseTowerCompleted += SetSelectedTower;
        _manager.OnStartGame += ConfigStartGame;
    }

    

    void Update()
    {
        if (!_canPlace) return;
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

    private void OnDestroy()
    {
        _manager.PlayerController.OnPurchaseTowerCompleted -= SetSelectedTower;
        _manager.OnStartGame -= ConfigStartGame;
    }

    private void ConfigStartGame()
    {
        ResetValue();
    }

    public void PlaceTower(Vector3 cursorPos)
    {
        if (_selectedTower == null) return;
        Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(cursorPos);
        Vector3Int cellPos = _gridManager.GetTilePosition(touchWorldPos);
        if (!_gridManager.IsPlaceable(cellPos))
        {
            Debug.LogError("Cannot place tower at " + cellPos);
            return;
        }

        SpawnTower(cellPos,()=> SetSelectedTower(null));
    }

    public void SpawnTower(Vector3Int tilePosition,Action onSpawnCompleted = null)
    {
        var position = _gridManager.GetTileCenterPosition(tilePosition);
        var tower = Instantiate(_selectedTower, position, Quaternion.identity);
        _lstTower.Add(tower);
        onSpawnCompleted?.Invoke();
    }

    public Tower SelectTower(TowerType type)
    {
        return type == TowerType.Archer ? _archerTower : _castleTower;
    }

    public Tower GetSelectedTower() => _selectedTower;

    private void SetSelectedTower(Tower tower)
    {
        _selectedTower = tower;
        _canPlace = _selectedTower != null;
    }

    private void ResetValue()
    {
        SetSelectedTower(null);
        ResetListTower();

    }

    private void ResetListTower()
    {
        if (_lstTower == null)
        {
            _lstTower = new List<Tower>();
            return;
        }

        if (_lstTower.Count <= 0) return;
        foreach (var tower in _lstTower)
        {
            Destroy(tower.gameObject);
        }
        _lstTower.Clear();
    }
}
