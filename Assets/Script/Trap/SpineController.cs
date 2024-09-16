using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineController : TrapController
{
    private void Start()
    {
        StartFunc();
    }

    void Update()
    {
        if (isActive)
        {
            activateCD -= Time.deltaTime;
            if (activateCD <= 0)
            {
                for (int i = 0; i < numOfTrap; i++)
                {
                    RandomTrap().ActivateTrap();
                }

                activateCD = activateTime;
            }

            targetCD -= Time.deltaTime;
            if (targetCD <= 0)
            {
                Trap trap = FindTrapNearPlayer(new Vector2(2, 0.5f));
                if (trap != null)
                    trap.ActivateTrap();
                targetCD = playerTargetTime;
            }
        }
    }
}
