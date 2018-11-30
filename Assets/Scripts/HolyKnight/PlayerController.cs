using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PhotonView photonView;

    public int id = 0;

    public GameObject[] finger = new GameObject[2];
    public GameObject[] arm = new GameObject[2];

    void Start ()
    {
        if (PhotonNetwork.isMasterClient)
        {
            if(photonView.isMine)
            {
                id = 1;
                transform.position = Vector2.left * 7;
            }
            else
            {
                id = 2;
                transform.position = Vector2.right * 7;

                transform.rotation = Quaternion.Euler(Vector3.up * 180);
            }
        }
        else
        {
            if (photonView.isMine)
            {
                id = 2;
                transform.position = Vector2.right * 7;

                transform.rotation = Quaternion.Euler(Vector3.up * 180);    
            }
            else
            {
                id = 1;
                transform.position = Vector2.left * 7;
            }
        }
	}
	
	void Update ()
    {
        if (!photonView.isMine)
            return;

        MoveArm();
    }

    void MoveArm()
    {
        var x = Input.GetAxis("Horizontal") * 5 * Time.deltaTime;
        var y = Input.GetAxis("Vertical") * 17 * Time.deltaTime;

        if (id == 1)
        {
            arm[0].transform.parent.Translate(Vector2.right * x);
            arm[1].transform.Rotate(Vector3.back * y);

            SendArm();
        }
        else
        {
            arm[0].transform.parent.Translate(Vector2.right * -x);
            arm[1].transform.Rotate(Vector3.back * -y);

            SendArm();
        }
    }

    public void SendFinger(int type)
    {
        photonView.RPC("PunSendFinger", PhotonTargets.All, type, finger[type].transform.position, finger[type].transform.rotation);
    }

    [PunRPC]
    public void PunSendFinger(int type, Vector3 pos, Quaternion rot)
    {
        finger[type].transform.position = pos;
        finger[type].transform.rotation = rot;
    }

    public void SendArm()
    {
        photonView.RPC("PunSendArm", PhotonTargets.All, arm[0].transform.parent.position, arm[1].transform.rotation);
    }

    [PunRPC]
    public void PunSendArm(Vector3 pos, Quaternion rot)
    {
        arm[0].transform.parent.position = pos;
        arm[1].transform.rotation = rot;

        //Debug.Log(pos);
    }
}
