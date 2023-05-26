using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Jumsu : MonoBehaviour
{
    public TextMeshPro countText1;
    public TextMeshPro countText2;
    public TextMeshPro countText3;
    public TextMeshPro countText4;
    public static int Score1;
    public static int Score2;
    public static int Score3;
    public static int Score4;
    // Start is called before the first frame update
    void Start()
    {
        countText1.text = "player1:" + Score1;
        countText2.text = "player2:" + Score2;
        countText3.text = "player3:" + Score3;
        countText4.text = "player4:" + Score4;
    }

    // Update is called once per frame
    void Update()
    {
        countText1.text = "player1:" + Score1;
        countText2.text = "player2:" + Score2;
        countText3.text = "player3:" + Score3;
        countText4.text = "player4:" + Score4;
    }
}