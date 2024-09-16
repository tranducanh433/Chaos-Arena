using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject[] hearts1;
    [SerializeField] GameObject[] hearts2;

    public void SetHeart(int hp, int id)
    {
        if(id == 0)
        {
            for (int i = 0; i < hearts1.Length; i++)
            {
                if (i < hp)
                {
                    hearts1[i].SetActive(true);
                }
                else
                {
                    hearts1[i].SetActive(false);
                }
            }
        }
        if (id == 1)
        {
            for (int i = 0; i < hearts2.Length; i++)
            {
                if (i < hp)
                {
                    hearts2[i].SetActive(true);
                }
                else
                {
                    hearts2[i].SetActive(false);
                }
            }
        }
    }
}
