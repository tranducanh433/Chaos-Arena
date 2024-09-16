using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OpenPauseScreen()
    {
        pauseScreen.SetActive(true);
    }
    public void ClosePauseScreen()
    {
        pauseScreen.SetActive(false);
    }

    public void OpenGameoverScreen()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        ClosePauseScreen();
        Time.timeScale = 1;
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
