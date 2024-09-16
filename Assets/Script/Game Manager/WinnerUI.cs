using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerUI : MonoBehaviour
{
    [SerializeField] Image winnerImage;

    public void SetWinner(Sprite playerSprite)
    {
        gameObject.SetActive(true);
        winnerImage.sprite = playerSprite;
        Time.timeScale = 0f;
    }
}
