using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEffect : MonoBehaviour
{
    [SerializeField] GameObject effect;

    public void CloneEffect()
    {
        GameObject e = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(e, 2f);
    }
}
