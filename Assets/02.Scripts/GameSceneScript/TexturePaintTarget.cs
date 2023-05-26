
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;



namespace Rito.TexturePainter
{
    /*
     * [NOTE]
     * 
     * - 반드??Rito/PaintTexture 마테리얼 ?�용
     * - 마테리얼??Enable GPU Instancing 체크
     * 
     */

    /// <summary> 
    /// 그림 그려�??�??
    /// </summary>
    [DisallowMultipleComponent]
    public class TexturePaintTarget : MonoBehaviourPunCallbacks
    {
        /***********************************************************************
        *                               Static Fields
        ***********************************************************************/
        #region .
        /*private static Texture2D ClearTex
        {
            get
            {
                if (_clearTex == null)
                {
                    _clearTex = new Texture2D(1, 1);
                    //_clearTex.SetPixel(0, 0, Color.clear); //?�명?�으�?초기??
                    _clearTex.SetPixel(0, 0, Color.white); //?��??�으�?초기??
                    _clearTex.Apply();
                }
                return _clearTex;
            }
        }
        */

       
        
       /* private MaterialPropertyBlock TextureBlock
        {
            get
            {
                if (_textureBlock == null)
                {
                    _textureBlock = new MaterialPropertyBlock();
                }
                return _textureBlock;
            }
        }
       */
      

        public static Texture2D _clearTex;
        public static MaterialPropertyBlock _textureBlock;

        private static readonly string PaintTexPropertyName = "_PaintTex";
        private static readonly string MainTexPropertyName = "_MainTex";
        private static readonly string ColorPropertyName = "_Color";
        






        #endregion
        /***********************************************************************
        *                               Private Fields
        ***********************************************************************/
        #region .
        private MeshRenderer _mr;

        #endregion
        /***********************************************************************
        *                               Public Fields
        ***********************************************************************/
        #region .
        public int resolution = 512;
        public RenderTexture renderTexture = null;


        #endregion
        /***********************************************************************
        *                               Unity Magics
        ***********************************************************************/
        #region .
        public void Awake()
        {


            Init();
            InitRenderTexture();
        }

        #endregion
        /***********************************************************************
        *                               Private Methods
        ***********************************************************************/
        #region .

        private void Init()
        {
            TryGetComponent(out _mr); //메쉬 ?�더?�의 컴포?�트�?참조??(초기??
        }

        /// <summary> ?�더 ?�스�?초기??</summary>
        public void InitRenderTexture()
        {
            renderTexture = new RenderTexture(resolution, resolution, 32);// 512x512 pixel???�더 ?�스처�? ?�성.


            _clearTex = new Texture2D(1, 1);
            //_clearTex.SetPixel(0, 0, Color.clear); //?�명?�으�?초기??
            _clearTex.SetPixel(0, 0, Color.white); //?��??�으�?초기??
            _clearTex.Apply();


            _textureBlock = new MaterialPropertyBlock();


            RenderTexture.active = renderTexture;
            Graphics.Blit(_clearTex, renderTexture); //?�더?�스처의 ?�을 초기?�함.
            RenderTexture.active = null;

            Debug.Log("?�더?�스�???초기???�공");
            // 마테리얼 ?�로?�티 블록 ?�용?�여 배칭 ?��??�고
            // 마테리얼???�로?�티???�더 ?�스�??�어주기
            _textureBlock.SetTexture(PaintTexPropertyName, renderTexture);
            _mr.SetPropertyBlock(_textureBlock);
            //_mr.SetPropertyBlock(TextureBlock);


            //미림
            _textureBlock.SetTexture(MainTexPropertyName, renderTexture);
            _mr.SetPropertyBlock(_textureBlock);
            //_mr.SetPropertyBlock(TextureBlock);

            _textureBlock.SetTexture(ColorPropertyName, renderTexture);
            _mr.SetPropertyBlock(_textureBlock);
            Debug.Log("?�더?�스�??�로?�티 초기???�공");


        }

        #endregion
        /***********************************************************************
        *                               Public Methods
        ***********************************************************************/
        #region .
        /// <summary> ?�더 ?�스쳐에 브러???�스쳐로 그리�?</summary>
        [PunRPC]
        public void DrawTexture(float posX, float posY, float brushSize, Texture2D brushTexture)
        {
            RenderTexture.active = renderTexture; // ?�인?�을 ?�해 ?�성 ?�더 ?�스�??�시 ?�당. (?�더�????�스처�? ?�정?�다.) 
            GL.PushMatrix();                      // 매트�?�� ?�??
            GL.LoadPixelMatrix(0, resolution, resolution, 0); // ?�맞?� ?�기�??��? 매트�?�� ?�정

            float brushPixelSize = brushSize * resolution; //브러???�이�?x ?�상??512 = 브러???��? ?�이�?

           
            Graphics.DrawTexture( 
                new Rect(
                    posX - brushPixelSize * 0.5f, 
                    (renderTexture.height - posY) - brushPixelSize * 0.5f,
                   

                    brushPixelSize, //그릴 브러???�스처의 width.
                    brushPixelSize //그릴 브러???�스처의 height.
                ),
                brushTexture //?�정??브러???�스처로 그림.
            );

            GL.PopMatrix();              // 매트�?�� 복구
            RenderTexture.active = null; // ?�성 ?�더 ?�스�??�제. (?�더링한 그림 ???��???)
        }

       
        #endregion
        [PunRPC]
        public TexturePaintTarget DeepCopy() //깊�? 복사.
        {
            TexturePaintTarget newCopy = new TexturePaintTarget();
            newCopy.renderTexture = renderTexture;
            return newCopy;
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

    }

}
