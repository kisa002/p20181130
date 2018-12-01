using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updown : MonoBehaviour {
    float currentPos;
    float prevPos;
    void FixedUpdate()
    {
        currentPos = Input.mousePosition.y;
        transform.Rotate(0, 0, (currentPos - prevPos) /3);
        prevPos = currentPos;
       
    } 
}

