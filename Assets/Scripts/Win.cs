using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    public void WinGame()
    {
        WinPanel.SetActive(true);
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
