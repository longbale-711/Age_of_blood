using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGameOverPresent : MonoBehaviour
{
    [SerializeField] Button _btnRestart;
    // Start is called before the first frame update
    void Start()
    {
        _btnRestart.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
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
