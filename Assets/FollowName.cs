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
        rigs = GameObject.FindGameObjectsWithTag("rig");//���󰡴� ���׸� ���ؾ��ϴµ� ismine�� ���θ��׸� ���󰡰���
        if (rigs[0].GetComponent<PhotonView>().IsMine)
            following = rigs[0];
        if (rigs[1].GetComponent<PhotonView>().IsMine)  //�̰� ���߽ÿ��� ���װ� �� �ڱ�Ŷ� �ϳ����� �� �׽�Ʈ�� ���װ� ���ڲ� �Ǹ� ���ڸ�����
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
