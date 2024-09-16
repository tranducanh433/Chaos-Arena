using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] TrapController[] trapController;
    [SerializeField] float newTrapTime;
    [SerializeField] GameObject[] players;
    [SerializeField] GameObject[] playerhealths;
    [SerializeField] WinnerUI winnerUI;
    [SerializeField] Score score;
    [SerializeField] GameObject[] startTexts;
    [SerializeField] AudioSource[] musicToTurnOff;

    public static MapController instance;

    float newTrapCD;
    bool gameStart;

    GameManager GM;
    UIManager UIM;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        newTrapCD = newTrapTime;

        GM = GameManager.instance;
        UIM = UIManager.instance;

        SetupGame();
        StartCoroutine(StartGameCO());
    }

    private void Update()
    {
        if(AllTrapIsActivate() == false && gameStart)
        {
            newTrapCD -= Time.deltaTime;

            if (newTrapCD <= 0)
            {
                List<TrapController> traps = new List<TrapController>();
                foreach (TrapController trap in trapController)
                {
                    if (trap.IsActivate() == false)
                        traps.Add(trap);
                }

                int r = Random.Range(0, traps.Count);
                traps[r].ActiveTrap();

                newTrapCD = newTrapTime;
            }
        }
    }

    void SetupGame()
    {
        if (GM.gameMode == GameMode.SINGLEPLAYER)
        {
            for (int i = 1; i < players.Length; i++)
            {
                players[i].SetActive(false);
                playerhealths[i].SetActive(false);
            }
        }
        else if (GM.gameMode == GameMode.COOP_3HEART)
        {
            if (players.Length > 2)
            {
                for (int i = 2; i < players.Length; i++)
                {
                    players[i].SetActive(false);
                }
            }
        }
    }

    IEnumerator StartGameCO()
    {
        yield return new WaitForSeconds(1f);
        startTexts[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        startTexts[0].SetActive(false);
        startTexts[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        startTexts[1].SetActive(false);
        startTexts[2].SetActive(true);
        yield return new WaitForSeconds(1f);
        startTexts[2].SetActive(false);
        startTexts[3].SetActive(true);
        yield return new WaitForSeconds(1f);
        startTexts[3].SetActive(false);
        startTexts[4].SetActive(true);

        yield return new WaitForSeconds(.5f);
        trapController[0].ActiveTrap();
        trapController[1].ActiveTrap();
        gameStart = true;
        score.StartScore();

        yield return new WaitForSeconds(.5f);
        startTexts[4].SetActive(false);
    }

    bool AllTrapIsActivate()
    {
        foreach (TrapController trap in trapController)
        {
            if (trap.IsActivate() == false)
                return false;
        }

        return true;
    }

    public void DeathSignal()
    {
        if(GM.gameMode == GameMode.SINGLEPLAYER)
        {
            score.StopScore();
            UIM.OpenGameoverScreen();
            StopMusic();
        }
        else if(GM.gameMode == GameMode.COOP_3HEART)
        {
            int numOfSurvival = 0;
            GameObject lastPlayer = null;
            for (int i = 0; i < players.Length; i++)
            {
                if(players[i].GetComponent<PlayerMovement>().hp > 0)
                {
                    numOfSurvival++;
                    lastPlayer = players[i];
                }
            }

            if(numOfSurvival == 1)
            {
                PlayerMovement pm = lastPlayer.GetComponent<PlayerMovement>();
                winnerUI.SetWinner(pm.idleSprite);
                StopMusic();
            }
        }
    }

    public void StopMusic()
    {
        foreach (AudioSource audio in musicToTurnOff)
        {
            audio.Stop();
        }
    }
}
