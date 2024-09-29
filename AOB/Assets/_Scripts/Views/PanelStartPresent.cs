using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelStartPresent : MonoBehaviour
{
    [SerializeField] Button _btnStart;
    // Start is called before the first frame update
    void Start()
    {
        _btnStart.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        GameManager.Instance.StartGame();
        Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
