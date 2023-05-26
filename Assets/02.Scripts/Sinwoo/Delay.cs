using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{

    public IEnumerator Joindelay()
    {
        yield return new WaitForSeconds(5f);
    }

    public void delayJoin()
    {
        StartCoroutine(Joindelay());
    }
}
