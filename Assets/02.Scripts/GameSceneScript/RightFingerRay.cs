
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 오른손에서 나가는 레이를 정의한 스크립트.
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

        //if (indexTip == null && m_skeleton.IsInitialized)//손이 인식이 되었을 때.
        if (indexTip == null && m_skeleton.IsInitialized)//손이 인식이 되었을 때.
        {
                Debug.Log("Skeleton initialized");
                indexTip = m_skeleton.Bones[(int)OVRPlugin.BoneId.Hand_IndexTip].Transform;//검지 끝의좌표값 indextip
                indexDistal = m_skeleton.Bones[(int)OVRPlugin.BoneId.Hand_Index2].Transform; //검지의 2번째 마디 꺾이는 부분의 좌표값 indexdistal.
                indexCol = m_skeleton.Capsules[(int)OVRPlugin.BoneId.Hand_Index3].CapsuleCollider;//검지의 2번째 마디 캡슐 콜라이더 indexCol.                                         
                indexCol2 = m_skeleton.Capsules[(int)OVRPlugin.BoneId.Hand_Index2].CapsuleCollider;//검지의 1번째 마디 캡슐 콜라이더 indexCol2



                indexCol.isTrigger = true; //검지의 캡슐 콜라이더를 트리거로 만든다.
                indexCol2.isTrigger = true;
            //Destroy(indexCol.gameObject.GetComponent<Rigidbody>()); //검지의 캡슐 콜라이더가 달려있는 오브젝트의 리지드바디를 지운다.

            indexCol.gameObject.tag = "Sketch"; //검지의 캡슐 콜라이더가 붙어있는 오브젝트의 태그를 Sketch라고 할당해준다.
            indexCol2.gameObject.tag = "Sketch";

            ////////주의 : Paint Target의 Trigger 오브젝트에 Brush스크립트가 붙어있어야함.
               

            

          /*  public OVRBoneCapsule(short boneIndex, Rigidbody capsuleRigidBody, CapsuleCollider capsuleCollider)
            {
                BoneIndex = boneIndex;
                CapsuleRigidbody = capsuleRigidBody;
                CapsuleCollider = capsuleCollider;
            }
          */


             }
      

            /////////////////그림은 레이로 그림/////////////////////
            if (!indexTip) return; //손이 인식되지 않았다면 리턴.
      
        originPoint = indexDistal.position;
        targetPoint = indexTip.position;

        Vector3 direction = Vector3.Normalize(targetPoint - originPoint);//검지에서 나가는 레이의 방향
        distance = Vector3.Distance(originPoint, targetPoint); //검지에서 나가는 레이의 거리.
       
        ray = new Ray(originPoint, direction); //검지에서 나가는 레이를 정의.


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
