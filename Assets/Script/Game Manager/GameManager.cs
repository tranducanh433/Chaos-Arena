using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    SINGLEPLAYER,
    COOP_3HEART
}

public class GameManager : MonoBehaviour
{
    public GameMode gameMode;
    [HideInInspector] public int highestScore;

    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PauseGame()
    {
        UIManager.instance.OpenPauseScreen();
        Time.timeScale = 0;
    }
}
