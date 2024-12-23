using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;

    private bool Isopened = false;

    // Start is called before the first frame update
    void Start()
    {
        Isopened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseLogick();
    }

    private void PauseLogick()
    {
        if (Isopened == false)
        {
            StopGame();
        }
        else
        {
            Resume();
        }
    }

    private void StopGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Isopened = true;
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        Isopened = false;
    }
}
