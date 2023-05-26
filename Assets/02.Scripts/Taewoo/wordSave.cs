using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Photon.Pun;
using Photon.Realtime;
//이스크립트는 virtualKeyboardCanvas아래에 virtualtextinputbox에붙입니당
//fire에는Firebase를붙입니당
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

    //제시문가져오기
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
                    //wordsave숫자에있는 값을 가져옴
                    //return snapshot.Child(outputNum.ToString()).Value;

                    //snapshot.Child(outputNum.ToString()).Value;
                    k = (string)snapshot.Child(outputNum.ToString()).Value;
                    JesimunTxt.text = k;
                    Debug.Log("제시문변경!");
                    //맘대로사용


                }

            });



    }

    // Update is called once per frame


    public void Update()
    {

        // firstinput();
        // firstoutput();
    }
    //첫번째제시문, 함수호출해서 사용
    public void firstinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11의 앞의1은 플레이어 뒤의 1은 몇번째제시문이냐
            Fire.GetComponent<CFirebase>().WriteUserData("11", gameObject.GetComponent<InputField>().text);

        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11의 앞의2는 플레이어 뒤의 1은 몇번째제시문이냐
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
    //두번째제시문
    public void twoinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {//11의 앞의1은 플레이어 뒤의 2는 몇번째제시문이냐
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
    //세번째제시문
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
    //네번째제시문
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
            //11의 앞의1은 플레이어 뒤의 1은 몇번째제시문이냐
            //플레이어1은 4의제시문을보니 41
            ReadUserData(41);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11의 앞의2는 플레이어 뒤의 1은 몇번째제시문이냐
            //플레이어2는 1의제시문을보니 11
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
            //11의 앞의1은 플레이어 뒤의 1은 몇번째제시문이냐
            ReadUserData(42);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11의 앞의2는 플레이어 뒤의 1은 몇번째제시문이냐
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
            //11의 앞의1은 플레이어 뒤의 1은 몇번째제시문이냐
            ReadUserData(43);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11의 앞의2는 플레이어 뒤의 1은 몇번째제시문이냐
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
            //11의 앞의1은 플레이어 뒤의 1은 몇번째제시문이냐
            ReadUserData(44);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11의 앞의2는 플레이어 뒤의 1은 몇번째제시문이냐
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
    //제시문가져오기
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
                    //wordsave숫자에있는 값을 가져옴
                    //return snapshot.Child(outputNum.ToString()).Value;

                    //snapshot.Child(outputNum.ToString()).Value;
                    k = (string)snapshot.Child(outputNum.ToString()).Value;
                    JesimunTxt.text = k;
                    Debug.Log("제시문변경!");
                    //맘대로사용
                    
                  
                }
   
            });
        


    }

    // Update is called once per frame


    public void Update()
    {

        // firstinput();
        // firstoutput();
    }
    //첫번째제시문, 함수호출해서 사용
    public void firstinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            //11의 앞의1은 플레이어 뒤의 1은 몇번째제시문이냐
            Fire.GetComponent<CFirebase>().WriteUserData("11", gameObject.GetComponent<InputField>().text);

        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11의 앞의2는 플레이어 뒤의 1은 몇번째제시문이냐
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
    //두번째제시문
    public void twoinput()
    {
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {//11의 앞의1은 플레이어 뒤의 2는 몇번째제시문이냐
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
    //세번째제시문
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
    //네번째제시문
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
            //11의 앞의1은 플레이어 뒤의 1은 몇번째제시문이냐
            //플레이어1은 4의제시문을보니 41
            ReadUserData(41);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11의 앞의2는 플레이어 뒤의 1은 몇번째제시문이냐
            //플레이어2는 1의제시문을보니 11
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
            //11의 앞의1은 플레이어 뒤의 1은 몇번째제시문이냐
            ReadUserData(42);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11의 앞의2는 플레이어 뒤의 1은 몇번째제시문이냐
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
            //11의 앞의1은 플레이어 뒤의 1은 몇번째제시문이냐
            ReadUserData(43);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11의 앞의2는 플레이어 뒤의 1은 몇번째제시문이냐
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
            //11의 앞의1은 플레이어 뒤의 1은 몇번째제시문이냐
            ReadUserData(44);
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            //11의 앞의2는 플레이어 뒤의 1은 몇번째제시문이냐
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
