using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : TrapController
{
    int counter = 0;

    void Start()
    {
        StartFunc();
    }


    void Update()
    {
        if (isActive)
        {
            activateCD -= Time.deltaTime;
            if(activateCD <= 0)
            {
                if(counter < 3)
                {
                    RandomTrap().ActivateTrap();
                    counter++;
                }
                else
                {
                    counter = 0;
                    Trap trap = FindTrapNearPlayer(new Vector2(30, 3));
                    if(trap != null)
                        trap.ActivateTrap();
                    else
                        RandomTrap().ActivateTrap();
                }
                activateCD = activateTime;
            }
        }
    }
}
