using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    public void ActivateTrap()
    {
        gameObject.SetActive(true);
        Activate();
    }

    protected abstract void Activate();
    public abstract bool IsActivate();
}
