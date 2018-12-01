using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKILL : MonoBehaviour {
    public StoneColor stoneColor;
    public bool Donotmove = false;
    public FingerController finger;
    public MouseCont move;
    public bool guard = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SpaceStone()
    {
        if (!guard) {
           finger.stone = null;
        }
            
        
    }
    public void TimeStone()
    {
        move.fast = true;
        StartCoroutine("TimeCoroutine");
    }
    public void MindStone()
    {

    }
    public void PowerStone()
    {
        if (!guard)
        {
            StartCoroutine("PowerCoroutine");
        }
    }
    public void SoulStone()
    {
        if (!guard)
        {
            guard = true;
        }
    }
    public void RealStone()
    {
        if (!guard)
        {
            while (true)
            {
            string S = stoneColor.StoneName;
            int i = Random.Range(0, 6);
            if (i == 0 && S != "Soul")
            {
                    stoneColor.StoneName = "Soul";
                    break;
            }
            else if (i == 1 && S != "Mind")
            {
                    stoneColor.StoneName = "Mind";
                    break;
             }
            else if (i ==  2 && S != "Power")
            {
                    stoneColor.StoneName = "Power";
                    break;
            }
            else if (i == 3 && S != "Space")
            {
                    stoneColor.StoneName = "Space";
                    break;
                }
            else if (i == 4 && S != "Time")
            {
                    stoneColor.StoneName = "Time";
                    break;
                }
            else if (i == 5 && S != "Real")
            {

                    stoneColor.StoneName = "Real";
                    break;
                }
            }
            
        }
    }
    IEnumerator TimeCoroutine()
    {
        yield return new WaitForSeconds(5f);
        move.fast = false;
    }
    IEnumerator PowerCoroutine()
    {
        move.donMove = true;
        yield return new WaitForSeconds(5f);
        move.donMove = false;
    }
}
