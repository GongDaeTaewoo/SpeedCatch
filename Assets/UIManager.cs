using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    //public GameObject VirtualKeyboardCanvas;
   
    public Gamemanager gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UIOn(GameObject uiobject)//UI Object�� ȭ�鿡 ��Ÿ���� �ϴ� �Լ�.
    {
        uiobject.SetActive(true);

    }
    
    public void UIOff(GameObject uiobject)//UI Objecct�� ȭ�鿡�� ����� �Լ�.
    {
        uiobject.SetActive(false);

    }
}
