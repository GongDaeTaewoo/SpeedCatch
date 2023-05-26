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
    //모드 변수 동기화 모드변수에 따라 모드가 바뀜 근데 방장만바꿀수있는데 방장이 바꿀경우 모두에게 적용되야하므로 동기화시킴
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
            //this.mode = (int)stream.ReceiveNext(); static변수로 바꾸면서 this를뻄
            mode = (int)stream.ReceiveNext();
        }
    }
}
