using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] float explodeCD = 5f;
    [SerializeField] float radius = 3f;
    [SerializeField] GameObject explodeEffect;
    [SerializeField] GameObject explodeSound;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(ExplodeCO());
    }

    IEnumerator ExplodeCO()
    {
        yield return new WaitForSeconds(explodeCD - 1f);
        GetComponent<Animator>().SetTrigger("Ready");
        yield return new WaitForSeconds(0.9f);
        GetComponent<Animator>().SetTrigger("Explode");
        GameObject effect = Instantiate(explodeEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        sr.sprite = null;
        GetComponent<CircleCollider2D>().enabled = false;
        GameObject sound = Instantiate(explodeSound);
        Destroy(sound, 2f);

        yield return new WaitForSeconds(0.15f);
        Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in collision)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerMovement>().TakeDamage();
            }
            else if (collider.CompareTag("Boom"))
            {
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                Vector2 dir = collider.transform.position - transform.position;
                rb.velocity = dir * 15f;
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
