using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_FireBall : Trap
{
    [SerializeField] FireBall fireBall;
    [SerializeField] GameObject appearEffect;

    bool onClode = false;

    public override bool IsActivate()
    {
        return fireBall.gameObject.activeSelf == true || onClode == true;
    }

    protected override void Activate()
    {
        onClode = true;
        StartCoroutine(CloneFireBallCO());
    }

    IEnumerator CloneFireBallCO()
    {
        GameObject effect = Instantiate(appearEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
        yield return new WaitForSeconds(1f);
        fireBall.gameObject.SetActive(true);
        fireBall.transform.position = transform.position;
        Vector2[] dirs = { new Vector2(1, 1), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(-1, -1) };
        int r = Random.Range(0, dirs.Length);
        fireBall.SetData(dirs[r].normalized);
        onClode = false;
    }
}
