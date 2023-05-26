using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour 
{
 public Dialogue info;

    public void TUtoTextTrigger()
    {
    var system = FindObjectOfType<TutoGuideSystem>();
    system.Begin(info);
  /*  GameObject.Find("Main Camera").transform.Find("Canvas").
        transform.Find("AskTutorial").transform.Find("NextBtn1").
        gameObject.SetActive(true);*/
    }

   

}
