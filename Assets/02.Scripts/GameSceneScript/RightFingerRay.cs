
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �����տ��� ������ ���̸� ������ ��ũ��Ʈ.
/// </summary>
public class RightFingerRay : MonoBehaviour
{


    public Ray ray;
    public float distance;

  //  private PunOVRHand m_hand;
  //  private OVRCustomSkeleton m_skeleton;

    private PunOVRHand m_hand;
    private OVRSkeleton m_skeleton;
    private OVRBoneCapsule m_bonecapsule;

    private Transform indexTip;
    private Transform indexDistal;
    public CapsuleCollider indexCol;
    public CapsuleCollider indexCol2;

    public Vector3 originPoint;
    public Vector3 targetPoint;
     

    private void Awake()
    {
        //Get the scripts that hold information about hand tracking
        /* m_hand = GetComponent<OVRHand>();
         m_skeleton = GetComponent<OVRSkeleton>();*/
        m_hand = GetComponent<PunOVRHand>();
        m_skeleton = GetComponent<OVRSkeleton>();
        // m_bonecapsule = GetComponent<OVRBoneCapsule>();
        ray = new Ray();
        originPoint = new Vector3();
        targetPoint = new Vector3();
   
    }

    // Update is called once per frame
    void Update()
    {

        //if (indexTip == null && m_skeleton.IsInitialized)//���� �ν��� �Ǿ��� ��.
        if (indexTip == null && m_skeleton.IsInitialized)//���� �ν��� �Ǿ��� ��.
        {
                Debug.Log("Skeleton initialized");
                indexTip = m_skeleton.Bones[(int)OVRPlugin.BoneId.Hand_IndexTip].Transform;//���� ������ǥ�� indextip
                indexDistal = m_skeleton.Bones[(int)OVRPlugin.BoneId.Hand_Index2].Transform; //������ 2��° ���� ���̴� �κ��� ��ǥ�� indexdistal.
                indexCol = m_skeleton.Capsules[(int)OVRPlugin.BoneId.Hand_Index3].CapsuleCollider;//������ 2��° ���� ĸ�� �ݶ��̴� indexCol.                                         
                indexCol2 = m_skeleton.Capsules[(int)OVRPlugin.BoneId.Hand_Index2].CapsuleCollider;//������ 1��° ���� ĸ�� �ݶ��̴� indexCol2



                indexCol.isTrigger = true; //������ ĸ�� �ݶ��̴��� Ʈ���ŷ� �����.
                indexCol2.isTrigger = true;
            //Destroy(indexCol.gameObject.GetComponent<Rigidbody>()); //������ ĸ�� �ݶ��̴��� �޷��ִ� ������Ʈ�� ������ٵ� �����.

            indexCol.gameObject.tag = "Sketch"; //������ ĸ�� �ݶ��̴��� �پ��ִ� ������Ʈ�� �±׸� Sketch��� �Ҵ����ش�.
            indexCol2.gameObject.tag = "Sketch";

            ////////���� : Paint Target�� Trigger ������Ʈ�� Brush��ũ��Ʈ�� �پ��־����.
               

            

          /*  public OVRBoneCapsule(short boneIndex, Rigidbody capsuleRigidBody, CapsuleCollider capsuleCollider)
            {
                BoneIndex = boneIndex;
                CapsuleRigidbody = capsuleRigidBody;
                CapsuleCollider = capsuleCollider;
            }
          */


             }
      

            /////////////////�׸��� ���̷� �׸�/////////////////////
            if (!indexTip) return; //���� �νĵ��� �ʾҴٸ� ����.
      
        originPoint = indexDistal.position;
        targetPoint = indexTip.position;

        Vector3 direction = Vector3.Normalize(targetPoint - originPoint);//�������� ������ ������ ����
        distance = Vector3.Distance(originPoint, targetPoint); //�������� ������ ������ �Ÿ�.
       
        ray = new Ray(originPoint, direction); //�������� ������ ���̸� ����.


    }
}

        //Cast a ray starting from the second index finger joint to the tip of the index finger.
        //Only check for objects that are in the whiteboard layer.

/*
if (Physics.Raycast(originPoint, direction, out touch, distance, 1 << WHITEBOARD_LAYER))
{
    //Get the Whiteboard component of the whiteboard we obtain from the raycast.
    whiteboard = touch.collider.GetComponent<Whiteboard>();

    //touch.textureCoord gives us the texture coordinates at which our raycast
    //intersected the whiteboard. We can use this to tell the whiteboard where to
    //render the next circle.
    whiteboard.SetTouchPosition(touch.textureCoord.x, touch.textureCoord.y);

    //If the raycast intersects the board, it means we are touching the board
    whiteboard.ToggleTouch(true);
}
else
{
    if (whiteboard != null)
    {
        //If the raycast no longer intersects, stop drawing on the board.
        whiteboard.ToggleTouch(false);
    }
}


//If your thumb touches your pinky, reset the scene.
if (m_hand.GetFingerIsPinching(OVRHand.HandFinger.Pinky))
{
    SceneManager.LoadScene("WhiteboardScene");
}
}
}*/
