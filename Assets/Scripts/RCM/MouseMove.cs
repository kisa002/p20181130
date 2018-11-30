using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 AfterMouse;
    Vector3 moveVelocity;
    public float movePower;

    public float dragSpeed = 2;
    private Vector3 dragOrigin;


    void Start()
    {
        //StartCoroutine("MouseSet");

        Debug.Log("CREATE");
    }
    public int cameraDragSpeed = 50;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float speed = cameraDragSpeed * Time.deltaTime;
            transform.position -= new Vector3(Input.GetAxis("Mouse X") * speed, 0, Input.GetAxis("Mouse Y") * speed);
        }
    }
    void FixedUpdate()
    {
        //Move();

        
    
    }


    void Move()
    {
        transform.position += movePower * moveVelocity * Time.deltaTime;
    }
    IEnumerator MouseSet()
       {
        mousePos = Input.mousePosition;
        yield return new WaitForSeconds(0.01f);
        AfterMouse = Input.mousePosition;
        
        if(AfterMouse.x > mousePos.x && AfterMouse.x < mousePos.x)
        {
            moveVelocity = Vector3.right;
        }
        else if (AfterMouse.x < mousePos.x )
        {
            moveVelocity = Vector3.left;
        }
        
        StartCoroutine("MouseSet");
    }

}

