using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redbutton : MonoBehaviour
{
    public Gamemanager Gamemanager;

    public void ButtonStart()
    {
        Gamemanager.start();
    }
}