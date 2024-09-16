using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] LayerMask trapLayer;
    [SerializeField] GameObject appearEffect;

    public void Respawn(GameObject player, GameObject playerSoul)
    {
        StartCoroutine(ReSparwnCO(player, playerSoul));
    }

    IEnumerator ReSparwnCO(GameObject player, GameObject playerSoul)
    {
        int r;
        do
        {
            r = Random.Range(0, points.Length);
        } while (Physics2D.OverlapBox(points[r].position, Vector2.one / 0.5f, 0, trapLayer));

        GameObject effect = Instantiate(appearEffect, points[r].position + Vector3.up / 2, Quaternion.identity);
        Destroy(effect, 1f);

        GameObject ps = Instantiate(playerSoul, player.transform.position + Vector3.up / 2, Quaternion.identity);
        ps.GetComponent<PlayerSoul>().SetData(player.transform.position + Vector3.up / 2, points[r].position + Vector3.up / 2);

        yield return new WaitForSeconds(0.5f);
        player.SetActive(true);
        player.transform.position = points[r].position;
    }
}
