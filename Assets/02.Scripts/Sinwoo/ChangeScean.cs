using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScean : MonoBehaviour
{
   /* public IEnumerator Joindelay()
    {
        yield return new WaitForSeconds(10.0f);
    }*/

    public void ToSelectScean()
    {
       SceneManager.LoadScene("Select Game");
    }
    public void ToStartScean()
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void ToGameScean()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void DelayToSelectScean()
    {
        Invoke("ToSelectScean", 5);
    }
}
