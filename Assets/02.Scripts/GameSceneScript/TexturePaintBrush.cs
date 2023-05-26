
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
    /// <summary> 검지�??�스쳐에 그림 그리�?</summary>
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

        //?�레?�어 마다 ?�기?�택가�?
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

        public Texture2D CopiedBrushTexture; // ?�시간으�??�상 칠하?�데 ?�용?�는 브러???�스�?카피�?
        public Vector2 sameUvPoint; // 직전 ?�레?�에 마우?��? ?�치???�??UV 지??(?�일 ?�치??중첩?�서 그리???�상 방�?)


        /***********************************************************************
        *                               Unity Events
        ***********************************************************************/

        private void Awake()
        {
            // ?�록??브러???�스쳐�? ?�을 경우, ??모양???�스�??�성
            if (brushTexture == null)
            {
                CreateDefaultBrushTexture();
            }

            CopyBrushTexture();


            //?��? ?�스�??�성.
            WhiteTexture = new Texture2D(1, 1);
            WhiteTexture.SetPixel(0, 0, Color.white); //?��??�으�?초기??
            WhiteTexture.Apply();




            // ?�인?�을 ?�해 ?�성 ?�더 ?�스�??�시 ?�당. (?�더�????�스처�? ?�정?�다.) 
            try
            {
                RenderTexture.active = paintTarget.renderTexture;
                Graphics.Blit(WhiteTexture, paintTarget.renderTexture);
                RenderTexture.active = null; // ?�성 ?�더 ?�스�??�제. (?�더링한 그림 ???��???)

                Debug.Log("?�더?�하?�게");
            }
            catch (Exception e)
            {
                Debug.Log("?�더?�하?�게?�패");
            }
        }

        // ?�스�??�???�인??그릴 ?�마??PNG�?지??경로???�?�하??코드.
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

             RenderTexture currentTexture = RenderTexture.active;//가??최근???�성?�된 ?�더 ?�스처�? currentTexture???�??
             RenderTexture copiedRenderTexture = new RenderTexture(width, height, 0);

             Graphics.Blit(texture, copiedRenderTexture); //???�더 ?�스처생??

             RenderTexture.active = copiedRenderTexture; //copiedRenderTexture�??�성?�함.

             Texture2D texture2d = new Texture2D(width, height, TextureFormat.RGB24, false);

             texture2d.ReadPixels(new Rect(0, 0, width, height), 0, 0);
             texture2d.Apply();

             RenderTexture.active = currentTexture; //currentTexture�?texture2D�?바꾼 ?? ?�시 ?�성?�함.

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

                if (PhotonNetwork.CurrentRoom.PlayerCount >= 1) //paint target1,2,3,4가?�오�?추후 ?�레?�어1??그림?� 1?�만 2?�그림�? 2?�만?�??
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

            /////////////그리�?코드/////////////
            if (Physics.Raycast(FingerRay.ray, out var hit, FingerRay.distance, sketchbookMask))
            {
                currentCollider = hit.collider;
                if (currentCollider != null) //?�른 ?�브?�트??붙어?�는 콜라?�더�?무시?�고 ?��?치북?�만 그리�??�한 코드.
                {
                    // ?�???��?치북) 참조 갱신
                   /* if (prevCollider == null || prevCollider != currentCollider)
                    {
                        prevCollider = currentCollider;
                        currentCollider.TryGetComponent(out paintTarget); //paintTarget ?�래?��? 참조.
                    }*/


                    // ?�일??지?�에??중첩?�여 ?�시 그리지 ?�음
                    if (sameUvPoint != hit.lightmapCoord)
                    {
                        sameUvPoint = hit.lightmapCoord;
                        Vector2 pixelUV = hit.lightmapCoord; //?�이?� 충돌??지?�의 ?�이?�맵 UV 좌표�?가?��? pixelUV???�??
                        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                        {
                            pixelUV *= paintTarget.resolution;// UV좌표???��?치북???�상?��? 곱함
                            paintTarget.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
                        }

                        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                        {
                            pixelUV *= paintTarget2.resolution;// UV좌표???��?치북???�상?��? 곱함
                            paintTarget2.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
                        }

                        if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
                        {
                            pixelUV *= paintTarget3.resolution;// UV좌표???��?치북???�상?��? 곱함
                            paintTarget3.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
                        }

                        if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
                        {
                            pixelUV *= paintTarget4.resolution;// UV좌표???��?치북???�상?��? 곱함
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
            if (b == 1) //p_number??1?�고 sketch가0?�면 첫번�??�인?�겟??그려�?pixeluv???�에??(update?�수)?�해지�??�문??건들지?�음
            {
                Vector2 pixelUV = a; //?�이?� 충돌??지?�의 ?�이?�맵 UV 좌표�?가?��? pixelUV???�??
                paintTarget.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
            }

            if (b == 2) //p_number??1?�고 sketch가0?�면 첫번�??�인?�겟??그려�?pixeluv???�에??(update?�수)?�해지�??�문??건들지?�음
            {
                Vector2 pixelUV = a; //?�이?� 충돌??지?�의 ?�이?�맵 UV 좌표�?가?��? pixelUV???�??
                paintTarget2.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
            }

            if (b == 3) //p_number??1?�고 sketch가0?�면 첫번�??�인?�겟??그려�?pixeluv???�에??(update?�수)?�해지�??�문??건들지?�음
            {
                Vector2 pixelUV = a; //?�이?� 충돌??지?�의 ?�이?�맵 UV 좌표�?가?��? pixelUV???�??
                paintTarget3.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
            }

            if (b == 4) //p_number??1?�고 sketch가0?�면 첫번�??�인?�겟??그려�?pixeluv???�에??(update?�수)?�해지�??�문??건들지?�음
            {
                Vector2 pixelUV = a; //?�이?� 충돌??지?�의 ?�이?�맵 UV 좌표�?가?��? pixelUV???�??
                paintTarget4.DrawTexture(pixelUV.x, pixelUV.y, brushSize, CopiedBrushTexture);
            }
        }
        /// <summary>
        /// ?�인 ?�보�??�?�하??코드.
        /// </summary>

        //?�른??검지가 SketchBook???�았?��? ?�을 ???�행?�는 ?�수.      
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Sketch"))
            {

                //?�인 ?�보�?STACK배열???�?�하??코드 
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    TexturePaintTarget copy_texture = paintTarget.DeepCopy();//깊�?복사.(?�재�??�?? call by value)

                    paintbrush.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
                    
                }
                if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    TexturePaintTarget copy_texture = paintTarget2.DeepCopy();//깊�?복사.(?�재�??�?? call by value)

                    paintbrush2.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
                }
                if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
                {
                    TexturePaintTarget copy_texture = paintTarget3.DeepCopy();//깊�?복사.(?�재�??�?? call by value)

                    paintbrush3.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
                }
                if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
                {
                    TexturePaintTarget copy_texture = paintTarget4.DeepCopy();//깊�?복사.(?�재�??�?? call by value)

                    paintbrush4.save_line_texture_undo.Push(RenderTextureTo2DTexture(copy_texture.renderTexture));
                }
                photonView.RPC("updateTrigger", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber);
                Debug.Log("?��?치북?�서 검지 ?�는 ?�간 ?�인 ?�보 undo배열???�???�공.");
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

        //브러??컬러 SETTING

        /*
        public void SetBrushColor(in Color color)
        {
            brushColor = color;
            CopyBrushTexture();
            Debug.Log("컬러복사?�앙?�ㅏ");
        }*/
        public void SetBrushColor(in Color color)
        {
            brushColor = color;
            //photonView.RPC("CopyBrushTexture",RpcTarget.All);
            CopyBrushTexture();
            Debug.Log("컬러복사?�앙?�ㅏ");
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



        //////////       밑에�??�봐?�됨~~~~~    /////////////////////////

        /***********************************************************************
        *                               Private Methods
        ***********************************************************************/


        /// <summary> 기본 ?�태(????브러???�스�??�성 </summary>

        public void CreateDefaultBrushTexture()
        {
            int res = 512;
            float hRes = res * 0.5f; //0~1??값으�?변?�해준 브러???�스�??�상??
            float sqrSize = hRes * hRes; //0~1 값으�?변?�해준 브러???�스�??�상?�의 sqare?�이�?

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

        /// <summary> ?�본 브러???�스�?-> ?�제 브러???�스�??�상 ?�용) 복제 </summary>
        /// 

        public void CopyBrushTexture()
        {
            if (brushTexture == null) return;

            // 기존??카피 ?�스쳐는 메모�??�제
            DestroyImmediate(CopiedBrushTexture);

            // ?�롭�??�당
            {

                CopiedBrushTexture = new Texture2D(brushTexture.width, brushTexture.height);
                CopiedBrushTexture.filterMode = FilterMode.Point; //?�터�?모드. ?�스처의 ?��??�을 뭉툭?�게 만든?? (?�명?�게)
#if UNITY_EDITOR
                CopiedBrushTexture.alphaIsTransparency = true; //?�스처�? 가?�왔?��? 체크??(unity editor ?�용 code?��?�??�처리문 처리.)
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

            Debug.Log("Copy Brush Texture"); //?�상 ?�용.
        }

        private Texture2D RenderTextureTo2DTexture(RenderTexture rt)//?�더 ?�스처�? 2Dtexture�??��??�하???�수.
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
        // ?�상 변�?감�??�여 브러???�스�??�시 복제
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
