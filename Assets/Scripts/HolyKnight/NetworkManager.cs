using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    public string version = "0.1";

    public bool isPlay = false;
    public PlayerController player;

    private void Awake()
    {
        if (NetworkManager.Instance == null)
            NetworkManager.Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ConnectToPhoton();
    }

    #region Connect
    public void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings(version);

        Debug.Log("포톤 접속 중..!");
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);

        Debug.Log("마스터 접속 완료");
    }

    private void OnJoinedLoby()
    {
        Debug.Log("로비 접속 완료");
    }

    private void OnDisconnectedFromPhoton()
    {
        Debug.Log("포톤 접속 해제");
    }
    #endregion

    #region Create & Join Room
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 2
        };

        PhotonNetwork.CreateRoom("test", roomOptions, null);
    }

    public void JoinCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom("gameName" + Random.Range(1, 10000) + " " + Random.Range(1, 10000), roomOptions, TypedLobby.Default);
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 2 }, null);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("test");
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    private void OnJoinedRoom()
    {
        SpawnPlayer();
        //PhotonNetwork.LoadLevel("InGame");
        Debug.Log("방 접속 완료");
    }
    #endregion

    #region InGame
    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"NAME: {scene.name}");
        switch (scene.name)
        {
            case "InGame":
                SpawnPlayer();
                break;
        }
    }

    public void SpawnPlayer()
    {
        PhotonNetwork.Instantiate("Player3", Vector3.zero, Quaternion.identity, 0);
    }
    #endregion
}
