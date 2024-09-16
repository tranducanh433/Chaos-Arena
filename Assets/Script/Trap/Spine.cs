using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage();
        }
    }

    public void InActive()
    {
        gameObject.SetActive(false);
    }

    public void SpikeSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
