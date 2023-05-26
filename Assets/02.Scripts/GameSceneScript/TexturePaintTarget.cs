
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
     * - ë°˜ë“œ??Rito/PaintTexture ë§ˆí…Œë¦¬ì–¼ ?¬ìš©
     * - ë§ˆí…Œë¦¬ì–¼??Enable GPU Instancing ì²´í¬
     * 
     */

    /// <summary> 
    /// ê·¸ë¦¼ ê·¸ë ¤ì§??€??
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
                    //_clearTex.SetPixel(0, 0, Color.clear); //?¬ëª…?‰ìœ¼ë¡?ì´ˆê¸°??
                    _clearTex.SetPixel(0, 0, Color.white); //?˜ì??‰ìœ¼ë¡?ì´ˆê¸°??
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
            TryGetComponent(out _mr); //ë©”ì‰¬ ?Œë”?¬ì˜ ì»´í¬?ŒíŠ¸ë¥?ì°¸ì¡°??(ì´ˆê¸°??
        }

        /// <summary> ?Œë” ?ìŠ¤ì³?ì´ˆê¸°??</summary>
        public void InitRenderTexture()
        {
            renderTexture = new RenderTexture(resolution, resolution, 32);// 512x512 pixel???Œë” ?ìŠ¤ì²˜ë? ?ì„±.


            _clearTex = new Texture2D(1, 1);
            //_clearTex.SetPixel(0, 0, Color.clear); //?¬ëª…?‰ìœ¼ë¡?ì´ˆê¸°??
            _clearTex.SetPixel(0, 0, Color.white); //?˜ì??‰ìœ¼ë¡?ì´ˆê¸°??
            _clearTex.Apply();


            _textureBlock = new MaterialPropertyBlock();


            RenderTexture.active = renderTexture;
            Graphics.Blit(_clearTex, renderTexture); //?Œë”?ìŠ¤ì²˜ì˜ ?‰ì„ ì´ˆê¸°?”í•¨.
            RenderTexture.active = null;

            Debug.Log("?Œë”?ìŠ¤ì²???ì´ˆê¸°???±ê³µ");
            // ë§ˆí…Œë¦¬ì–¼ ?„ë¡œ?¼í‹° ë¸”ë¡ ?´ìš©?˜ì—¬ ë°°ì¹­ ? ì??˜ê³ 
            // ë§ˆí…Œë¦¬ì–¼???„ë¡œ?¼í‹°???Œë” ?ìŠ¤ì³??£ì–´ì£¼ê¸°
            _textureBlock.SetTexture(PaintTexPropertyName, renderTexture);
            _mr.SetPropertyBlock(_textureBlock);
            //_mr.SetPropertyBlock(TextureBlock);


            //ë¯¸ë¦¼
            _textureBlock.SetTexture(MainTexPropertyName, renderTexture);
            _mr.SetPropertyBlock(_textureBlock);
            //_mr.SetPropertyBlock(TextureBlock);

            _textureBlock.SetTexture(ColorPropertyName, renderTexture);
            _mr.SetPropertyBlock(_textureBlock);
            Debug.Log("?Œë”?ìŠ¤ì²??„ë¡œ?¼í‹° ì´ˆê¸°???±ê³µ");


        }

        #endregion
        /***********************************************************************
        *                               Public Methods
        ***********************************************************************/
        #region .
        /// <summary> ?Œë” ?ìŠ¤ì³ì— ë¸ŒëŸ¬???ìŠ¤ì³ë¡œ ê·¸ë¦¬ê¸?</summary>
        [PunRPC]
        public void DrawTexture(float posX, float posY, float brushSize, Texture2D brushTexture)
        {
            RenderTexture.active = renderTexture; // ?˜ì¸?…ì„ ?„í•´ ?œì„± ?Œë” ?ìŠ¤ì³??„ì‹œ ? ë‹¹. (?Œë”ë§????ìŠ¤ì²˜ë? ?¤ì •?œë‹¤.) 
            GL.PushMatrix();                      // ë§¤íŠ¸ë¦?Š¤ ?€??
            GL.LoadPixelMatrix(0, resolution, resolution, 0); // ?Œë§?€ ?¬ê¸°ë¡??½ì? ë§¤íŠ¸ë¦?Š¤ ?¤ì •

            float brushPixelSize = brushSize * resolution; //ë¸ŒëŸ¬???¬ì´ì¦?x ?´ìƒ??512 = ë¸ŒëŸ¬???½ì? ?¬ì´ì¦?

           
            Graphics.DrawTexture( 
                new Rect(
                    posX - brushPixelSize * 0.5f, 
                    (renderTexture.height - posY) - brushPixelSize * 0.5f,
                   

                    brushPixelSize, //ê·¸ë¦´ ë¸ŒëŸ¬???ìŠ¤ì²˜ì˜ width.
                    brushPixelSize //ê·¸ë¦´ ë¸ŒëŸ¬???ìŠ¤ì²˜ì˜ height.
                ),
                brushTexture //?¤ì •??ë¸ŒëŸ¬???ìŠ¤ì²˜ë¡œ ê·¸ë¦¼.
            );

            GL.PopMatrix();              // ë§¤íŠ¸ë¦?Š¤ ë³µêµ¬
            RenderTexture.active = null; // ?œì„± ?Œë” ?ìŠ¤ì³??´ì œ. (?Œë”ë§í•œ ê·¸ë¦¼ ???˜í???)
        }

       
        #endregion
        [PunRPC]
        public TexturePaintTarget DeepCopy() //ê¹Šì? ë³µì‚¬.
        {
            TexturePaintTarget newCopy = new TexturePaintTarget();
            newCopy.renderTexture = renderTexture;
            return newCopy;
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

    }

}
