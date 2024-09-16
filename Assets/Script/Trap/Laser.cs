using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float minSpeed = 1f;

    Vector2 startPos;

    enum MoveState { NONE, MOVING, COMEBACK}
    MoveState moveState;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if(moveState == MoveState.MOVING)
        {
            float speed = Mathf.Clamp(Vector2.Distance(transform.position, targetPos.position), minSpeed, maxSpeed);
            transform.position = Vector2.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, targetPos.position) <= 0.1f)
            {
                moveState = MoveState.COMEBACK;
            }
        }
        else if(moveState == MoveState.COMEBACK)
        {
            float speed = Mathf.Clamp(Vector2.Distance(transform.position, targetPos.position), minSpeed, maxSpeed);
            transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, startPos) <= 0.1f)
            {
                moveState = MoveState.NONE;
            }
        }
    }

    public void StartMoving()
    {
        moveState = MoveState.MOVING;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage();
        }
    }
}
