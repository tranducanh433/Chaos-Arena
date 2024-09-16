using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject credit;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject howToPlay;
    [SerializeField] GameObject youtubeMess;
    [SerializeField] GameObject customizeCharater;
    [SerializeField] GameObject playUI;

    public void SinglePlayer()
    {
        GameManager.instance.gameMode = GameMode.SINGLEPLAYER;
        SceneManager.LoadScene("Game");
    }

    public void TwoPlayer()
    {
        GameManager.instance.gameMode = GameMode.COOP_3HEART;
        SceneManager.LoadScene("Game");
    }

    public void Play()
    {
        playUI.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Credit()
    {
        credit.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Back()
    {
        youtubeMess.SetActive(false);
        howToPlay.SetActive(false);
        credit.SetActive(false);
        customizeCharater.SetActive(false);
        playUI.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void HowToPlay()
    {
        howToPlay.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CustomizeCharacter()
    {
        customizeCharater.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void YouTube()
    {
        mainMenu.SetActive(false);
        Application.OpenURL("https://www.youtube.com/channel/UC9oQcB_lvyGATYLab9SrvTw/featured");
        youtubeMess.SetActive(true);
    }
}
