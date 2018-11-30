using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCont : MonoBehaviour {
    Vector3 Distance;
    float PositionX;
    float PositionY;
    public bool fast;
    public bool donMove;
    bool isDrag = false;

    /*
    private void OnMouseDown()
    {
        Distance = Camera.main.WorldToScreenPoint(transform.position);
        PositionX = Input.mousePosition.x - Distance.x;
        PositionX = Input.mousePosition.y - Distance.y;

        Debug.Log("AA");
    }
    private void OnMouseDrag()
    {
        Vector3 CurrentPosition = new Vector3(Input.mousePosition.x - PositionX, Input.mousePosition.y - PositionY, Distance.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(CurrentPosition);
        transform.position = worldPosition;
    }
    */
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
            if (!isDrag)
            {
                Distance = Camera.main.WorldToScreenPoint(transform.position);
                PositionX = Input.mousePosition.x - Distance.x;
                PositionY = Input.mousePosition.y - Distance.y;

                isDrag = true;
            }
            else
            {
                Vector3 CurrentPosition;
                if (fast)
                {
                CurrentPosition = new Vector3(Input.mousePosition.x - PositionX * 1.2f, Distance.y, Distance.z);
                }
                else
                {
                CurrentPosition = new Vector3(Input.mousePosition.x - PositionX, Distance.y, Distance.z);
                }
                
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(CurrentPosition);
                if(!donMove)
                transform.position = worldPosition;
            }
        else
            isDrag = false;
	}
}
