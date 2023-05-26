
// https://www.patreon.com/posts/rendertexture-15961186
// https://pastebin.com/rMx1PVXi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;


namespace Rito.TexturePainter
{
    /// <summary> ê²€ì§€ë¡??ìŠ¤ì³ì— ê·¸ë¦¼ ê·¸ë¦¬ê¸?</summary>
    [DisallowMultipleComponent]
    public class TexturePaintBrush : MonoBehaviourPunCallbacks
    {
        /***********************************************************************
        *                               Public Field
        ***********************************************************************/
        public NetworkManager networkManager;
        [Range(0.01f, 1f)] public float brushSize = 0.1f;
        public Texture2D brushTexture;
        public Color brushColor = new Color();
        public Color col2 = new Color();

        public RightFingerRay FingerRay;

        //?Œë ˆ?´ì–´ ë§ˆë‹¤ ?ê¸°?¤íƒê°€ì§?
        public Stack<Texture2D> save_line_texture_undo = new Stack<Texture2D>();
        public Stack<Texture2D> save_line_texture_redo = new Stack<Texture2D>();
        public Texture2D undopop = null;
        public Texture2D redopop = null;

      


        public Texture2D WhiteTexture;
        public Texture2D PaintTexture;
        public Texture2D init;

        public Texture2D reinit;
        public int resolution = 512;
        public Collider currentCollider;



        /***********************************************************************
        *                               Private Fields
        ***********************************************************************/

        public Gamemanager gamemanager;

        public TexturePaintTarget paintTarget; //player1
        public TexturePaintTarget paintTarget2; //2
        public TexturePaintTarget paintTarget3;//3
        public TexturePaintTarget paintTarget4;//4

        public TexturePaintBrush paintbrush;
        public TexturePaintBrush paintbrush2;
        public TexturePaintBrush paintbrush3;
        public TexturePaintBrush paintbrush4;

        private Collider prevCollider;
        private RaycastHit hit;
        int sketchbookMask = 1 << 6;

        public Texture2D CopiedBrushTexture; // ?¤ì‹œê°„ìœ¼ë¡??‰ìƒ ì¹ í•˜?”ë° ?¬ìš©?˜ëŠ” ë¸ŒëŸ¬???ìŠ¤ì³?ì¹´í”¼ë³?
        public Vector2 sameUvPoint; // ì§ì „ ?„ë ˆ?„ì— ë§ˆìš°?¤ê? ?„ì¹˜???€??UV ì§€??(?™ì¼ ?„ì¹˜??ì¤‘ì²©?´ì„œ ê·¸ë¦¬???„ìƒ ë°©ì?)


        /***********************************************************************
        *                               Unity Events
        ***********************************************************************/

        private void Awake()
        {
            // ?±ë¡??ë¸ŒëŸ¬???ìŠ¤ì³ê? ?†ì„ ê²½ìš°, ??ëª¨ì–‘???ìŠ¤ì³??ì„±
            if (brushTexture == null)
            {
                CreateDefaultBrushTexture();
            }

            CopyBrushTexture();


            //?˜ì? ?ìŠ¤ì²??ì„±.
            WhiteTexture = new Texture2D(1, 1);
            WhiteTexture.SetPixel(0, 0, Color.white); //?˜ì??‰ìœ¼ë¡?ì´ˆê¸°??
            WhiteTexture.Apply();




            // ?˜ì¸?…ì„ ?„í•´ ?œì„± ?Œë” ?ìŠ¤ì³??„ì‹œ ? ë‹¹. (?Œë”ë§????ìŠ¤ì²˜ë? ?¤ì •?œë‹¤.) 
            try
            {
                RenderTexture.active = paintTarget.renderTexture;
                Graphics.Blit(WhiteTexture, paintTarget.renderTexture);
                RenderTexture.active = null; // ?œì„± ?Œë” ?ìŠ¤ì³??´ì œ. (?Œë”ë§í•œ ê·¸ë¦¼ ???˜í???)

                Debug.Log("?Œë”?í•˜?—ê²Œ");
            }
            catch (Exception e)
            {
                Debug.Log("?Œë”?í•˜?—ê²Œ?¤íŒ¨");
            }
        }

