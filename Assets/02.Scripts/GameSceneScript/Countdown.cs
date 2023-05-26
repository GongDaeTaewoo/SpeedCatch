using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Countdown : MonoBehaviour
{
    float timer = 0.0f;
    int wait = 5;
    bool oncedelay = true;

    public string m_Timer = @"00:00:00.000";
    public Gamemanager gamemanager;
 
    public bool m_IsPlaying;
    public float m_TotalSeconds = 1 * 60; // 카운트 다운 전체 초(5분 X 60초), 인스펙트 창에서 수정해야 함. 
    public TMP_Text m_Text;

    public void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
        m_Timer = CountdownTimer(false); // Text에 초기값을 넣어 주기 위해
        //Gamemanager = .GetComponent<Gamemanager>();
        
        
      
    }

    public void Update()
    {
  
        if (gamemanager.turn)//다음 사람에게 턴이 돌아갈 때 
            m_IsPlaying = !m_IsPlaying;//플레이 상태로 갱신.

        if (m_IsPlaying)//플레이 시간 중에 타이머 함수 실행.
        {
            m_Timer = CountdownTimer();
        }

        // m_TotalSeconds이 줄어들때, 완전히 0에 맞출수 없기 때문에  
        if (m_TotalSeconds <= 0) //제한시간이 종료 될 때,
        {
            Debug.Log("호호");
            SetZero();
            //... 여기에 카운트 다운이 종료 될때 [이벤트]를 넣으면 됩니다. 
            Debug.Log("헤헷");
        }

        if (m_Text)
            m_Text.text = m_Timer;
    }

    public string CountdownTimer(bool IsUpdate = true)
    {
        if (IsUpdate)
            m_TotalSeconds -= Time.deltaTime;

        TimeSpan timespan = TimeSpan.FromSeconds(m_TotalSeconds);
        string timer = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);

        return timer;
    }

    public void SetZero() //카운트 다운이 종료될 때의 함수.
    {
        m_Timer = @"00:00:00.000";
        m_TotalSeconds = 0;
        m_IsPlaying = false;
    }
}