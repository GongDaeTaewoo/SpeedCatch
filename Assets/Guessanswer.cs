using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Guessanswer : MonoBehaviour
{
    public bool corrects = false;
    public Gamemanager gamemanager;
    public PhotonView pv;
    //������ ����ϴ� ui�Ҵ�
    public GameObject dap;
    public GameObject Fire;
    public Text correctperson;

    //�����ƹ��ų�
    //public PhotonView pv;
    //���ù���������
    public void ReadUserData(int outputNum, int player, string answer)
    {
        string k;

        FirebaseDatabase.DefaultInstance.GetReference("turn")
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {

                    Debug.Log("error");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("ok");
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    //wordsave���ڿ��ִ� ���� ������
                    //return snapshot.Child(outputNum.ToString()).Value;

                    //snapshot.Child(outputNum.ToString()).Value;
                    k = (string)snapshot.Child(outputNum.ToString()).Value;
                    if (k == answer)
                    {
                        if (player == 1)
                        {
                            Jumsu.Score1 += 1;
                            correctperson.text = PhotonNetwork.MasterClient.NickName + "���� ������ ���߼̽��ϴ�.";
                            corrects = true;
                            pv.RPC("UpdateScore1", RpcTarget.Others);
                        }
                        else if (player == 2)
                        {
                            Jumsu.Score2 += 1;
                            correctperson.text = PhotonNetwork.PlayerList[1].NickName + "���� ������ ���߼̽��ϴ�.";
                            corrects = true;

                            pv.RPC("UpdateScore2", RpcTarget.Others);
                        }
                        else if (player == 3)
                        {
                            Jumsu.Score3 += 1;
                            correctperson.text = PhotonNetwork.PlayerList[2].NickName + "���� ������ ���߼̽��ϴ�.";
                            corrects = true;

                            pv.RPC("UpdateScore3", RpcTarget.Others);
                        }
                        else if (player == 4)
                        {
                            Jumsu.Score4 += 1;
                            correctperson.text = PhotonNetwork.PlayerList[3].NickName + "���� ������ ���߼̽��ϴ�.";
                            corrects = true;

                            pv.RPC("UpdateScore4", RpcTarget.Others);
                        }
                    
                }

                    Debug.Log("���ù�����!");



                }

            });



    }
    // Start is called before the first frame update
    public void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void firstTurn()
    {
        if (gamemanager.turncount % PhotonNetwork.CurrentRoom.PlayerCount == 0)
        {
                     
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                Fire.GetComponent<CFirebase>().WriteUserData("1", dap.GetComponent<InputField>().text);
                
            }
            else
            {
                ReadUserData(1, PhotonNetwork.LocalPlayer.ActorNumber, dap.GetComponent<InputField>().text);
            }
        }
    }


    public void twoTurn()
    {
        if (gamemanager.turncount % PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.PlayerCount -1)
        {
            
            if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                    Fire.GetComponent<CFirebase>().WriteUserData("2", dap.GetComponent<InputField>().text);
                else
                {
                    ReadUserData(2, PhotonNetwork.LocalPlayer.ActorNumber, dap.GetComponent<InputField>().text);
                }
        }
    }
    public void threeTurn()
    {
       
        if (gamemanager.turncount % PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.PlayerCount - 2)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
                Fire.GetComponent<CFirebase>().WriteUserData("3", dap.GetComponent<InputField>().text);
            else    
                ReadUserData(3, PhotonNetwork.LocalPlayer.ActorNumber, dap.GetComponent<InputField>().text);
        }
    }
    public void fourTurn()
    {
       
        if (gamemanager.turncount % PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.PlayerCount - 3)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
                Fire.GetComponent<CFirebase>().WriteUserData("4", dap.GetComponent<InputField>().text);
            else
                ReadUserData(4, PhotonNetwork.LocalPlayer.ActorNumber, dap.GetComponent<InputField>().text);
        }
    }

    [PunRPC]
    public void UpdateScore1()
    {
        correctperson.text = PhotonNetwork.MasterClient.NickName + "���� ������ ���߼̽��ϴ�.";
        Jumsu.Score1 += 1;
        corrects = true;


    }
    [PunRPC]
    public void UpdateScore2()
    {
        correctperson.text = PhotonNetwork.PlayerList[1].NickName + "���� ������ ���߼̽��ϴ�.";
        Jumsu.Score2 += 1; 
        corrects = true;

    }
    [PunRPC]
    public void UpdateScore3()
    {
        correctperson.text = PhotonNetwork.PlayerList[2].NickName + "���� ������ ���߼̽��ϴ�.";
        Jumsu.Score3 += 1;
        corrects = true;

    }
    [PunRPC]
    public void UpdateScore4()
    {
        correctperson.text = PhotonNetwork.PlayerList[3].NickName + "���� ������ ���߼̽��ϴ�.";
        Jumsu.Score4 += 1;
        corrects = true;

    }
}
