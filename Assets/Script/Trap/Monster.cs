using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyExtension;

public class Monster : MonoBehaviour
{
    [SerializeField] float speed = 7f;
    [SerializeField] int numOfMove = 4;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] GameObject disapearEffect;

    bool isRight;
    bool check = true;
    int numOfMoveLeft;
    float currentSpeed;

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        numOfMoveLeft = numOfMove;
        currentSpeed = speed;
    }

    void Update()
    {
        if (isRight)
        {
            rb.velocity = Vector2.right * currentSpeed;
            sr.flipX = true;
        }
        else
        {
            rb.velocity = -Vector2.right * currentSpeed;
            sr.flipX = false;
        }

        if(check == true)
        {
            if (isRight && Physics2D.Linecast(transform.position, transform.position + Vector3.right * 6.5f, wallLayer) == true ||
                isRight == false && Physics2D.Linecast(transform.position, transform.position - Vector3.right * 6.5f, wallLayer) == true)
            {
                StartCoroutine(ChangeDirCO());
            }
        }
    }

    IEnumerator ChangeDirCO()
    {
        check = false;
        currentSpeed = speed * 5f / 6f;
        yield return new WaitForSeconds(0.25f);
        currentSpeed = speed * 4f / 6f;
        yield return new WaitForSeconds(0.25f);
        currentSpeed = speed * 3f / 6f;
        yield return new WaitForSeconds(0.25f);
        currentSpeed = speed * 2f / 6f;
        yield return new WaitForSeconds(0.25f);
        currentSpeed = speed * 1f / 6f;
        yield return new WaitForSeconds(0.25f);
        isRight = !isRight;

        numOfMoveLeft--;

        if (numOfMoveLeft <= 0)
        {
            GameObject effect = Instantiate(disapearEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.75f);
            StopCoroutine(ChangeDirCO());
            gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.25f);
        currentSpeed = speed * 2f / 6f;
        yield return new WaitForSeconds(0.25f);
        currentSpeed = speed * 3f / 6f;
        yield return new WaitForSeconds(0.25f);
        currentSpeed = speed * 4f / 6f;
        yield return new WaitForSeconds(0.25f);
        currentSpeed = speed * 5f / 6f;
        yield return new WaitForSeconds(0.25f);
        currentSpeed = speed;
        check = true;
    }

    public void SetData(Vector2 startPos, bool isRight)
    {
        currentSpeed = speed;
        check = true;
        transform.position = startPos;
        this.isRight = isRight;
        numOfMoveLeft = numOfMove;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage();
        }
    }
}
