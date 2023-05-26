using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.TexturePainter;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Networking.Pun2;

public class Gamemanager : MonoBehaviourPunCallbacks
{


    public NetworkManager networkManager;
    int a = 0;
    public int b = 1;
    public Text whoturn;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject ovrCameraRig;
    public int pcount;
    public CFirebase cfirebase;
    public Guessanswer guessanswer;
    
    
  

    public int turncount; //�� ��. 
    public bool turn; //���� �������?���� �Ѿ ���� ���� ����.
    bool onceSpon = true;
    bool gamestart = false;

    /// <summary>
    /// UI ////////
    /// </summary>
    public UIManager uimanager;//UImanager ��ũ��Ʈ�� ����.UI�� ���õ� Ŭ������ ��Ƴ���?
    public bool turnOnce = true;
    public Stack<Text>[] save_txt; //�÷��̾��� ���ù� ���� ���� �迭
    public Stack<Texture2D>[] save_pic; //�÷��̾��� �׸� ���� ���� �迭

    public TexturePaintTarget[] TexturePaintTarget;
    public TexturePaintBrush[] TexturePaintBrush;
    public Countdown countdown;
    public NameBar name1;
    public NameBar2 name2;
    public NameBar3 name3;
    public NameBar4 name4;
    public PhotonView[] photonview;
    public PhotonView pv;
    public GameObject[] ui;
    public string[] uiname;
    public string[] mode = {"���п� ���õ� ���ù��� ������ �����ּ���",
        "���ӿ� ���õ� ���ù��� ������ �����ּ���",
        "�Ӵ㿡 ���õ� ���ù��� ������ �����ּ���",
        "��� ���õ� ���ù��� ������ �����ּ���",
        "��ȭ�� ���õ� ���ù��� ������ �����ּ���",
        "�˰��� ���õ� ���ù��� ������ �����ּ���",
        "��ǻ�ͱ׷��Ƚ��� ���õ� ���ù��� ������ �����ּ���"};
    public Text modetext;
    

    public MColorPick[] colors1;
    public MTools[] tools1;
    public MTools[] ptools1;

    public MColorPick[] colors2;
    public MTools[] tools2;
    public MTools[] ptools2;

    public MColorPick[] colors3;
    public MTools[] tools3;
    public MTools[] ptools3;

    public MColorPick[] colors4;
    public MTools[] tools4;
    public MTools[] ptools4;


    void Awake()
    {

    }

    public void Start()
    {
       
        save_pic = new Stack<Texture2D>[4];
        save_txt = new Stack<Text>[4];
        TexturePaintTarget = new TexturePaintTarget[4];
        TexturePaintBrush = new TexturePaintBrush[4];
        
        photonview = new PhotonView[4];
        ui = new GameObject[20];
        uiname = new string[100];
        
               
          
        colors1 = new MColorPick[20];
        tools1 = new MTools[20];
        ptools1 = new MTools[10];

        colors2 = new MColorPick[20];
        tools2 = new MTools[20];
        ptools2 = new MTools[10];

        colors3 = new MColorPick[20];
        tools3 = new MTools[20];
        ptools3 = new MTools[10];

        colors4 = new MColorPick[20];
        tools4 = new MTools[20];
        ptools4 = new MTools[10];



        turncount = -1;
        turn = false;

        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }
    //���� �Ŵ��� ��ũ��Ʈ ���� ���� Ȥ�� �Լ�����
    //GameManager.instance.(���� or�Լ���)���� ���� �ҷ����� ��밡��?

