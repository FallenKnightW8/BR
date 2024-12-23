using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] TMP_Text CounterOfDies;

    public void WinGame()
    {
        StartTime();
        WinPanel.SetActive(true);
        CounterOfDies.text = PlayerPrefs.GetInt("CounterOfDies").ToString();
    }

    public void MainMenu()
    {
        StartTime();
        SceneManager.LoadScene(0);
    }
    public void QuitTheGame()
    {
        StartTime();
        Application.Quit();
    }

    public void Restart()
    {
        StartTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void StartTime()
    {
        Time.timeScale = 1;
    }
}
