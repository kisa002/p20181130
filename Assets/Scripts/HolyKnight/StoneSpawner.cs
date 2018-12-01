using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
	void Start ()
    {
        StartCoroutine(CorSpawnStone());
	}
	
	IEnumerator CorSpawnStone()
    {
        int type = Random.Range(0, 7);
        float time = Random.Range(0.5f, 2f);

        NetworkManager.Instance.player.SpawnStone(type);

        yield return new WaitForSeconds(time);
    }
}
