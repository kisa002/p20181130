using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextController : MonoBehaviour {
    public Text[] text = new Text[4];

    public List<string> listSentence = new List<string>();

    public string PlayerText;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void No1()
    {
       
        for (int j=0;j<4; j++)
        {
        int num = Random.Range(0, 4);
        bool isCheck = true;
        while(true)
        {
            for (int i = 0; i < 4; i++)
                if ((text[j].text == text[i].text && j != i) || text[i].text == listSentence[num])
                         isCheck = false;

            if (isCheck)
                break;
        }
         text[j].text = listSentence[num];
        }
        
        
    }
    
}
