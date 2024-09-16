using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoul : MonoBehaviour
{
    [SerializeField] float speed = 20;
    Vector2 targetPos;
    Vector2 startPos;

    public void SetData(Vector2 start, Vector2 target)
    {
        startPos = start;
        targetPos = target;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(startPos, targetPos);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, (distance / 0.75f) * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, targetPos) <= 0.1f) Destroy(gameObject, 0.5f);
    }
}
