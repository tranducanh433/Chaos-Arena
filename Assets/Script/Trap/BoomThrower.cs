using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyExtension;

public class BoomThrower : Trap
{
    [SerializeField] GameObject boomPrefab;
    [SerializeField] GameObject appearEffect;
    [SerializeField] float throwForce = 7f;

    bool isThrowing = false;

    public override bool IsActivate()
    {
        return isThrowing;
    }

    protected override void Activate()
    {
        GetComponent<Animator>().SetTrigger("Throw");
        isThrowing = true;
    }

    public void ThrowBomb()
    {
        float r = Random.Range(45f, 135f);
        Vector2 dir = r.ToDirection();
        GameObject boom = Instantiate(boomPrefab, transform.position, Quaternion.identity);
        Rigidbody2D boomRb = boom.GetComponent<Rigidbody2D>();
        boomRb.velocity = dir * throwForce;
        boomRb.AddTorque(-dir.x * 20f);

        GameObject effect = Instantiate(appearEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        isThrowing = false;
    }
}
