using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserActivate : Trap
{
    [SerializeField] GameObject laser;
    [SerializeField] GameObject warning;

    public override bool IsActivate()
    {
        return Vector2.Distance(transform.position, laser.transform.position) >= 0.1f || warning.activeSelf;
    }

    protected override void Activate()
    {
        StartCoroutine(ActivateLaserCO());
    }

    IEnumerator ActivateLaserCO()
    {
        warning.SetActive(true);
        yield return new WaitForSeconds(2f);
        warning.SetActive(false);
        laser.GetComponent<Laser>().StartMoving();
    }
}
