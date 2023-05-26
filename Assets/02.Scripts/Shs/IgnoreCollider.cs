using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ġ�ϰ� ����ġ�� ��ħ���� �ݶ��̴��� ���� �����ϱ� ���� ��ũ��Ʈ. ƨ�ܳ����⸦ ������.
public class IgnoreCollider : MonoBehaviour
{
    public Collider Paint_Target; //����ġ��
    public Collider Collider_this; //����ġ�� ��ħ��
    public Collider Paint_Trigger; //����ġ�� �浹 ������ ���� Trigger
    void Start()
    {
        Physics.IgnoreCollision(Paint_Target, Collider_this, true);//����ġ�ϰ� ����ġ�� ��ħ���� �ݶ��̴��� ���� ������.(ƨ�ܳ����� ����)
        Physics.IgnoreCollision(Paint_Target, Paint_Trigger, true);
        Physics.IgnoreCollision(Paint_Trigger, Collider_this, true);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