        // ?ìŠ¤ì²??€???¼ì¸??ê·¸ë¦´ ?Œë§ˆ??PNGë¡?ì§€??ê²½ë¡œ???€?¥í•˜??ì½”ë“œ.
        /* public void SaveTexture(RenderTexture texture, string directoryPath, string fileName)
         {

             if (true == string.IsNullOrEmpty(directoryPath))
             {
                 return;
             }

             if (false == Directory.Exists(directoryPath))
             {
                 Debug.Log("none_path");

                 Directory.CreateDirectory(directoryPath);
             }


             int width = texture.width;
             int height = texture.height;

             RenderTexture currentTexture = RenderTexture.active;//ê°€??ìµœê·¼???œì„±?”ëœ ?Œë” ?ìŠ¤ì²˜ë? currentTexture???€??
             RenderTexture copiedRenderTexture = new RenderTexture(width, height, 0);

             Graphics.Blit(texture, copiedRenderTexture); //???Œë” ?ìŠ¤ì²˜ìƒ??

             RenderTexture.active = copiedRenderTexture; //copiedRenderTextureë¥??œì„±?”í•¨.

             Texture2D texture2d = new Texture2D(width, height, TextureFormat.RGB24, false);

             texture2d.ReadPixels(new Rect(0, 0, width, height), 0, 0);
             texture2d.Apply();

             RenderTexture.active = currentTexture; //currentTextureë¥?texture2Dë¡?ë°”ê¾¼ ?? ?¤ì‹œ ?œì„±?”í•¨.

             byte[] texturePNGBytes = texture2d.EncodeToPNG();

             string filePath = directoryPath + fileName + ".png";

             File.WriteAllBytes(filePath, texturePNGBytes);
         }*/

        void Start()
        {

            networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
            col2 = Color.black;
            brushColor = Color.black;
            SetBrushColor(in col2);

            //photonView.RPC("SetBrushColor", RpcTarget.All, col2);
            PaintTexture = new Texture2D(1, 1);



        }


