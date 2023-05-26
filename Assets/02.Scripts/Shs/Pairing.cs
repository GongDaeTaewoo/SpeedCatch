using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//스케치북과 스케치북 받침대의 위치를 서로 맞춰주기 위한 스크립트.
//스케치북 받침대의 위치를 스케치북이 따라감.
public class Pairing : MonoBehaviour
{
    public GameObject following;
   
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = following.transform.position;
        transform.rotation = following.transform.rotation;
        //transform.localScale = following.transform.localScale;

    }
}
