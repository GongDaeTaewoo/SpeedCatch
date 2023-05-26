using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Photon.Pun;
using Photon.Realtime;
//�̽�ũ��Ʈ�� virtualKeyboardCanvas�Ʒ��� virtualtextinputbox�����Դϴ�
//fire����Firebase�����Դϴ�
//
public class wordSave : MonoBehaviour
{
    public Text JesimunTxt;
    public GameObject Fire;
    string k;

    // Start is called before the first frame update
    public void Start()
    {
 


    }

    //���ù���������
    public void ReadUserData(int outputNum)
    {

        FirebaseDatabase.DefaultInstance.GetReference("users")
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
                    JesimunTxt.text = k;
                    Debug.Log("���ù�����!");
                    //����λ��


                }

            });



    }

    // Update is called once per frame


    public void Update()
    {

        // firstinput();
        // firstoutput();
    }
    //ù��°���ù�, �Լ�ȣ���ؼ� ���
    public void firstinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11�� ����1�� �÷��̾� ���� 1�� ���°���ù��̳�
            Fire.GetComponent<CFirebase>().WriteUserData("11", gameObject.GetComponent<InputField>().text);

        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11�� ����2�� �÷��̾� ���� 1�� ���°���ù��̳�
            Fire.GetComponent<CFirebase>().WriteUserData("21", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("31", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("41", gameObject.GetComponent<InputField>().text);
        }
    }
    //�ι�°���ù�
    public void twoinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {//11�� ����1�� �÷��̾� ���� 2�� ���°���ù��̳�
            Fire.GetComponent<CFirebase>().WriteUserData("12", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("22", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("32", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("42", gameObject.GetComponent<InputField>().text);
        }
    }
    //����°���ù�
    public void threeinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("13", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("23", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("33", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("43", gameObject.GetComponent<InputField>().text);
        }
    }
    //�׹�°���ù�
    public void fourinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("14", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("24", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("34", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("44", gameObject.GetComponent<InputField>().text);
        }

    }
    public void firstoutput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11�� ����1�� �÷��̾� ���� 1�� ���°���ù��̳�
            //�÷��̾�1�� 4�����ù������� 41
            ReadUserData(41);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11�� ����2�� �÷��̾� ���� 1�� ���°���ù��̳�
            //�÷��̾�2�� 1�����ù������� 11
            ReadUserData(11);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            ReadUserData(21);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            ReadUserData(31);
        }
    }
    public void twooutput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11�� ����1�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(42);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11�� ����2�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(12);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            ReadUserData(22);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            ReadUserData(32);
        }
    }
    public void threeoutput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11�� ����1�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(43);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11�� ����2�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(13);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            ReadUserData(23);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            ReadUserData(33);
        }
    }

    public void fouroutput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11�� ����1�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(44);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11�� ����2�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(14);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            ReadUserData(24);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            ReadUserData(34);
        }
    }


    /*
    //���ù���������
    public void ReadUserData(int outputNum)
    {

        FirebaseDatabase.DefaultInstance.GetReference("users")
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
                    JesimunTxt.text = k;
                    Debug.Log("���ù�����!");
                    //����λ��
                    
                  
                }
   
            });
        


    }

    // Update is called once per frame


    public void Update()
    {

        // firstinput();
        // firstoutput();
    }
    //ù��°���ù�, �Լ�ȣ���ؼ� ���
    public void firstinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11�� ����1�� �÷��̾� ���� 1�� ���°���ù��̳�
            Fire.GetComponent<CFirebase>().WriteUserData("11", gameObject.GetComponent<InputField>().text);

        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11�� ����2�� �÷��̾� ���� 1�� ���°���ù��̳�
            Fire.GetComponent<CFirebase>().WriteUserData("21", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("31", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("41", gameObject.GetComponent<InputField>().text);
        }
    }
    //�ι�°���ù�
    public void twoinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {//11�� ����1�� �÷��̾� ���� 2�� ���°���ù��̳�
            Fire.GetComponent<CFirebase>().WriteUserData("12", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("22", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("32", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("42", gameObject.GetComponent<InputField>().text);
        }
    }
    //����°���ù�
    public void threeinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("13", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("23", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("33", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("43", gameObject.GetComponent<InputField>().text);
        }
    }
    //�׹�°���ù�
    public void fourinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("14", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("24", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("34", gameObject.GetComponent<InputField>().text);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            Fire.GetComponent<CFirebase>().WriteUserData("44", gameObject.GetComponent<InputField>().text);
        }

    }
    public void firstoutput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11�� ����1�� �÷��̾� ���� 1�� ���°���ù��̳�
            //�÷��̾�1�� 4�����ù������� 41
            ReadUserData(41);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11�� ����2�� �÷��̾� ���� 1�� ���°���ù��̳�
            //�÷��̾�2�� 1�����ù������� 11
            ReadUserData(11);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            ReadUserData(21);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            ReadUserData(31);
        }
    }
    public void twooutput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11�� ����1�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(42);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11�� ����2�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(12);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            ReadUserData(22);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            ReadUserData(32);
        }
    }
    public void threeoutput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11�� ����1�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(43);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11�� ����2�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(13);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            ReadUserData(23);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            ReadUserData(33);
        }
    }

    public void fouroutput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11�� ����1�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(44);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11�� ����2�� �÷��̾� ���� 1�� ���°���ù��̳�
            ReadUserData(14);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            ReadUserData(24);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            ReadUserData(34);
        }
    }
    */

}