        private void Update()
        {
            if (networkManager.isSpon == true)
            {

                if (PhotonNetwork.CurrentRoom.PlayerCount >= 1) //paint target1,2,3,4ê°€?¸ì˜¤ê¸?ì¶”í›„ ?Œë ˆ?´ì–´1??ê·¸ë¦¼?€ 1?ë§Œ 2?˜ê·¸ë¦¼ì? 2?ë§Œ?€??
                {
                    paintTarget = gamemanager.TexturePaintTarget[0];
                    paintbrush = gamemanager.TexturePaintBrush[0];
                    

                }
                if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
                {
                    paintTarget2 = gamemanager.TexturePaintTarget[1];
                    paintbrush2 = gamemanager.TexturePaintBrush[1];

                }
                if (PhotonNetwork.CurrentRoom.PlayerCount >= 3)
                {
                    paintTarget3 = gamemanager.TexturePaintTarget[2];
                    paintbrush3 = gamemanager.TexturePaintBrush[2];

                }
                if (PhotonNetwork.CurrentRoom.PlayerCount >= 4)
                {
                    paintTarget4 = gamemanager.TexturePaintTarget[3];
                    paintbrush4 = gamemanager.TexturePaintBrush[3];

                }
            }

            /////////////ê·¸ë¦¬ê¸?ì½”ë“œ/////////////
            if (Physics.Raycast(FingerRay.ray, out var hit, FingerRay.distance, sketchbookMask))
            {
                currentCollider = hit.collider;
                if (currentCollider != null) //?¤ë¥¸ ?¤ë¸Œ?íŠ¸??ë¶™ì–´?ˆëŠ” ì½œë¼?´ë”ë¥?ë¬´ì‹œ?˜ê³  ?¤ì?ì¹˜ë¶?ë§Œ ê·¸ë¦¬ê¸??„í•œ ì½”ë“œ.
                {
                    // ?€???¤ì?ì¹˜ë¶) ì°¸ì¡° ê°±ì‹ 
                   /* if (prevCollider == null || prevCollider != currentCollider)
                    {
                        prevCollider = currentCollider;
                        currentCollider.TryGetComponent(out paintTarget); //paintTarget ?´ë˜?¤ë? ì°¸ì¡°.
                    }*/


                    // ?™ì¼??ì§€?ì—??ì¤‘ì²©?˜ì—¬ ?¤ì‹œ ê·¸ë¦¬ì§€ ?ŠìŒ
                    if (sameUvPoint != hit.lightmapCoord)
                    {
                        sameUvPoint = hit.lightmapCoord;
                        Vector2 pixelUV = hit.lightmapCoord; //?ˆì´?€ ì¶©ëŒ??ì§€?ì˜ ?¼ì´?¸ë§µ UV ì¢Œí‘œë¥?ê°€?¸ì? pixelUV???€??
                        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                        {
                            pixelUV *= paintTarget.resolution;// UVì¢Œí‘œ???¤ì?ì¹˜ë¶???´ìƒ?„ë? ê³±í•¨
                            paintTarget.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
                        }

                        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                        {
                            pixelUV *= paintTarget2.resolution;// UVì¢Œí‘œ???¤ì?ì¹˜ë¶???´ìƒ?„ë? ê³±í•¨
                            paintTarget2.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
                        }

                        if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
                        {
                            pixelUV *= paintTarget3.resolution;// UVì¢Œí‘œ???¤ì?ì¹˜ë¶???´ìƒ?„ë? ê³±í•¨
                            paintTarget3.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
                        }

                        if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
                        {
                            pixelUV *= paintTarget4.resolution;// UVì¢Œí‘œ???¤ì?ì¹˜ë¶???´ìƒ?„ë? ê³±í•¨
                            paintTarget4.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
                        }
                        photonView.RPC("updatedraw", RpcTarget.All, pixelUV, PhotonNetwork.LocalPlayer.ActorNumber);

                    }
                    Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);



                }
            }

        }
        [PunRPC]
        public void updatedraw(Vector2 a, int b)
        {
            if (b == 1) //p_number??1?´ê³  sketchê°€0?´ë©´ ì²«ë²ˆì§??˜ì¸?¸ê²Ÿ??ê·¸ë ¤ì§?pixeluv???„ì—??(update?¨ìˆ˜)?•í•´ì§€ê¸??Œë¬¸??ê±´ë“¤ì§€?ŠìŒ
            {
                Vector2 pixelUV = a; //?ˆì´?€ ì¶©ëŒ??ì§€?ì˜ ?¼ì´?¸ë§µ UV ì¢Œí‘œë¥?ê°€?¸ì? pixelUV???€??
                paintTarget.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
            }

            if (b == 2) //p_number??1?´ê³  sketchê°€0?´ë©´ ì²«ë²ˆì§??˜ì¸?¸ê²Ÿ??ê·¸ë ¤ì§?pixeluv???„ì—??(update?¨ìˆ˜)?•í•´ì§€ê¸??Œë¬¸??ê±´ë“¤ì§€?ŠìŒ
            {
                Vector2 pixelUV = a; //?ˆì´?€ ì¶©ëŒ??ì§€?ì˜ ?¼ì´?¸ë§µ UV ì¢Œí‘œë¥?ê°€?¸ì? pixelUV???€??
                paintTarget2.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
            }

            if (b == 3) //p_number??1?´ê³  sketchê°€0?´ë©´ ì²«ë²ˆì§??˜ì¸?¸ê²Ÿ??ê·¸ë ¤ì§?pixeluv???„ì—??(update?¨ìˆ˜)?•í•´ì§€ê¸??Œë¬¸??ê±´ë“¤ì§€?ŠìŒ
            {
                Vector2 pixelUV = a; //?ˆì´?€ ì¶©ëŒ??ì§€?ì˜ ?¼ì´?¸ë§µ UV ì¢Œí‘œë¥?ê°€?¸ì? pixelUV???€??
                paintTarget3.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
            }

            if (b == 4) //p_number??1?´ê³  sketchê°€0?´ë©´ ì²«ë²ˆì§??˜ì¸?¸ê²Ÿ??ê·¸ë ¤ì§?pixeluv???„ì—??(update?¨ìˆ˜)?•í•´ì§€ê¸??Œë¬¸??ê±´ë“¤ì§€?ŠìŒ
            {
                Vector2 pixelUV = a; //?ˆì´?€ ì¶©ëŒ??ì§€?ì˜ ?¼ì´?¸ë§µ UV ì¢Œí‘œë¥?ê°€?¸ì? pixelUV???€??
                paintTarget4.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
            }
        }
        /// <summary>
        /// ?¼ì¸ ?•ë³´ë¥??€?¥í•˜??ì½”ë“œ.
        /// </summary>

