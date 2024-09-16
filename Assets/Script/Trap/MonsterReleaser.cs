using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterReleaser : Trap
{
    [SerializeField] Monster monster;
    [SerializeField] GameObject appearEffect;
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;

    bool onEffect = false;

    public override bool IsActivate()
    {
        return monster.gameObject.activeSelf == true || onEffect == true;
    }

    protected override void Activate()
    {
        StartCoroutine(SummonMonster());
        onEffect = true;
    }

    IEnumerator SummonMonster()
    {
        int r = Random.Range(0, 2);
        GameObject effect;
        if (r == 0)
            effect = Instantiate(appearEffect, point1.position, Quaternion.identity);
        else
            effect = Instantiate(appearEffect, point2.position, Quaternion.identity);

        Destroy(effect, 1.5f);

        yield return new WaitForSeconds(1f);

        monster.gameObject.SetActive(true);
        if (r == 0)
        {
            monster.SetData(point1.position, false);
        }
        else
        {
            monster.SetData(point2.position, true);
        }
        onEffect = false;
    }
}
