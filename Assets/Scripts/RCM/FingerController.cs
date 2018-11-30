using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerController : MonoBehaviour {

    public GameObject stone;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        stone = collision.gameObject;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        stone = null;
    }
}
