using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rito.TexturePainter;

public class RelayCatch : MonoBehaviour
{

    public static TexturePaintBrush texturepaintbrush;
    public static TexturePaintTarget texturepainttarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //3.1.) 모드를 설정하고 게임이 시작되면, (제한시간 2분 내)에 플레이어의 앞에 있는 UI에 제시문을 입력한다.(입력한 제시문은 스케치북 위에 생성된다.)
        //3.2.) 제시문의 입력이 완료되면, 옆 사람에게 돌리라는 UI가이드 문구를 띄운다.

        //3.2.1.) 제한 시간 안에 제시문을 전혀 아무것도 입력을 못했을 경우, 랜덤으로 제시문을 생성함.




        //3.3.) 스케치북을 옆 사람에게 받으면, 스케치북에 띄워진 제시문장을 보고, 이해한대로 그림을 그립니다.(제한시간 5분)


        //3.4.) 그림을 그린 후, 다시 옆 사람에게 돌림. 돌린 스케치북이 자신의 앞에 돌아왔을 때, 게임이 끝남.


        ////////************4.) 게임이 끝나면**************/////////

        //화면을 각자의 스케치북을 비추는 화면으로 전환하고, (문장, 그림 문장 --- 순서대로  )
        /*
         * if(릴레이 캐치 모드 설정 완료)
         * {
         *   if(
         *   
         * 
         *     if(게임 시작){
         *     
         *     
         *     
         *     -2분 카운트
         *     
         *     키보드, 제시문 입력창 UI 띄우기
         *     제시문 입력
         *     
         *     -if(2분 종료)
         *     키보드, 제시문 입력창 UI 지우기
         * 
         * 
         * }
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * }
         * 
         * 
         * 
         */







    }
}
