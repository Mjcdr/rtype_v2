using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool _IsPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        //if (_IsPaused)
        //{

        //    if (Input.GetKeyDown(KeyCode.R))
        //    {
        //        Time.timeScale = 1.0f;
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //    }

        //}
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        _IsPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        _IsPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        _IsPaused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.LogWarning("Quitting application");
        Application.Quit();
    }
}