        //?¤ë¥¸??ê²€ì§€ê°€ SketchBook???¿ì•˜?¤ê? ?ì„ ???¤í–‰?˜ëŠ” ?¨ìˆ˜.      
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Sketch"))
            {

                //?¼ì¸ ?•ë³´ë¥?STACKë°°ì—´???€?¥í•˜??ì½”ë“œ 
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    TexturePaintTarget copy_texture = paintTarget.DeepCopy();//ê¹Šì?ë³µì‚¬.(?„ì¬ê°??€?? call by value)

                    paintbrush.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
                    
                }
                if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    TexturePaintTarget copy_texture = paintTarget2.DeepCopy();//ê¹Šì?ë³µì‚¬.(?„ì¬ê°??€?? call by value)

                    paintbrush2.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
                }
                if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
                {
                    TexturePaintTarget copy_texture = paintTarget3.DeepCopy();//ê¹Šì?ë³µì‚¬.(?„ì¬ê°??€?? call by value)

                    paintbrush3.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
                }
                if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
                {
                    TexturePaintTarget copy_texture = paintTarget4.DeepCopy();//ê¹Šì?ë³µì‚¬.(?„ì¬ê°??€?? call by value)

                    paintbrush4.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
                }
                photonView.RPC("updateTrigger", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber);
                Debug.Log("?¤ì?ì¹˜ë¶?ì„œ ê²€ì§€ ?¼ëŠ” ?œê°„ ?¼ì¸ ?•ë³´ undoë°°ì—´???€???±ê³µ.");
                Debug.Log(save_line_texture_undo.Count);
                //  }


            }

        }
        [PunRPC]
        public void updateTrigger(int who)
        {
            if (who==1)
            {
                TexturePaintTarget copy_texture = paintTarget.DeepCopy();
                paintbrush.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
            }
            else if(who==2)
            {
                TexturePaintTarget copy_texture = paintTarget2.DeepCopy();
                paintbrush2.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
            }
            else if (who == 3)
            {
                TexturePaintTarget copy_texture = paintTarget3.DeepCopy();
                paintbrush3.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
            }
            else if (who == 4)
            {
                TexturePaintTarget copy_texture = paintTarget4.DeepCopy();
                paintbrush4.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
            }
        }

        /***********************************************************************
        *                               Public Methods
        ***********************************************************************/

        //ë¸ŒëŸ¬??ì»¬ëŸ¬ SETTING

        /*
        public void SetBrushColor(in Color color)
        {
            brushColor = color;
            CopyBrushTexture();
            Debug.Log("ì»¬ëŸ¬ë³µì‚¬?„ì•™?„ã…");
        }*/
        public void SetBrushColor(in Color color)
        {
            brushColor = color;
            //photonView.RPC("CopyBrushTexture",RpcTarget.All);
            CopyBrushTexture();
            Debug.Log("ì»¬ëŸ¬ë³µì‚¬?„ì•™?„ã…");
            Vector4 a = color;
            Vector3 b = a;
            photonView.RPC("updateColor", RpcTarget.Others, b);
        }

        [PunRPC]
        public void updateColor(Vector3 color)
        {
            Vector4 c = color;
            c.w = 1.0f;
            Color d = c;
            //brushColor = color;
            brushColor = d;
            CopyBrushTexture();
        }



        //////////       ë°‘ì—ê±??ˆë´?„ë¨~~~~~    /////////////////////////

        /***********************************************************************
        *                               Private Methods
        ***********************************************************************/


        /// <summary> ê¸°ë³¸ ?•íƒœ(????ë¸ŒëŸ¬???ìŠ¤ì³??ì„± </summary>

        public void CreateDefaultBrushTexture()
        {
            int res = 512;
            float hRes = res * 0.5f; //0~1??ê°’ìœ¼ë¡?ë³€?˜í•´ì¤€ ë¸ŒëŸ¬???ìŠ¤ì²??´ìƒ??
            float sqrSize = hRes * hRes; //0~1 ê°’ìœ¼ë¡?ë³€?˜í•´ì¤€ ë¸ŒëŸ¬???ìŠ¤ì²??´ìƒ?„ì˜ sqare?¬ì´ì¦?

            brushTexture = new Texture2D(res, res);
            brushTexture.filterMode = FilterMode.Point;
#if UNITY_EDITOR
            brushTexture.alphaIsTransparency = true;
#endif
            for (int y = 0; y < res; y++)
            {
                for (int x = 0; x < res; x++)
                {
                    // Sqaure Length From Center
                    float sqrLen = (hRes - x) * (hRes - x) + (hRes - y) * (hRes - y);
                    float alpha = Mathf.Max(sqrSize - sqrLen, 0f) / sqrSize;

                    //brushTexture.SetPixel(x, y, (sqrLen < sqrSize ? brushColor : Color.clear));
                    brushTexture.SetPixel(x, y, new Color(0f, 0f, 0f, alpha));
                }
            }

            brushTexture.Apply();
        }

        /// <summary> ?ë³¸ ë¸ŒëŸ¬???ìŠ¤ì³?-> ?¤ì œ ë¸ŒëŸ¬???ìŠ¤ì³??‰ìƒ ?ìš©) ë³µì œ </summary>
        /// 

        public void CopyBrushTexture()
        {
            if (brushTexture == null) return;

            // ê¸°ì¡´??ì¹´í”¼ ?ìŠ¤ì³ëŠ” ë©”ëª¨ë¦??´ì œ
            DestroyImmediate(CopiedBrushTexture);

            // ?ˆë¡­ê²?? ë‹¹
            {

                CopiedBrushTexture = new Texture2D(brushTexture.width, brushTexture.height);
                CopiedBrushTexture.filterMode = FilterMode.Point; //?„í„°ë§?ëª¨ë“œ. ?ìŠ¤ì²˜ì˜ ?½ì??¤ì„ ë­‰íˆ­?˜ê²Œ ë§Œë“ ?? (? ëª…?˜ê²Œ)
#if UNITY_EDITOR
                CopiedBrushTexture.alphaIsTransparency = true; //?ìŠ¤ì²˜ë? ê°€?¸ì™”?”ì? ì²´í¬??(unity editor ?„ìš© code?´ë?ë¡??„ì²˜ë¦¬ë¬¸ ì²˜ë¦¬.)
#endif
            }

            int height = brushTexture.height;
            int width = brushTexture.width;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = brushColor;
                    c.a *= brushTexture.GetPixel(x, y).a;

                    CopiedBrushTexture.SetPixel(x, y, c);
                }
            }

            CopiedBrushTexture.Apply();

            Debug.Log("Copy Brush Texture"); //?‰ìƒ ?ìš©.
        }

        private Texture2D RenderTextureTo2DTexture(RenderTexture rt)//?Œë” ?ìŠ¤ì²˜ë? 2Dtextureë¡??•ë??˜í•˜???¨ìˆ˜.
        {
            var texture = new Texture2D(rt.width, rt.height, rt.graphicsFormat, 0);
            RenderTexture.active = rt;
            texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            texture.Apply();

            RenderTexture.active = null;

            return texture;
        }

        /***********************************************************************
        *                               Editor Only
        ***********************************************************************/

#if UNITY_EDITOR
        // ?‰ìƒ ë³€ê²?ê°ì??˜ì—¬ ë¸ŒëŸ¬???ìŠ¤ì³??¤ì‹œ ë³µì œ
        private Color prevBrushColor;
        private float brushTextureUpdateCounter = 0f;
        private const float BrushTextureUpdateCounterInitValue = 0.7f;

        private void OnValidate()
        {
            if (Application.isPlaying && prevBrushColor != brushColor)
            {
                brushTextureUpdateCounter = BrushTextureUpdateCounterInitValue;
                prevBrushColor = brushColor;
            }
        }
#endif

#if UNITY_EDITOR

        [System.Diagnostics.Conditional("UNITY_EDITOR")]

        private void UpdateBrushColorOnEditor()
        {
            if (brushTextureUpdateCounter > 0f &&
                brushTextureUpdateCounter <= BrushTextureUpdateCounterInitValue)
            {
                brushTextureUpdateCounter -= Time.deltaTime;
            }

            if (brushTextureUpdateCounter < 0f)
            {
                CopyBrushTexture();
                brushTextureUpdateCounter = 9999f;
            }
        }
#endif
    }
}
