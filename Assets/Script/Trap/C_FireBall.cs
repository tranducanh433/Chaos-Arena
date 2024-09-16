using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FireBall : TrapController
{
    // Start is called before the first frame update
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
            if(activateCD <= 0)
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
