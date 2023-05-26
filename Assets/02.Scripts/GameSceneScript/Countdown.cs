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
    public float m_TotalSeconds = 1 * 60; // ī��Ʈ �ٿ� ��ü ��(5�� X 60��), �ν���Ʈ â���� �����ؾ� ��. 
    public TMP_Text m_Text;

    public void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
        m_Timer = CountdownTimer(false); // Text�� �ʱⰪ�� �־� �ֱ� ����
        //Gamemanager = .GetComponent<Gamemanager>();
        
        
      
    }

    public void Update()
    {
  
        if (gamemanager.turn)//���� ������� ���� ���ư� �� 
            m_IsPlaying = !m_IsPlaying;//�÷��� ���·� ����.

        if (m_IsPlaying)//�÷��� �ð� �߿� Ÿ�̸� �Լ� ����.
        {
            m_Timer = CountdownTimer();
        }

        // m_TotalSeconds�� �پ�鶧, ������ 0�� ����� ���� ������  
        if (m_TotalSeconds <= 0) //���ѽð��� ���� �� ��,
        {
            Debug.Log("ȣȣ");
            SetZero();
            //... ���⿡ ī��Ʈ �ٿ��� ���� �ɶ� [�̺�Ʈ]�� ������ �˴ϴ�. 
            Debug.Log("����");
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

    public void SetZero() //ī��Ʈ �ٿ��� ����� ���� �Լ�.
    {
        m_Timer = @"00:00:00.000";
        m_TotalSeconds = 0;
        m_IsPlaying = false;
    }
}