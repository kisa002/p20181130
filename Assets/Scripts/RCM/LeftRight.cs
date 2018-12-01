using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRight : MonoBehaviour {
    float currentPos;
    float prevPos;

    private void Start()
    {
        currentPos = Input.mousePosition.x;
    }

    void FixedUpdate()
    {
        currentPos = Input.mousePosition.x;
        transform.Rotate(0, 0, (currentPos - prevPos) / 3);
        prevPos = currentPos;

    }
}
