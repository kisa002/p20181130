using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCont : MonoBehaviour {
    public float moveSpeed;
    public bool fast;
    public bool donMove;


    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 )
        {
            transform.position += moveSpeed * Vector3.right * Time.deltaTime;
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.position += moveSpeed * Vector3.left * Time.deltaTime;
        }
    }
}