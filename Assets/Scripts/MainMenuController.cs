using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject readMeCanvas;
    public GameObject mainMenuCanvas;
    public void StartGame()
    {
        SceneManager.LoadScene("Testing");
    }

    public void OpenReadMe()
    {
        mainMenuCanvas.SetActive(false);
        readMeCanvas.SetActive(true);
    }

    public void CloseReadMe()
    {
        readMeCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
}
