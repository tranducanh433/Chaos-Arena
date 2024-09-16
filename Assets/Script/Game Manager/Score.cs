using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI yourScoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;

    float score;
    bool scoring = false;

    GameManager GM;

    void Start()
    {
        GM = GameManager.instance;
    }

    void Update()
    {
        if (GM.gameMode == GameMode.SINGLEPLAYER)
        {
            if (scoring == true)
            {
                score += Time.deltaTime;
                scoreText.text = ((int)score).ToString();
            }
        }
        else
        {
            scoreText.gameObject.SetActive(false);
        }
    }

    public void StartScore()
    {
        scoring = true;
    }

    public void StopScore()
    {
        scoring = false;

        if(GM.gameMode == GameMode.SINGLEPLAYER)
        {
            if (score >= GM.highestScore)
            {
                GM.highestScore = (int)score;
            }

            bestScoreText.text = "Hightest Score: " + GM.highestScore.ToString();
            yourScoreText.text = "Your Score: " + ((int)score).ToString();
        }
    }
}