    // Update is called once per frame
    [PunRPC]
    public void UpdateModetext(int mode_num)
    {
        modetext.text = mode[mode_num];
    }
    public void Update()
    {
       



        if (networkManager.isSpon == true)//NetwrokManager��ũ��Ʈ�� onjoined�Լ��� ȣ��Ǹ�?True����(�����ؾ��ϴ»�Ȳ�̵Ȱ��̹Ƿ�)
        {
            if (onceSpon == true)
            {


                if (PhotonNetwork.CurrentRoom.PlayerCount == 1)//���� �׽�Ʈ�� 1~4�ιٲ����?���߽�����1
                {
                    PhotonNetwork.Instantiate("NewPlayer1", networkManager.ConvertAngleToVectorR(180, 1.0f), Quaternion.Euler(new Vector3(0, 90, 0)), 0);

                   
                    /////player1�� ������Ʈ ����//////
                    player1 = GameObject.Find("NewPlayer1(Clone)"); //player1 Ŭ���� ������ �Ǿ����Ƿ�(Ȱ��ȭ�� �Ǿ����Ƿ�,) Find�Լ� ��밡��?
                    ovrCameraRig.transform.position = player1.transform.position;
                    ovrCameraRig.transform.rotation = player1.transform.rotation;

                    TexturePaintTarget[0] = player1.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                    TexturePaintBrush[0] = player1.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();
       
        
                    countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                    name1 = player1.transform.Find("PunHead/name").GetComponent<NameBar>();
                    name1.username();
                    uiname[0] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                    photonview[0] = player1.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();


                    ui[0] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.Find("gestureguide").gameObject;
                    ui[1] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.Find("startguide").gameObject;
                    ui[2] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.Find("guide1").gameObject;
                    ui[3] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.Find("endguide").gameObject;
                    ui[4] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.Find("guide2").gameObject;
                    ui[0].SetActive(true); //����ó ui����
                    ui[1].SetActive(true); //start ui����




                    colors1 = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/ColorPicker/GameObject/Colors").GetComponentsInChildren<MColorPick>();
                    tools1 = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/ColorPicker/GameObject/tools").GetComponentsInChildren<MTools>();
                  
                    foreach (MColorPick mcolorpick in colors1)//�÷��̾�1�� rig�� �پ��ִ� mcolorpick�� texturepaintbursh������ �÷��̾�1�� trigger������Ʈ�� �پ��ִ� texturepaintbrushŬ������ �����Ѵ�.
                    {
                        mcolorpick.texturepaintbrush = TexturePaintBrush[0];
                    }

                    foreach (MTools mtools in tools1)//�÷��̾�1�� rig�� �پ��ִ� mcolorpick�� texturepaintbursh������ �÷��̾�1�� trigger������Ʈ�� �پ��ִ� texturepaintbrushŬ������ �����Ѵ�.
                    {
                        mtools.texturepaintbrush = TexturePaintBrush[0];
                        //mtools.pv = photonview[0];
                    }

                

                 


                    if (PhotonNetwork.IsMasterClient == false)//����ƴϸ�?
                    {
                        // startcanvas.SetActive(false);//���ӹ�ư����
                    }

                    onceSpon = false;
                }


                if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
                {
                    PhotonNetwork.Instantiate("NewPlayer2", networkManager.ConvertAngleToVectorR(90, 1.0f), Quaternion.Euler(new Vector3(0, 180, 0)), 0);
                   

                    player1 = GameObject.Find("NewPlayer1(Clone)");
                    player2 = GameObject.Find("NewPlayer2(Clone)");


                 

                    TexturePaintTarget[1] = player2.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                    TexturePaintBrush[1] = player2.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();

                    countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                    name2 = player2.transform.Find("PunHead/name").GetComponent<NameBar2>();
                    name2.username();
                    uiname[1] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                    photonview[1] = player2.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();

                    ui[0] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(0).gameObject;
                    ui[1] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(1).gameObject;
                    ui[2] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(2).gameObject;
                    ui[3] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(3).gameObject;
                    ui[4] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.Find("guide2").gameObject;
                    ui[0].SetActive(true); //����ó ui����
                    ui[1].SetActive(true); //start ui����

                    colors2 = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/ColorPicker/GameObject/Colors").GetComponentsInChildren<MColorPick>();
                    tools2 = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/ColorPicker/GameObject/tools").GetComponentsInChildren<MTools>();
               

                    foreach (MColorPick mcolorpick in colors2)//�÷��̾�1�� rig�� �پ��ִ� mcolorpick�� texturepaintbursh������ �÷��̾�1�� trigger������Ʈ�� �پ��ִ� texturepaintbrushŬ������ �����Ѵ�.
                    {
                        mcolorpick.texturepaintbrush = TexturePaintBrush[1];
                       
                    }

                    foreach (MTools mtools in tools2)//�÷��̾�1�� rig�� �پ��ִ� mcolorpick�� texturepaintbursh������ �÷��̾�1�� trigger������Ʈ�� �پ��ִ� texturepaintbrushŬ������ �����Ѵ�.
                    {
                        mtools.texturepaintbrush = TexturePaintBrush[1];
                       // mtools.pv = photonview[1];
                    }





                    ovrCameraRig.transform.position = player2.transform.position;
                    ovrCameraRig.transform.rotation = player2.transform.rotation;

                    if (PhotonNetwork.IsMasterClient == false) //����ƴϸ�?
                    {
                        // startcanvas.SetActive(false);//���ӹ�ư����
                    }

                    onceSpon = false;
                }


                if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
                {
                    PhotonNetwork.Instantiate("NewPlayer3", networkManager.ConvertAngleToVectorR(0, 1.0f), Quaternion.Euler(new Vector3(0, 270, 0)), 0);




                    player1 = GameObject.Find("NewPlayer1(Clone)");
                    player2 = GameObject.Find("NewPlayer2(Clone)");
                    player3 = GameObject.Find("NewPlayer3(Clone)");
        

               

                    TexturePaintTarget[2] = player3.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                    TexturePaintBrush[2] = player3.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();


                    countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                    name3 = player3.transform.Find("PunHead/name").GetComponent<NameBar3>();
                    name3.username();
                    uiname[2] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                    photonview[2] = player3.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();

                    ui[0] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(0).gameObject;
                    ui[1] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(1).gameObject;
                    ui[2] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(2).gameObject;
                    ui[3] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(3).gameObject;
                    ui[4] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.Find("guide2").gameObject;
                    ui[0].SetActive(true); //����ó ui����
                    ui[1].SetActive(true); //start ui����

                    colors3 = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/ColorPicker/GameObject/Colors").GetComponentsInChildren<MColorPick>();
                    tools3 = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/ColorPicker/GameObject/tools").GetComponentsInChildren<MTools>();
                  
                    foreach (MColorPick mcolorpick in colors3)//�÷��̾�1�� rig�� �پ��ִ� mcolorpick�� texturepaintbursh������ �÷��̾�1�� trigger������Ʈ�� �پ��ִ� texturepaintbrushŬ������ �����Ѵ�.
                    {
                        mcolorpick.texturepaintbrush = TexturePaintBrush[2];
                    }

                    foreach (MTools mtools in tools3)//�÷��̾�1�� rig�� �پ��ִ� mcolorpick�� texturepaintbursh������ �÷��̾�1�� trigger������Ʈ�� �پ��ִ� texturepaintbrushŬ������ �����Ѵ�.
                    {
                        mtools.texturepaintbrush = TexturePaintBrush[2];
                       // mtools.pv = photonview[2];
                    }

                  





                    ovrCameraRig.transform.position = player3.transform.position;
                    ovrCameraRig.transform.rotation = player3.transform.rotation;
                    onceSpon = false;
                    if (PhotonNetwork.IsMasterClient == false) //����ƴϸ�?
                    {
                        //startcanvas.SetActive(false);//���ӹ�ư����
                    }

                    onceSpon = false;
                }



                

                if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
                {
                    PhotonNetwork.Instantiate("NewPlayer4", networkManager.ConvertAngleToVectorR(270, 1.0f), Quaternion.Euler(new Vector3(0, 0, 0)), 0);
                    // PhotonNetwork.Instantiate("Player4", networkManager.ConvertAngleToVectorR(270, 0.05f), Quaternion.Euler(new Vector3(0, 0, 0)), 0);




                    player1 = GameObject.Find("NewPlayer1(Clone)");
                    player2 = GameObject.Find("NewPlayer2(Clone)");
                    player3 = GameObject.Find("NewPlayer3(Clone)");
                    player4 = GameObject.Find("NewPlayer4(Clone)");



                  

                    TexturePaintTarget[3] = player4.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                    TexturePaintBrush[3] = player4.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();


                    countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                    name4 = player4.transform.Find("PunHead/name").GetComponent<NameBar4>();
                    name4.username();
                    uiname[3] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                    photonview[3] = player4.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();

                    ui[0] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(0).gameObject;
                    ui[1] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(1).gameObject;
                    ui[2] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(2).gameObject;
                    ui[3] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.GetChild(3).gameObject;
                    ui[4] = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/UI").transform.Find("guide2").gameObject;
                    ui[0].SetActive(true); //����ó ui����
                    ui[1].SetActive(true); //start ui����

                    colors4 = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/ColorPicker/GameObject/Colors").GetComponentsInChildren<MColorPick>();
                    tools4 = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/ColorPicker/GameObject/tools").GetComponentsInChildren<MTools>();
                 

                    foreach (MColorPick mcolorpick in colors4)//�÷��̾�1�� rig�� �پ��ִ� mcolorpick�� texturepaintbursh������ �÷��̾�1�� trigger������Ʈ�� �پ��ִ� texturepaintbrushŬ������ �����Ѵ�.
                    {
                        mcolorpick.texturepaintbrush = TexturePaintBrush[3];
                    }

                    foreach (MTools mtools in tools4)//�÷��̾�1�� rig�� �پ��ִ� mcolorpick�� texturepaintbursh������ �÷��̾�1�� trigger������Ʈ�� �پ��ִ� texturepaintbrushŬ������ �����Ѵ�.
                    {
                        mtools.texturepaintbrush = TexturePaintBrush[3];
                       // mtools.pv = photonview[3];
                    }




                    ovrCameraRig.transform.position = player4.transform.position;
                    ovrCameraRig.transform.rotation = player4.transform.rotation;
                    onceSpon = false;
                    if (PhotonNetwork.IsMasterClient == false) //����ƴϸ�?
                    {
                        //startcanvas.SetActive(false);//���ӹ�ư����
                    }

                    onceSpon = false;

                }


            }



        }
        if (networkManager.isSpon == true)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1 && GameObject.Find("NewPlayer1(Clone)") != null)
            {
                player1 = GameObject.Find("NewPlayer1(Clone)");


           

                TexturePaintTarget[0] = player1.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                TexturePaintBrush[0] = player1.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();

                countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                name1 = player1.transform.Find("PunHead/name").GetComponent<NameBar>();
                name1.username();
                uiname[0] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                photonview[0] = player1.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();




            }
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && GameObject.Find("NewPlayer2(Clone)")!=null)
            {
                player1 = GameObject.Find("NewPlayer1(Clone)");
             

                TexturePaintTarget[0] = player1.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                TexturePaintBrush[0] = player1.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();


                countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                name1 = player1.transform.Find("PunHead/name").GetComponent<NameBar>();
                name1.username();
                photonview[0] = player1.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();






                player2 = GameObject.Find("NewPlayer2(Clone)");

               
                TexturePaintTarget[1] = player2.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.              
                TexturePaintBrush[1] = player2.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();


                countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                name2 = player2.transform.Find("PunHead/name").GetComponent<NameBar2>();
                name2.username();
                uiname[1] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                photonview[1] = player2.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();






            }
            if (PhotonNetwork.CurrentRoom.PlayerCount == 3 && GameObject.Find("NewPlayer3(Clone)") != null)
            {
                player1 = GameObject.Find("NewPlayer1(Clone)");
            

                TexturePaintTarget[0] = player1.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                TexturePaintBrush[0] = player1.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();

                countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                name1 = player1.transform.Find("PunHead/name").GetComponent<NameBar>();
                name1.username();
                uiname[0] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                photonview[0] = player1.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();





                player2 = GameObject.Find("NewPlayer2(Clone)");
             

                TexturePaintTarget[1] = player2.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                TexturePaintBrush[1] = player2.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();

                countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                name2 = player2.transform.Find("PunHead/name").GetComponent<NameBar2>();
                name2.username();
                uiname[1] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                photonview[1] = player2.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();


                player3 = GameObject.Find("NewPlayer3(Clone)");
           

                TexturePaintTarget[2] = player3.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                TexturePaintBrush[2] = player3.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();


                countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                name3 = player3.transform.Find("PunHead/name").GetComponent<NameBar3>();
                name3.username();
                uiname[2] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                photonview[2] = player3.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();





            }
            if (PhotonNetwork.CurrentRoom.PlayerCount == 4 && GameObject.Find("NewPlayer4(Clone)") != null)
            {
                player1 = GameObject.Find("NewPlayer1(Clone)");
              
                TexturePaintTarget[0] = player1.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                TexturePaintBrush[0] = player1.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();


                countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                name1 = player1.transform.Find("PunHead/name").GetComponent<NameBar>();
                name1.username();
                uiname[0] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                photonview[0] = player1.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();


                player2 = GameObject.Find("NewPlayer2(Clone)");
             

                TexturePaintTarget[1] = player2.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                TexturePaintBrush[1] = player2.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();

                countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                name2 = player2.transform.Find("PunHead/name").GetComponent<NameBar2>();
                name2.username();
                uiname[1] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                photonview[1] = player2.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();


                player3 = GameObject.Find("NewPlayer3(Clone)");
               
                TexturePaintTarget[2] = player3.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                TexturePaintBrush[2] = player3.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();


                countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                name3 = player3.transform.Find("PunHead/name").GetComponent<NameBar3>();
                name3.username();
                uiname[2] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                photonview[2] = player3.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();




                player4 = GameObject.Find("NewPlayer4(Clone)");
            

                TexturePaintTarget[3] = player4.transform.Find("Pairing PaintTarget/Paint Target").GetComponent<TexturePaintTarget>();//p1 ����ġ�� ����.                       
                TexturePaintBrush[3] = player4.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<TexturePaintBrush>();

                countdown = GameObject.Find("OculusInteractionSampleRig_mr1/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CountDown").GetComponent<Countdown>();
                name4 = player4.transform.Find("PunHead/name").GetComponent<NameBar4>();
                name4.username();
                uiname[3] = player1.transform.Find("PunHead/name").GetComponent<TextMeshPro>().text;
                photonview[3] = player4.transform.Find("Pairing PaintTarget/Paint Target/Trigger").GetComponent<PhotonView>();



            }
        }
      

        if (gamestart == true)
            {


            if (PhotonNetwork.CurrentRoom.PlayerCount == null) return;
            if (turnOnce == true)

            {
                if (PhotonNetwork.IsMasterClient)
                {
                     pcount = PhotonNetwork.CurrentRoom.PlayerCount;
                    int random_var = Random.Range(0, 7);
                    modetext.text = mode[random_var];
                    photonView.RPC("UpdateModetext", RpcTarget.Others, random_var);
                }
                turncount = PhotonNetwork.CurrentRoom.PlayerCount * 3;
                whoturn.text = PhotonNetwork.MasterClient.NickName + "���� �����Դϴ�.";
                

                turnOnce = false;//�÷��̾����� ���� *3 ��ŭ �� ���� ������.
              
               
            }

            //�� ���� 0�� �� ������ �ݺ��մϴ�.. 



            //////���ù� �Է� ��//////
            // -1) 2�� ī��Ʈ ����
            if (turncount >= 0)
            {
              
                if (turncount == PhotonNetwork.CurrentRoom.PlayerCount*3)
                {
                    ui[0].SetActive(false); //����ó ui����
                    ui[1].SetActive(false); //start ui����
                }
               
               
                
                
                ///////// ���ǵ� ĳġ ���� ���� /////////


                // 1�� ī��Ʈ�ٿ� ����.
                turn = true; //�� ����

                if(guessanswer.corrects==true)
                {
                    countdown.SetZero();
                    turncount--;
                    turn = false;

                    if (b < pcount)
                    {
                        
                        whoturn.text = PhotonNetwork.PlayerList[b].NickName + "���� �����Դϴ�.";
                        photonView.RPC("Updatename", RpcTarget.All, b);
                        Debug.Log(PhotonNetwork.PlayerList[b] + " " + b);

                        b++;
                        if (b == pcount)
                        {
                            b = 0;
                        }

                    }
                    guessanswer.corrects = false;
                    countdown.m_TotalSeconds = 1 * 60;

                    
                }




                // -2) ī��Ʈ ����
             
                
               if (countdown.m_TotalSeconds <= 1)
                {
                    if (b < pcount)
                    {
                       
                        whoturn.text = PhotonNetwork.PlayerList[b].NickName + "���� �����Դϴ�.";
                        photonView.RPC("Updatename", RpcTarget.All,b);
                        Debug.Log(PhotonNetwork.PlayerList[b] + " " + b);

                        b++;
                        if (b == pcount)
                        {
                            b = 0;
                        }
                    }


                    countdown.m_TotalSeconds = 1 * 60;
                    turn = false;
                    turncount--;
                        
                }
                



                if (turncount == 0)

                {
                    ui[3].SetActive(true); //start ui����
                    gamestart = false;
                    turnOnce = true;
                    return;
                }
            }
            

             
        
                                    }
                                }
                            


   public void start()
    {
        pv.RPC("withStart", RpcTarget.All);
        
        Debug.Log("start");
    }
    [PunRPC]
    void Updatename(int a)
    {
        whoturn.text = PhotonNetwork.PlayerList[a].NickName + "���� �����Դϴ�.";
    }
    [PunRPC]
    void withStart()
    {
        gamestart = true;
    }
}
