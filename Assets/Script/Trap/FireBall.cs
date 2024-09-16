using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float speed = 7;
    [SerializeField] int bounceNumber = 6;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject disapearEffect;

    Vector2 currentDir;
    int currentBounce;

    Rigidbody2D rb;

    public void SetData(Vector2 dir)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed;
        currentDir = dir;
        currentBounce = bounceNumber;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            ChangeDir();
            currentBounce--;
            if(currentBounce <= 0)
            {
                GameObject effect = Instantiate(disapearEffect, transform.position, Quaternion.identity);
                Destroy(effect, 1f);
                gameObject.SetActive(false);
            }
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage();
        }
    }

    void ChangeDir()
    {
        bool right = Physics2D.Linecast(transform.position, transform.position + Vector3.right, groundLayer);
        bool left = Physics2D.Linecast(transform.position, transform.position - Vector3.right, groundLayer);
        bool up = Physics2D.Linecast(transform.position, transform.position + Vector3.up, groundLayer);
        bool down = Physics2D.Linecast(transform.position, transform.position - Vector3.up, groundLayer);

        if (right)
        {
            if (currentDir == new Vector2(1, 1).normalized)
            {
                currentDir = new Vector2(-1, 1).normalized;
            }
            else
            {
                currentDir = new Vector2(-1, -1).normalized;
            }
        }
        else if (left)
        {
            if (currentDir == new Vector2(-1, 1).normalized)
            {
                currentDir = new Vector2(1, 1).normalized;
            }
            else
            {
                currentDir = new Vector2(1, -1).normalized;
            }
        }
        else if (up)
        {
            if (currentDir == new Vector2(1, 1).normalized)
            {
                currentDir = new Vector2(1, -1).normalized;
            }
            else
            {
                currentDir = new Vector2(-1, -1).normalized;
            }
        }
        else if (down)
        {
            if (currentDir == new Vector2(1, -1).normalized)
            {
                currentDir = new Vector2(1, 1).normalized;
            }
            else
            {
                currentDir = new Vector2(-1, 1).normalized;
            }
        }
        else
        {
            currentDir = -currentDir;
        }

        rb.velocity = currentDir * speed;
    }
}
