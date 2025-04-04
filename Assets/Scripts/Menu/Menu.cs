using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private String StartScene;
    [SerializeField] private GameObject SetingPanel;
    [SerializeField] private TMP_Text Txt;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip Muse;
    private bool IsOpened = false;

    private void Awake()
    {
        audioSource.clip = Muse;
        audioSource.Play();
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("MaxScore", 0);
        }
        if(PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("MaxScore"))
            PlayerPrefs.SetInt("MaxScore", PlayerPrefs.GetInt("Score"));
        PlayerPrefs.SetInt("Score", 0);
        char[] Tity = new char[100];
        Tity = PlayerPrefs.GetInt("MaxScore").ToString().ToCharArray();
        string Titi = "";
        for (int i = 0; i < Tity.Length; i++)
        {
            Titi += ("<sprite name=\"" + Tity[i].ToString() + "\">");
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
