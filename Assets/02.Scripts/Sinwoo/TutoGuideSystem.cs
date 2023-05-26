using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TutoGuideSystem : MonoBehaviour
{
    //public TextMeshProUGUI textName;
    public TextMeshProUGUI textSentence;



    Queue<string> sentences = new Queue<string>();
    // public Animator anim;

    public void Begin(Dialogue info)
    {
        // GameObject.Find("CanvasMenu").transform.Find("guide").gameObject.SetActive(true);
        // anim.SetBool("isOpen", true);
        sentences.Clear();

        // textName.text = info.name;


        foreach (var sentence in info.sentnences)
        {
            sentences.Enqueue(sentence);
        }
        Next();
    }

    public void Next()
    {
        /*if (sentences.Count == 1)
        {
            GameObject.Find("Main Camera").transform.Find("Canvas").
                       transform.Find("AskTutorial").
                       transform.Find("YesButton").gameObject.SetActive(true);
            GameObject.Find("Main Camera").transform.Find("Canvas").
                transform.Find("AskTutorial").
                transform.Find("NoButton").gameObject.SetActive(true);
        }*/
        if (sentences.Count == 0)
        {
            End();
            return;
        }
        textSentence.text = sentences.Dequeue();
        //textSentence.text = string.Empty;
        //StopAllCoroutines();
        //StartCoroutine(TypeSentence(sentences.Dequeue()));
    }

   /* IEnumerator TypeSentence(string sentence)
    {
        foreach (var letter in sentence)
        {
            textSentence.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }*/

    private void End()
    {
        // anim.SetBool("isOpen", false);
        // textName.text = string.Empty;
        // textSentence.text = string.Empty;

    }
}


