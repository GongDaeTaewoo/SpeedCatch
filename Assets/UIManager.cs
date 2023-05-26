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
    public void UIOn(GameObject uiobject)//UI Object를 화면에 나타나게 하는 함수.
    {
        uiobject.SetActive(true);

    }
    
    public void UIOff(GameObject uiobject)//UI Objecct를 화면에서 지우는 함수.
    {
        uiobject.SetActive(false);

    }
}
