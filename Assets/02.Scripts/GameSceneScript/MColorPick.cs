

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.TexturePainter;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;



public class MColorPick : MonoBehaviourPunCallbacks

{
    public Renderer rend;
    public TexturePaintBrush texturepaintbrush;

    void Start() 
    {
        //rend = transform.Find("Button").gameObject.transform.Find("Button").gameObject.GetComponent<Renderer>(); 
       // rend = transform.Find("Button").gameObject.GetComponent<Renderer>(); ;

        //rend = GetComponent<Renderer>();// renderer 컴포넌트 변수

    }

    [PunRPC]
    public void ColorPick()
    {       
        texturepaintbrush.col2 = rend.material.color;
        texturepaintbrush.SetBrushColor(in texturepaintbrush.col2);
    }

    [PunRPC]
    public void ColorChange()
    {

        //texturepaintbrush.SetBrushColor(in texturepaintbrush.col2);
    }

    

}

