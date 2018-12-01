using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
	void Start ()
    {
        NetworkManager.Instance.SpawnPlayer(NetworkManager.Instance.player.playerNumber);
    }
}