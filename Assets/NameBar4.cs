using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class NameBar4 : MonoBehaviourPunCallbacks
{
    public TextMeshPro ScriptTxt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void username()
    {
        ScriptTxt.text = PhotonNetwork.PlayerList[3].NickName;
    }
}
