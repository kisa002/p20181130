using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject panelMain, panelSelect;
    public Button[] btnPlayer = new Button[4];

    public GameObject panelResult;
    public Image imgEnding;
    public Image imgResult;

    private void Awake()
    {
        if (UIManager.Instance == null)
            UIManager.Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
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

    public void ShowResult(int id)
    {
        panelResult = GameObject.Find("Canvas").transform.Find("Panel Result").gameObject;
        panelResult.SetActive(true);

        imgResult = panelResult.transform.Find("ImgResult").GetComponent<Image>();
        
        bool isWin = NetworkManager.Instance.player.id == id ? true : false;

        if (isWin)
            imgResult.sprite = Resources.Load<Sprite>("Sprites/Result/Win");
        else
            imgResult.sprite = Resources.Load<Sprite>("Sprites/Result/Defeat");

        ShowAnimation(4); //여기다가 승자의 PlayerNumber 넣어주면 돼
    }

    IEnumerator CorShowResult()
    {
        for (int i = 0; i < 100; i++)
        {
            imgResult.color = new Color(1, 1, 1, i * 0.01f);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1f);

        Button btn;
        btn = panelResult.transform.Find("BtnRestart").GetComponent<Button>();

        for (int i = 0; i < 100; i++)
        {
            btn.image.color = new Color(1, 1, 1, i * 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void ShowAnimation(int number)
    {
        string[] playerName = new string[4];
        playerName[0] = "Thanos";
        playerName[1] = "SymYoung";
        playerName[2] = "UnityChan";
        playerName[3] = "Ahri";
        
        imgEnding = panelResult.transform.Find("ImgEnding").GetComponent<Image>();

        StartCoroutine(CorEndingAnimation(playerName[number - 1]));
    }

    IEnumerator CorEndingAnimation(string playerName)
    {
        Sprite spr;

        for (int i = 0; ; i++)
        {
            Debug.Log("Sprites/Ending/" + playerName + i);

            spr = Resources.Load<Sprite>("Sprites/Ending/" + playerName + "/" + i);

            if (spr == null)
                break;

            imgEnding.sprite = spr;

            yield return new WaitForSeconds(.5f);
        }

        yield return new WaitForSeconds(2f);
        imgEnding.gameObject.SetActive(false);

        StartCoroutine(CorShowResult());
    }

    public void GameRestart()
    {
        Application.Quit();
    }
}