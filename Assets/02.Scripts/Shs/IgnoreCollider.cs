using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스케치북과 스케치북 받침대의 콜라이더를 서로 무시하기 위한 스크립트. 튕겨나가기를 방지함.
public class IgnoreCollider : MonoBehaviour
{
    public Collider Paint_Target; //스케치북
    public Collider Collider_this; //스케치북 받침대
    public Collider Paint_Trigger; //스케치북 충돌 감지를 위한 Trigger
    void Start()
    {
        Physics.IgnoreCollision(Paint_Target, Collider_this, true);//스케치북과 스케치북 받침대의 콜라이더를 서로 무시함.(튕겨나가기 방지)
        Physics.IgnoreCollision(Paint_Target, Paint_Trigger, true);
        Physics.IgnoreCollision(Paint_Trigger, Collider_this, true);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
