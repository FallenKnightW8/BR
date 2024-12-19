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
        WinPanel.SetActive(true);
        CounterOfDies.text = PlayerPrefs.GetInt("CounterOfDies").ToString();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitTheGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
