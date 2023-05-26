using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
public class FollowName : MonoBehaviourPun
{
    public PhotonView pv;
    public GameObject following;
    public GameObject[] rigs;
    // Start is called before the first frame update
    void Start()
    {
        rigs = GameObject.FindGameObjectsWithTag("rig");//따라가는 리그를 정해야하는데 ismine이 참인리그를 따라가게함
        if (rigs[0].GetComponent<PhotonView>().IsMine)
            following = rigs[0];
        if (rigs[1].GetComponent<PhotonView>().IsMine)  //이거 개발시에는 리그가 다 자기거라서 하나한테 쏠림 테스트시 리그가 각자꼐 되면 각자를따라감
            following = rigs[1];
        if (rigs[2].GetComponent<PhotonView>().IsMine)
            following = rigs[2];
        if (rigs[3].GetComponent<PhotonView>().IsMine)
            following = rigs[3];
    }

    // Update is called once per frame
    void Update()
    {



        transform.position = following.transform.position;
        transform.rotation = following.transform.rotation;
        transform.localScale = following.transform.localScale;

    }
}
