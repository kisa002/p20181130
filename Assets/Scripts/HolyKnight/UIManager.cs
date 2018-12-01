using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject panelMain, panelSelect;
    public Button[] btnPlayer = new Button[4];

    private void Awake()
    {
        if (UIManager.Instance == null)
            UIManager.Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowMain()
    {
        panelMain.SetActive(true);
        panelSelect.SetActive(false);
    }

    public void ShowSelect()
    {
        panelSelect.SetActive(true);
        panelMain.SetActive(false);
    }

    public void SelectPlayer(int number)
    {
        //btnPlayer[NetworkManager.Instance.player.playerNumber].interactable = true;
        //btnPlayer[number].interactable = false;

        //Debug.Log($"A {NetworkManager.Instance.player.playerNumber} {number}");
        NetworkManager.Instance.player.SelectPlayer(NetworkManager.Instance.player.playerNumber, number);
    }
}
