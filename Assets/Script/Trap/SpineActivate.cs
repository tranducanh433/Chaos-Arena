using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineActivate : Trap
{
    [SerializeField] float timeWait = 2;
    [SerializeField] GameObject trap;
    [SerializeField] GameObject warning;

    IEnumerator SetTrapCO(GameObject trap)
    {
        yield return new WaitForSeconds(timeWait);
        warning.SetActive(false);
        trap.SetActive(true);
    }

    protected override void Activate()
    {
        warning.SetActive(true);
        StartCoroutine(SetTrapCO(trap));
    }

    public override bool IsActivate()
    {
        return warning.activeSelf == true || trap.activeSelf == true;
    }
}
