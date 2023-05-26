using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class CameraOff : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine==false)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
