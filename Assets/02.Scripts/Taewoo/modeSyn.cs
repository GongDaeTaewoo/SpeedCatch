using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;



public class modeSyn : MonoBehaviourPunCallbacks, IPunObservable
{
    public static int mode = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //��� ���� ����ȭ ��庯���� ���� ��尡 �ٲ� �ٵ� ���常�ٲܼ��ִµ� ������ �ٲܰ�� ��ο��� ����Ǿ��ϹǷ� ����ȭ��Ŵ
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(mode);
        }
        else
        {
            // Network player, receive data
            //this.mode = (int)stream.ReceiveNext(); static������ �ٲٸ鼭 this���M
            mode = (int)stream.ReceiveNext();
        }
    }
}
