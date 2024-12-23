using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private String StartScene; //игрок появляется на разрушенном хабе
    [SerializeField] private GameObject SetingPanel; // панелька слева в меню
    private bool IsOpened = false;
    [SerializeField]private TMP_Text Txt;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }

        char[] Tity = new char[100];
        Tity = PlayerPrefs.GetInt("Score").ToString().ToCharArray();

        string Titi = "";
        if (Tity.Length > 1)
        {
            for (int i = 0; i < Tity.Length; i++)
            {
                Titi +=("<sprite name=\"" + Tity[i].ToString() + "\">");
            }
        }
        Txt.text = Titi;
    }

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
