using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PhotonView photonView;
    float currentPosY;
    float currentPosX;
    float prevPosY;
    float prevPosX;
    public int moveSpeed;
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
	
	void FixedUpdate ()
    {
        // 자기 자신 플레이어만 움직이도록
        if (!photonView.isMine)
            return;

        MoveArm();
        MovePlayer();
    }

    void MoveArm()
    {
        if (prevPosY == 0)
            prevPosY = Input.mousePosition.y;

        currentPosY = Input.mousePosition.y;
        arm[2].transform.Rotate(0, 0, (currentPosY - prevPosY) / 15);

        //Debug.Log($"Prev:{prevPosX} / Current: {currentPosX}");


        if (prevPosX == 0)
            prevPosX = Input.mousePosition.x;

        currentPosX = Input.mousePosition.x;
        arm[0].transform.Rotate(0, 0, (currentPosX - prevPosX) / 15);
        


        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.position += moveSpeed * Vector3.right * Time.deltaTime;
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.position += moveSpeed * Vector3.left * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {

            arm[1].transform.Rotate(0, 0, 15);

        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {

            arm[1].transform.Rotate(0, 0, -15);
        }
        if (Input.GetAxis("Mouse") > 0f)
        {

            finger[0].transform.Rotate(0, 0, 15);

        }

        else if (Input.GetAxis("Mouse") < 0f)
        {

            finger[0].transform.Rotate(0, 0, -15);
        }
        if (Input.GetAxisRaw("WE") > 0f)
        {

            finger[3].transform.Rotate(0, 0, 5);

        }

        else if (Input.GetAxisRaw("WE") < 0f)
        {

            finger[3].transform.Rotate(0, 0, -5);
        }
        prevPosX = currentPosX;
        prevPosY = currentPosY;

        SendArm();
    }

    void MovePlayer()
    {
        float x = Input.GetKey(KeyCode.LeftArrow) == true ? -1 : 0;
        x = Input.GetKey(KeyCode.RightArrow) == true ? 1 : x;
        
        if(id == 1)
            transform.Translate(Vector2.right * 2 * x * Time.deltaTime);
        else
            transform.Translate(Vector2.left * 2 * x * Time.deltaTime);

        SendMovePlayer();
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

    public void SendMovePlayer()
    {
        photonView.RPC("PunMovePlayer", PhotonTargets.All, transform.position);
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
