using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text StatusText;
    public InputField roomInput, NickNameInput;
    /* �ڱ� ����Ʈ�귯���ƴϸ� �����ڵ忡 �ʿ��Ѱ�
    public GameObject brush;
    public PhotonView PV;
    */
    public bool isSpon = false;
    int nick = 0;

    void Awake()
    {
        Screen.SetResolution(960, 540, false);

    }



    public void ModeChange()//��Ŭ���Լ��� ������ ������� ��尪�̺����
    {
        if (PhotonNetwork.IsMasterClient == true)

        {
            if (modeSyn.mode==0)
            {
                modeSyn.mode = 1;
            }
            if (modeSyn.mode == 1)
            {
                modeSyn.mode = 2;
            }
            else if (modeSyn.mode == 2)
            {
                modeSyn.mode = 1;
            }
        }
    }

    void Update()
    {//��� ����
        //Connect();
        //���ü�����Լ��� mode������ �ٲٴ°ͳְ� ����� �װͿ� ���� ��带 �ٲٴ������� ��庯���� ����ȭ�ؼ� ��ü�÷��̾ �����ϱ�������


        /*
        if (modeSyn.mode == 1)
        {
            StatusText.text = "���� ������";
        }
        else if (modeSyn.mode == 2)
        { 
            StatusText.text= "Ŀ���Ҹ��";
        }
        else
        {
            StatusText.text = "��带 ������";
            Debug.Log(modeSyn.mode);
        }
        */
        //StatusText.text = PhotonNetwork.NetworkClientState.ToString();
    }

    public Vector3 ConvertAngleToVector(float _deg, float r)
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(r * Mathf.Cos(rad), 0.0f, r * Mathf.Sin(rad));
    }

    public Vector3 ConvertAngleToVectorS(float _deg, float r) //����ġ��
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(r * Mathf.Cos(rad), 0.5286f, r * Mathf.Sin(rad));
    }
    public Vector3 ConvertAngleToVectorR(float _deg, float r)//�κ�=�÷��̾�
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(r * Mathf.Cos(rad), 0.612f, r * Mathf.Sin(rad));
    }
    public Vector3 ConvertAngleToVectorN(float _deg, float r)//�̸�
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(r * Mathf.Cos(rad), 0.7f, r * Mathf.Sin(rad));
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {

        print("�������ӿϷ�");
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        JoinLobby();




    }



    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause) => print("�������");



    public void JoinLobby() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        print("�κ����ӿϷ�");
        //CreateRoom();//�������� �ڵ��游���
    }



    public void CreateRoom() => PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 4 });

    public void JoinRoom() => PhotonNetwork.JoinRoom(roomInput.text);

    public void JoinOrCreateRoom() => PhotonNetwork.JoinOrCreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 4 }, null);

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnCreatedRoom()
    {
        print("�游���Ϸ�");
        isSpon = true;
        Info();
    }

    public override void OnJoinedRoom()
    {
        isSpon = true;
        print("�������Ϸ�");
       

    }

    public override void OnCreateRoomFailed(short returnCode, string message) => print("�游������");

    public override void OnJoinRoomFailed(short returnCode, string message) => print("����������");

    public override void OnJoinRandomFailed(short returnCode, string message) => print("�淣����������");



    [ContextMenu("����")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("���� �� �̸� : " + PhotonNetwork.CurrentRoom.Name);
            print("���� �� �ο��� : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("���� �� �ִ��ο��� : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "�濡 �ִ� �÷��̾� ��� : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
        else
        {
            print("������ �ο� �� : " + PhotonNetwork.CountOfPlayers);
            print("�� ���� : " + PhotonNetwork.CountOfRooms);
            print("��� �濡 �ִ� �ο� �� : " + PhotonNetwork.CountOfPlayersInRooms);
            print("�κ� �ִ���? : " + PhotonNetwork.InLobby);
            print("����ƴ���? : " + PhotonNetwork.IsConnected);
        }
    }
}