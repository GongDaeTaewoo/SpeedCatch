using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����ġ�ϰ� ����ġ�� ��ħ���� ��ġ�� ���� �����ֱ� ���� ��ũ��Ʈ.
//����ġ�� ��ħ���� ��ġ�� ����ġ���� ����.
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
