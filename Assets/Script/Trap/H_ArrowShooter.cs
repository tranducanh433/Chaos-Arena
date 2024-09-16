using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_ArrowShooter : Trap
{
    [SerializeField] T_ArrowShooter arrowShooter;

    public override bool IsActivate()
    {
        return arrowShooter.gameObject.activeSelf == true;
    }

    protected override void Activate()
    {
        arrowShooter.gameObject.SetActive(true);
        Vector2 startPos = new Vector2(transform.position.x, Random.Range(-5f, 5f));
        float dir = Random.Range(-1f, 1f);
        arrowShooter.SetData(startPos, dir);
    }
}
