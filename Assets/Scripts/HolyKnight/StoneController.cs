using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    private PhotonView photonView;

    private Vector3 selfPos;
    private Quaternion selfRot;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!photonView.isMine)
            SmoothMove();
    }

    private void SmoothMove()
    {
        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
        transform.rotation = Quaternion.Lerp(transform.rotation, selfRot, Time.deltaTime * 8);
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            selfPos = (Vector3)stream.ReceiveNext();
            selfRot = (Quaternion)stream.ReceiveNext();
            //transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
