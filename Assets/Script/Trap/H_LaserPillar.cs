using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_LaserPillar : Trap
{
    [SerializeField] T_LaserPillar laserPillar;
    [SerializeField] GameObject warning;
    [SerializeField] float timeWait;
    [SerializeField] ParticleSystem chargingEffect;

    public override bool IsActivate()
    {
        return laserPillar.gameObject.activeSelf == true || warning.activeSelf == true;
    }

    protected override void Activate()
    {
        StartCoroutine(ActivateTrapCO());
    }

    IEnumerator ActivateTrapCO()
    {
        chargingEffect.Play();
        warning.SetActive(true);
        yield return new WaitForSeconds(timeWait);
        chargingEffect.Stop();
        warning.SetActive(false);
        laserPillar.gameObject.SetActive(true);
    }
}
