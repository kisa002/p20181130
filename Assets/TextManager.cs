using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour {
    public Text NormalText;
    

    public void SetText(string text)
    {
        NormalText.text = text;
    }

    public string GetText()
    {
        return NormalText.text;
    }

    
}
