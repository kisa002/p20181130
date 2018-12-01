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

    public bool isReady = false;

    public int moveSpeed;
    public int id = 0;

    public int playerNumber = -1;

    public SKILL skill;

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
                transform.position = new Vector3(-7, 0, -1);
            }
            else
            {
                id = 2;
                transform.position = new Vector3(7, 0, -1);

                transform.rotation = Quaternion.Euler(Vector3.up * 180);
            }
        }
        else
        {
            if (photonView.isMine)
            {
                id = 2;
                transform.position = new Vector3(7, 0, -1);

                transform.rotation = Quaternion.Euler(Vector3.up * 180);    
            }
            else
            {
                id = 1;
                transform.position = new Vector3(-7, 0, -1);
            }
        }

        if(skill)
            skill = GetComponent<SKILL>();

        if (id == 2 && NetworkManager.Instance.isPlay)
            arm[0].transform.parent.position = new Vector3(arm[0].transform.parent.position.x, arm[0].transform.parent.position.y, 0);

        if (PhotonNetwork.isMasterClient && NetworkManager.Instance.isPlay)
            StartCoroutine(CorSpawnStone());

        //Cursor.lockState = CursorLockMode.Confined
    }
	
	void Update ()
    {
        // 자기 자신 플레이어만 움직이도록
        if (!photonView.isMine)
            return;

        if (NetworkManager.Instance.isPlay == false)
            return;

        MoveArm();
        MovePlayer();

        if (Input.GetKey(KeyCode.Insert) && Input.GetKey(KeyCode.Delete))
            GameEnd(1);

        if (Input.GetKey(KeyCode.Home) && Input.GetKey(KeyCode.End))
            GameEnd(2);
    }

    void MoveArm()
    {
        if (prevPosY == 0)
            prevPosY = Input.mousePosition.y;

        currentPosY = Input.mousePosition.y;
        arm[2].transform.Rotate(0, 0, (currentPosY - prevPosY) * Time.deltaTime * 3);

        //Debug.Log($"Prev:{prevPosX} / Current: {currentPosX}");


        if (prevPosX == 0)
            prevPosX = Input.mousePosition.x;

        currentPosX = Input.mousePosition.x;
        arm[0].transform.Rotate(0, 0, (currentPosX - prevPosX) * Time.deltaTime * 3);

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
            //arm[1].transform.Rotate(0, 0, 15);

            Quaternion rot;
            rot = arm[1].transform.rotation;

            rot = Quaternion.Euler(0, 0, rot.eulerAngles.z + 50);
            
            arm[1].transform.rotation = Quaternion.Slerp(arm[1].transform.rotation, rot, Time.deltaTime * 4);
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            //arm[1].transform.Rotate(0, 0, -15);

            Quaternion rot;
            rot = arm[1].transform.rotation;

            rot = Quaternion.Euler(0, 0, rot.eulerAngles.z - 50);

            arm[1].transform.rotation = Quaternion.Slerp(arm[1].transform.rotation, rot, Time.deltaTime * 4);
        }
        if (Input.GetAxis("Mouse") > 0f)
        {
            //finger[0].transform.Rotate(0, 0, 15);

            Quaternion rot;
            rot = finger[0].transform.rotation;

            rot = Quaternion.Euler(0, 0, rot.eulerAngles.z + 50);

            finger[0].transform.rotation = Quaternion.Slerp(finger[0].transform.rotation, rot, Time.deltaTime);
        }

        else if (Input.GetAxis("Mouse") < 0f)
        {
            Quaternion rot;
            rot = finger[0].transform.rotation;

            rot = Quaternion.Euler(0, 0, rot.eulerAngles.z - 50);

            finger[0].transform.rotation = Quaternion.Slerp(finger[0].transform.rotation, rot, Time.deltaTime);
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

    public void SendFinger()
    {
        float[] rot = new float[5];

        for (int i = 0; i < 5; i++)
            rot[i] = finger[i].transform.rotation.eulerAngles.z;

        photonView.RPC("PunSendFinger", PhotonTargets.All, rot[0], rot[1], rot[2], rot[3], rot[4]);
    }

    [PunRPC]
    public void PunSendFinger(float a, float b, float c, float d, float e)
    {
        float p = id == 1 ? 0 : 180;

        Quaternion[] rot = new Quaternion[5];
        rot[0] = Quaternion.Euler(new Vector3(0, p, a));
        rot[1] = Quaternion.Euler(new Vector3(0, p, b));
        rot[2] = Quaternion.Euler(new Vector3(0, p, c));
        rot[3] = Quaternion.Euler(new Vector3(0, p, d));
        rot[4] = Quaternion.Euler(new Vector3(0, p, e));

        for (int i = 0; i < 5; i++)
            finger[i].transform.rotation = Quaternion.Lerp(finger[i].transform.rotation, rot[i], 10f);
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
                if(photonView.isMine)
                    skill.SoulStone(); // M 공격 1회 방어
                break;

            case 1:
                if (!photonView.isMine)
                    skill.PowerStone(); // A 상대 5초 멈춤
                break;

            case 2:
                if (photonView.isMine)
                    skill.MindStone(); // M 수전증 감소
                break;

            case 3:
                if (!photonView.isMine)
                    skill.SpaceStone(); // A 돌떨굼
                break;

            case 4:
                if (!photonView.isMine)
                    skill.RealStone(); // A 색바꿈
                break;

            case 5:
                if (photonView.isMine)
                    skill.TimeStone(); // M 속도빨라짐
                break;
        }
    }

    public void SelectPlayer(int x, int y)
    {
        playerNumber = y;
        photonView.RPC("PunSelectPlayer", PhotonTargets.AllBuffered, x, y);
    }

    [PunRPC]
    public void PunSelectPlayer(int x, int y)
    {
        if (x == 0)
        {
            UIManager.Instance.btnPlayer[y - 1].interactable = false;

            var color = UIManager.Instance.btnPlayer[y - 1].colors;

            if (photonView.isMine)
                color.disabledColor = new Color(0.02516699f, 0.945098f, 0.01176471f, 0.5019608f);
            else
                color.disabledColor = new Color(0.945098f, 0.01176471f, 0.02063006f, 0.5019608f);

            UIManager.Instance.btnPlayer[y - 1].colors = color;
        }
        else
        {
            UIManager.Instance.btnPlayer[x - 1].interactable = true;
            UIManager.Instance.btnPlayer[y - 1].interactable = false;

            var color = UIManager.Instance.btnPlayer[y - 1].colors;

            if (photonView.isMine)
                color.disabledColor = new Color(0.02516699f, 0.945098f, 0.01176471f, 0.5019608f);
            else
                color.disabledColor = new Color(0.945098f, 0.01176471f, 0.02063006f, 0.5019608f);

            UIManager.Instance.btnPlayer[y - 1].colors = color;
        }
    }

    public void GameReady()
    {
        photonView.RPC("PunGameReady", PhotonTargets.All);
    }

    [PunRPC]
    public void PunGameReady()
    {
        if (isReady)
            return;

        isReady = true;
        NetworkManager.Instance.readyCount++;

        if (NetworkManager.Instance.readyCount == 2)
            GameStart();
    }

    public void GameStart()
    {
        photonView.RPC("PunGameStart", PhotonTargets.All);
    }

    [PunRPC]
    public void PunGameStart()
    {
        NetworkManager.Instance.isPlay = true;
        PhotonNetwork.LoadLevel("InGame");

        Cursor.visible = false;
    }

    public void GameEnd(int id)
    {
        photonView.RPC("PunGameEnd", PhotonTargets.All, id);
    }

    [PunRPC]
    public void PunGameEnd(int id)
    {
        UIManager.Instance.ShowResult(id);
    }

    IEnumerator CorSpawnStone()
    {
        int type = Random.Range(0, 6);
        float time = Random.Range(0.5f, 2f);

        float x = Random.Range(-4, 4);

        Vector3 pos = new Vector3(x, 6, 0);

        Quaternion rot;
        rot = Quaternion.Euler(0, 0, Random.Range(0, 359));

        PhotonNetwork.Instantiate("Stone" + type, pos, rot, 0);

        yield return new WaitForSeconds(time);
        StartCoroutine(CorSpawnStone());
    }

    public void SpawnStone(int type)
    {
        PhotonNetwork.Instantiate("Stone" + type, Vector3.zero, Quaternion.identity, 0);
    }

    public void NetworkDestroy(GameObject obj)
    {
        PhotonNetwork.Destroy(obj);
    }
}
