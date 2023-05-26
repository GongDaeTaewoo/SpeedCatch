using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

//�÷��̾ ���ù��� �Է��ϴ� â�� �����ϴ� ��ũ��Ʈ�Դϴ�.
//���� : ���ù��� '��'�� ������ �ϰ�, ���ù��� �ٸ� ����� �� �� ������ �ȵ�.


public class ChatManager : MonoBehaviourPunCallbacks
{
    public Button sendBtn;
    public Text chatLog;
    public Text chattingList;
    public InputField input;
    ScrollRect scroll_rect = null;
    string chatters;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        scroll_rect = GameObject.FindObjectOfType<ScrollRect>();
    }
    public void SendButtonOnClicked() //���� �� �޼����� Photon������ ������ �Լ�.
    {
        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        string msg = string.Format("[{0}] {1}", PhotonNetwork.LocalPlayer.NickName, input.text);
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);//�ٸ�����鵵 receivemsg�Լ��� ����.
        ReceiveMsg(msg);
        input.ActivateInputField(); // �ݴ�� input.select(); (�ݴ�� ���)
        input.text = "";
    }
    void Update()
    {
        chatterUpdate();
        if (Input.GetKeyDown(KeyCode.Return) && !input.isFocused) SendButtonOnClicked();
    }
    void chatterUpdate()
    {
        chatters = "Player List\n";
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            chatters += p.NickName + "\n";
        }
        chattingList.text = chatters;
    }
    [PunRPC]
    public void ReceiveMsg(string msg) //���漭���� �޼��� ������.
    {
        chatLog.text += "\n" + msg;
        scroll_rect.verticalNormalizedPosition = 0.0f;
    }
}
