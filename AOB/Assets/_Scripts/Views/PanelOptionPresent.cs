using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOptionPresent : MonoBehaviour
{
    [SerializeField] private Button _btnResume;
    [SerializeField] private Button _btnRestart;

    private void Start()
    {
        _btnResume.onClick.AddListener(ClickBtnResume);
        _btnRestart.onClick.AddListener(ClickBtnRestart);
        ClosePanel();
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void ClickBtnRestart()
    {
        GameManager.Instance.StartGame();
        ClosePanel();
    }

    private void ClickBtnResume()
    {
        GameManager.Instance.Resume();
        ClosePanel();
    }
}
