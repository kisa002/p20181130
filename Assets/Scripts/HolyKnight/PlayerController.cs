using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PhotonView photonView;

    public int id = 0;

    public GameObject[] finger = new GameObject[5];
    public GameObject[] arm = new GameObject[3];

    void Start ()
    {
        // 캐릭터 생성 시 ID
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
        // 자기 자신 플레이어만 움직이도록
        if (!photonView.isMine)
            return;

        MoveArm();
        MovePlayer();
    }

    void MoveArm()
    {
        float[] rot = new float[3];

        if (Input.GetKey(KeyCode.Q))
            rot[0] = 35f * Time.deltaTime;
        else
            if (Input.GetKey(KeyCode.E))
                rot[0] = -35f * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            rot[1] = 35f * Time.deltaTime;
        else
            if (Input.GetKey(KeyCode.A))
                rot[1] = -35f * Time.deltaTime;

        if (Input.GetKey(KeyCode.C))
             rot[2] = 35f * Time.deltaTime;
        else
            if (Input.GetKey(KeyCode.Z))
              rot[2] = -35f * Time.deltaTime;

        if(rot[0] != 0 || rot[1] != 0 || rot[2] != 0)
        {
            arm[0].transform.Rotate(Vector3.forward * rot[0]);
            arm[1].transform.Rotate(Vector3.forward * rot[1]);
            arm[2].transform.Rotate(Vector3.forward * rot[2]);

            SendArm();
        }
    }

    void MovePlayer()
    {
        float x = Input.GetKey(KeyCode.LeftArrow) == true ? -1 : 0;
        x = Input.GetKey(KeyCode.RightArrow) == true ? 1 : x;
        
        if(id == 1)
            transform.Translate(Vector2.right * 2 * x * Time.deltaTime);
        else
            transform.Translate(Vector2.left * 2 * x * Time.deltaTime);

        photonView.RPC("PunMovePlayer", PhotonTargets.All, transform.position);
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
        photonView.RPC("PunSendArm", PhotonTargets.All, arm[0].transform.rotation.eulerAngles.z, arm[1].transform.rotation.eulerAngles.z, arm[2].transform.rotation.eulerAngles.z);
    }

    [PunRPC]
    public void PunSendArm(float x, float y, float z)
    {
        if (photonView.isMine)
            return;

        float p = id == 1 ? 0 : 180;

        Quaternion[] rot = new Quaternion[3];
        rot[0] = Quaternion.Euler(new Vector3(0, p, x)); 
        rot[1] = Quaternion.Euler(new Vector3(0, p, y));
        rot[2] = Quaternion.Euler(new Vector3(0, p, z));

        for (int i = 0; i < 3; i++)
            arm[i].transform.rotation = Quaternion.Lerp(arm[i].transform.rotation, rot[i], 10f);
    }

    [PunRPC]
    public void PunMovePlayer(Vector3 pos)
    {
        if (photonView.isMine)
            return;

        transform.position = Vector3.Lerp(transform.position, pos, 10);
    }

    public void UseSkill(int id)
    {
        photonView.RPC("PunUseSkill", PhotonTargets.All, id);
    }

    [PunRPC]
    public void PunUseSkill(int id)
    {
        switch(id)
        {
            case 0:
                break;

            case 1:
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;

            case 5:
                break;
        }
    }
}
