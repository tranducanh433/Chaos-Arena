using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomController : TrapController
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
        }
    }
}
