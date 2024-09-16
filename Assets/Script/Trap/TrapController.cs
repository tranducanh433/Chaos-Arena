using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [Header("Trap Setting")]
    [SerializeField] protected float activateTime;
    [SerializeField] protected float playerTargetTime;
    [SerializeField] protected int startNumOfTrap;
    [SerializeField] protected LayerMask playerMask;
    [Header("Trap Object")]
    [SerializeField] protected Trap[] traps;

    protected int numOfTrap;
    protected float activateCD;
    protected float targetCD;
    protected bool isActive = false;

    protected void StartFunc()
    {
        numOfTrap = startNumOfTrap;
        //activateCD = activateTime;
        targetCD = playerTargetTime;
    }
    public void ActiveTrap()
    {
        isActive = true;
    }
    public void InactiveTrap()
    {
        isActive = false;
    }
    public bool IsActivate()
    {
        return isActive;
    }
    public void IncreaseNumberOfTrap()
    {
        numOfTrap++;
    }
    public void ResetNumberOfTrap()
    {
        numOfTrap = startNumOfTrap;
    }

    public Trap RandomTrap()
    {
        int r;
        do
        {
            r = Random.Range(0, traps.Length);
        }
        while (traps[r].IsActivate() == true);
        return traps[r];
    }

    public Trap FindTrapNearPlayer(Vector2 size)
    {
        List<int> j = new List<int>();
        for (int i = 0; i < traps.Length; i++)
        {
            bool meetPlayer = Physics2D.OverlapBox(traps[i].transform.position, size, 0, playerMask);
            if (meetPlayer == true && traps[i].IsActivate() == false)
            {
                j.Add(i);
            }
        }
        if(j.Count == 0)
        {
            return null;
        }
        int r = Random.Range(0, j.Count);
        return traps[r];
    }
}
