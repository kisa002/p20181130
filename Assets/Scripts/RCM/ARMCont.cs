using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMCont : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void FixedUpdate()
    {
        Roll();
    }

    // Update is called once per frame
    void Roll()
    {
        if (Input.GetAxis("Mouse") > 0f)
        {

            transform.Rotate(0, 0, 15);

        }

        else if (Input.GetAxis("Mouse") < 0f)
        {

            transform.Rotate(0, 0, -15);
        }
    }
}
