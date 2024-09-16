using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_LaserPillar : MonoBehaviour
{

    [SerializeField] ParticleSystem releaseEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage();
        }
    }

    public void StartEffect()
    {
        releaseEffect.Play();
    }

    public void EndEffect()
    {
        releaseEffect.Stop();
    }

    public void InActiveGameObject()
    {
        gameObject.SetActive(false);
    }
}
