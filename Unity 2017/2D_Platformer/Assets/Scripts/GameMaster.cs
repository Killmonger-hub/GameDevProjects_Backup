using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public static GameMaster gm;

    void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public IEnumerator RespawnPlayer()
    {
        Debug.Log("TODO: Play Spawn Sound");
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("TODO: Add Spawn Particles");
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }
}
