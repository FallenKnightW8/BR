using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private String StartScene; //игрок появляется на разрушенном хабе
    [SerializeField] private GameObject SetingPanel; // панелька слева в меню
    private bool IsOpened = false;

    public void NewGame() // новая игра, удаляет сохранения
    {
        SceneManager.LoadScene(StartScene);
    }

    public void SettingsButton() // открытие настроек игры
    {
        if (IsOpened)
        {
            SetingPanel.SetActive(false);
            IsOpened = false;
        }
        else
        {
            SetingPanel.SetActive(true);
            IsOpened = true;
        }

    }

    public void ExitPressed() // закрытие игры
    {
        Application.Quit();
    }
}
