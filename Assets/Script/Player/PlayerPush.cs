using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPush : MonoBehaviour
{
    [SerializeField] Transform pushPoint;
    [SerializeField] Vector2 size;
    [SerializeField] LayerMask pushableLayer;
    [SerializeField] float pushTime = 2f;
    [SerializeField] GameObject pushEffect;

    float pushCD;

    private void Update()
    {
        pushCD -= Time.deltaTime;
    }

    public void Push(InputAction.CallbackContext context)
    {
        if (context.performed && pushCD <= 0)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(pushPoint.position, size, 0, pushableLayer);

            foreach (Collider2D hit in hits)
            {
                if (hit.name == "Boom(Clone)")
                {
                    Vector2 dir = (hit.transform.position - transform.position).normalized;
                    hit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    hit.GetComponent<Rigidbody2D>().velocity = dir * 15f;

                    PushEffect();
                    pushCD = pushTime;
                }
                else if (hit.CompareTag("Player"))
                {
                    Vector2 dir = (hit.transform.position - transform.position).normalized;
                    hit.GetComponent<PlayerMovement>().Push(dir);

                    PushEffect();
                    pushCD = pushTime;
                }
            }

        }
    }

    void PushEffect()
    {
        GameObject effect = Instantiate(pushEffect, pushPoint.position, transform.rotation);
        Destroy(effect, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(pushPoint.position, size);
    }
}
