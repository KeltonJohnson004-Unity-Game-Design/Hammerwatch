using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public GameObject pauseMenuCanvas;
    public GameObject gameOverCanvas;
    private bool gameOver;
    private void Start()
    {
        gameOver = false;
        pauseMenuCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }
    public void DisplayGameOver()
    {
        gameOver = true;
        gameOverCanvas.SetActive(true);
    }
    public void DisplayPauseMenu()
    {
        if (!gameOver)
        {
            Time.timeScale = 0;
            pauseMenuCanvas.SetActive(true);
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Testing");
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {

        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
