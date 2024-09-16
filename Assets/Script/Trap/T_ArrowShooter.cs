using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyExtension;

public class T_ArrowShooter : MonoBehaviour
{
    [SerializeField] float surviveTime = 30f;
    [SerializeField] GameObject warning;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float arrowSpeed;
    [SerializeField] float speed;
    [SerializeField] Transform targetAim;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask playerLayer;

    enum State { MOVE, READY, AIM}
    State state = State.MOVE;
    bool moveUp;
    float surviveCD;
    float shootCD;
    Animator anim;
    AudioSource shootSound;

    public void SetData(Vector2 startPos, float dir)
    {
        transform.position = startPos;

        if (dir < 0)
            moveUp = false;
        else
            moveUp = true;
    }

    void Start()
    {
        surviveCD = surviveTime;
        anim = GetComponent<Animator>();
        shootSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        surviveCD -= Time.deltaTime;

        Moving();

        if (surviveCD <= 0 && state == State.MOVE)
        {
            surviveCD = surviveTime;
            gameObject.SetActive(false);
        }
    }

    void Moving()
    {
        if(state != State.AIM)
        {
            if (moveUp)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);

                bool edge = Physics2D.Linecast(transform.position, transform.position + Vector3.up * 2 / 3, wallLayer);

                if(edge == true)
                {
                    moveUp = false;
                }
            }
            else
            {
                transform.Translate(-Vector2.up * speed * Time.deltaTime);

                bool edge = Physics2D.Linecast(transform.position, transform.position - Vector3.up * 2 / 3, wallLayer);

                if (edge == true)
                {
                    moveUp = true;
                }
            }

            bool meetPlayer = Physics2D.Linecast(transform.position, targetAim.position, playerLayer);
            shootCD -= Time.deltaTime;
            if (meetPlayer && shootCD <= 0 && state == State.MOVE)
            {
                StartCoroutine(AimingCO());
                state = State.READY;
                shootCD = 1f;
            }
        }
    }

    IEnumerator AimingCO()
    {
        yield return new WaitForSeconds(0.05f);
        state = State.AIM;
        warning.SetActive(true);

        yield return new WaitForSeconds(0.45f);
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.05f);
        warning.SetActive(false);

        Vector2 dir = targetAim.position - transform.position;
        float z = dir.ToAngle();
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, z));
        arrow.GetComponent<Rigidbody2D>().velocity = arrow.transform.right * arrowSpeed;
        shootSound.Play();

        yield return new WaitForSeconds(0.5f);
        state = State.MOVE;
    }
}
