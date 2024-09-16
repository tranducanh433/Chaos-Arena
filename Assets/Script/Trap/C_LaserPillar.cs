using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_LaserPillar : TrapController
{
    int counter = 0;

    void Start()
    {
        StartFunc();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            activateCD -= Time.deltaTime;
            if (activateCD <= 0)
            {
                if (counter < 3)
                {
                    RandomTrap().ActivateTrap();
                    counter++;
                }
                else
                {
                    counter = 0;
                    Trap trap = FindTrapNearPlayer(new Vector2(5, 25));
                    if (trap != null)
                        trap.ActivateTrap();
                    else
                        RandomTrap().ActivateTrap();
                }
                activateCD = activateTime;
            }
        }
    }
}
