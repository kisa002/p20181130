using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRoll : MonoBehaviour {

	
    void FixedUpdate()
    {
       
        Roll();
    }

    void Roll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            
                transform.Rotate(0, 0, 15);
            
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            
                transform.Rotate(0, 0, -15);
        }
    }
}
