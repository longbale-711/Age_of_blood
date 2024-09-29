using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    [Header("UI Component")]
    [SerializeField] private Button _btnArcherTower;
    [SerializeField] private Button _btnCastleTower;
    [SerializeField] private Button _btnOption;
    [SerializeField] private Slider _sliderHealthBar;
    [SerializeField] private TMP_Text _txtMoney;
    [SerializeField] private PanelOptionPresent PanelOptionPresent;
    [SerializeField] private PanelStartPresent PanelStartPresent;
    [SerializeField] private PanelGameOverPresent PanelGameOverPresent;

    private GameManager _manager;
    private PlayerController _playerController;
    private Player _curPlayer;


    private void Start()
    {
        _manager = GameManager.Instance;
        _playerController = _manager.PlayerController;
        _curPlayer = _playerController.GetCurrentPlayer();

        _btnArcherTower.onClick.AddListener(()=> PickTower(TowerType.Archer));
        _btnCastleTower.onClick.AddListener(()=> PickTower(TowerType.Castle));
        _btnOption.onClick.AddListener(OpenOptionPanel);
        _curPlayer.OnHealthChanged += UpdateHealthBar;
        _curPlayer.OnMoneyChanged += UpdateMoneyText;

        _sliderHealthBar.value = _sliderHealthBar.maxValue = _curPlayer.Health;
    }

    private void OnDestroy()
    {
        _curPlayer.OnHealthChanged -= UpdateHealthBar;
        _curPlayer.OnMoneyChanged -= UpdateMoneyText;
    }

    private void PickTower(TowerType type)
    {
        var tower = _manager.TowerManager.SelectTower(type);
        _playerController.Purchase(tower);
    }

    private void OpenOptionPanel()
    {
        _manager.Pause();
        PanelOptionPresent.OpenPanel();
    }

    public void OpenGameOverPanel()
    {
        PanelGameOverPresent.Open();
    }

    private void UpdateMoneyText(int money)
    {
        _txtMoney.text = money.ToString();
    }

    private void UpdateHealthBar(int health)
    {
        _sliderHealthBar.value = health;
    }

    
}
