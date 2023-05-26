using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texture2DEncodeToJPG: MonoBehaviour
{
    // Start is called before the first frame update
    void Texture2DToJPG(Texture2D texture)
    {
        ImageConversion.EncodeToJPG(texture);
    }
}
