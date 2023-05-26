using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.TexturePainter;
using System.IO;
using System;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;




public class MTools : MonoBehaviourPunCallbacks
{

    public TexturePaintBrush texturepaintbrush;

    public PhotonView pv;

    public void Eraser()
    {
        texturepaintbrush.col2 = Color.white;
        texturepaintbrush.SetBrushColor(in texturepaintbrush.col2);
        pv.RPC("updateEraser", RpcTarget.Others);

    }
    [PunRPC]
    public void updateEraser()
    {
        texturepaintbrush.col2 = Color.white;
        texturepaintbrush.SetBrushColor(in texturepaintbrush.col2);
    }


    public void InitPaint()
    {
        texturepaintbrush.col2 = Color.white;      
        texturepaintbrush.PaintTexture.SetPixel(0, 0, texturepaintbrush.col2); //하얀색으로 초기화.
        texturepaintbrush.PaintTexture.Apply();
        pv.RPC("updateInitPaint", RpcTarget.Others);
    }    
    [PunRPC]
    public void updateInitPaint()
    {
        texturepaintbrush.col2 = Color.white;
        texturepaintbrush.PaintTexture.SetPixel(0, 0, texturepaintbrush.col2); //하얀색으로 초기화.
        texturepaintbrush.PaintTexture.Apply();
    }
    //페인트
    public void Paint()
    {
        texturepaintbrush.PaintTexture.SetPixel(0, 0, texturepaintbrush.col2); //하얀색으로 초기화.
        texturepaintbrush.PaintTexture.Apply();

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)//플레이어1이면 첫번째스택 2면 두번째스택저장
        
            texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);
          
        
        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        
            texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);
         
        if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        
        texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);
       
    
        if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
            texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);

        pv.RPC("updatePaint", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber);

    }

    [PunRPC]
    public void updatePaint(int who)
    {
        texturepaintbrush.PaintTexture.SetPixel(0, 0, texturepaintbrush.col2); //하얀색으로 초기화.
        texturepaintbrush.PaintTexture.Apply();
        if (who == 1)
        {
            RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
            Graphics.Blit(texturepaintbrush.PaintTexture, texturepaintbrush.paintTarget.renderTexture);
            RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
        }
        if (who == 2)
        {
            RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
            Graphics.Blit(texturepaintbrush.PaintTexture, texturepaintbrush.paintTarget2.renderTexture);
            RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
        }
        if (who == 3)
        {
            RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
            Graphics.Blit(texturepaintbrush.PaintTexture, texturepaintbrush.paintTarget3.renderTexture);
            RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
        }
        if (who == 4)
        {
            RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
            Graphics.Blit(texturepaintbrush.PaintTexture, texturepaintbrush.paintTarget4.renderTexture);
            RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
        }


        if (who == 1)//플레이어 1이 보내준 rpc함수면 1에 저장 2 면 두번째스택에저장
            texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);
        if (who == 2)
            texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);
        if (who == 3)
            texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);
        if (who == 4)
            texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);
    }

    //뒤로가기
    public void Undo()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //스택이 비어있지 않을 때까지
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->스택에서 제거하고 반환.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("뒤로가기 스택 비었음");
                    return;

                }

                // 페인팅을 위해 활성 렌더 텍스쳐 임시 할당. (렌더링 할 텍스처를 설정한다.) 
                RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
                Debug.Log("뒤로가기 성공");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo 스택에서 빼낸 값을 redo 배열에 저장
                //save_line_texture_undo배열에서 pop으로 없앤 텍스처를 save_line_texture_redo배열에 push로 저장.


                pv.RPC("updateUndo", RpcTarget.Others, 1);
            }

        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //스택이 비어있지 않을 때까지
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->스택에서 제거하고 반환.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget2.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("뒤로가기 스택 비었음");
                    return;

                }

                // 페인팅을 위해 활성 렌더 텍스쳐 임시 할당. (렌더링 할 텍스처를 설정한다.) 
                RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget2.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
                Debug.Log("뒤로가기 성공");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo 스택에서 빼낸 값을 redo 배열에 저장
                //save_line_texture_undo배열에서 pop으로 없앤 텍스처를 save_line_texture_redo배열에 push로 저장.


                pv.RPC("updateUndo", RpcTarget.Others, 2);
            }

        }
        if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //스택이 비어있지 않을 때까지
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->스택에서 제거하고 반환.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget3.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("뒤로가기 스택 비었음");
                    return;

                }

                // 페인팅을 위해 활성 렌더 텍스쳐 임시 할당. (렌더링 할 텍스처를 설정한다.) 
                RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget3.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
                Debug.Log("뒤로가기 성공");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo 스택에서 빼낸 값을 redo 배열에 저장
                //save_line_texture_undo배열에서 pop으로 없앤 텍스처를 save_line_texture_redo배열에 push로 저장.


                pv.RPC("updateUndo", RpcTarget.Others, 3);
            }
            if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
            {
                if (texturepaintbrush.save_line_texture_undo.Count != 0) //스택이 비어있지 않을 때까지
                {

                    texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                    texturepaintbrush.save_line_texture_undo.Pop();//pop->스택에서 제거하고 반환.
                    try
                    {
                        texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                    }
                    catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                    {
                        RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                        Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget4.renderTexture);

                        RenderTexture.active = null;
                        texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                        Debug.Log("뒤로가기 스택 비었음");
                        return;

                    }

                    // 페인팅을 위해 활성 렌더 텍스쳐 임시 할당. (렌더링 할 텍스처를 설정한다.) 
                    RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                    Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget4.renderTexture);

                    RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
                    Debug.Log("뒤로가기 성공");

                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                    //undo 스택에서 빼낸 값을 redo 배열에 저장
                    //save_line_texture_undo배열에서 pop으로 없앤 텍스처를 save_line_texture_redo배열에 push로 저장.


                    pv.RPC("updateUndo", RpcTarget.Others, 4);
                }

            }
        }
    }
    [PunRPC]
    public void updateUndo(int who)
    {
        if (who == 1)
        {
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //스택이 비어있지 않을 때까지
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->스택에서 제거하고 반환.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("뒤로가기 스택 비었음");
                    return;

                }

                // 페인팅을 위해 활성 렌더 텍스쳐 임시 할당. (렌더링 할 텍스처를 설정한다.) 
                RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
                Debug.Log("뒤로가기 성공");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo 스택에서 빼낸 값을 redo 배열에 저장
                //save_line_texture_undo배열에서 pop으로 없앤 텍스처를 save_line_texture_redo배열에 push로 저장.



            }

        }

        if (who == 2)
        {
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //스택이 비어있지 않을 때까지
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->스택에서 제거하고 반환.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget2.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("뒤로가기 스택 비었음");
                    return;

                }

                // 페인팅을 위해 활성 렌더 텍스쳐 임시 할당. (렌더링 할 텍스처를 설정한다.) 
                RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget2.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
                Debug.Log("뒤로가기 성공");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo 스택에서 빼낸 값을 redo 배열에 저장
                //save_line_texture_undo배열에서 pop으로 없앤 텍스처를 save_line_texture_redo배열에 push로 저장.



            }

        }
        if (who == 3)
        {
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //스택이 비어있지 않을 때까지
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->스택에서 제거하고 반환.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget3.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("뒤로가기 스택 비었음");
                    return;

                }

                // 페인팅을 위해 활성 렌더 텍스쳐 임시 할당. (렌더링 할 텍스처를 설정한다.) 
                RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget3.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
                Debug.Log("뒤로가기 성공");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo 스택에서 빼낸 값을 redo 배열에 저장
                //save_line_texture_undo배열에서 pop으로 없앤 텍스처를 save_line_texture_redo배열에 push로 저장.



            }
            if (who == 4)
            {
                if (texturepaintbrush.save_line_texture_undo.Count != 0) //스택이 비어있지 않을 때까지
                {

                    texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                    texturepaintbrush.save_line_texture_undo.Pop();//pop->스택에서 제거하고 반환.
                    try
                    {
                        texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                    }
                    catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                    {
                        RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                        Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget4.renderTexture);

                        RenderTexture.active = null;
                        texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                        Debug.Log("뒤로가기 스택 비었음");
                        return;

                    }

                    // 페인팅을 위해 활성 렌더 텍스쳐 임시 할당. (렌더링 할 텍스처를 설정한다.) 
                    RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                    Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget4.renderTexture);

                    RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)
                    Debug.Log("뒤로가기 성공");

                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                    //undo 스택에서 빼낸 값을 redo 배열에 저장
                    //save_line_texture_undo배열에서 pop으로 없앤 텍스처를 save_line_texture_redo배열에 push로 저장.



                }

            }
        }
    }
    //앞으로 가기
    public void Redo()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //스택이 비어있지 않을 때까지
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->스택에서 제거하고 반환.
                                                                   //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {

                    Debug.Log("앞으로가기 스택 비었음");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("앞으로가기 성공");
                pv.RPC("updateRedo", RpcTarget.Others, 1);
            }
        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //스택이 비어있지 않을 때까지
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->스택에서 제거하고 반환.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {

                    Debug.Log("앞으로가기 스택 비었음");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget2.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("앞으로가기 성공");
                pv.RPC("updateRedo", RpcTarget.Others, 2);
            }
        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //스택이 비어있지 않을 때까지
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->스택에서 제거하고 반환.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {

                    Debug.Log("앞으로가기 스택 비었음");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget3.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("앞으로가기 성공");
                pv.RPC("updateRedo", RpcTarget.Others, 3);
            }
        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //스택이 비어있지 않을 때까지
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->스택에서 제거하고 반환.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {

                    Debug.Log("앞으로가기 스택 비었음");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget4.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("앞으로가기 성공");
                pv.RPC("updateRedo", RpcTarget.Others, 4);
            }
        }
    }
    [PunRPC]
    public void updateRedo(int who)
    {
        if (who == 1)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //스택이 비어있지 않을 때까지
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->스택에서 제거하고 반환.
                                                                   //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {

                    Debug.Log("앞으로가기 스택 비었음");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("앞으로가기 성공");

            }
        }

        if (who == 2)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //스택이 비어있지 않을 때까지
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->스택에서 제거하고 반환.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {

                    Debug.Log("앞으로가기 스택 비었음");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget2.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("앞으로가기 성공");

            }
        }

        if (who == 3)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //스택이 비어있지 않을 때까지
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->스택에서 제거하고 반환.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {

                    Debug.Log("앞으로가기 스택 비었음");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget3.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("앞으로가기 성공");
            }
        }

        if (who == 4)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //스택이 비어있지 않을 때까지
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->스택에서 제거하고 반환.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//스택이 비었다면 스케치북을 하얗게 만들어줌.
                {

                    Debug.Log("앞으로가기 스택 비었음");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget4.renderTexture);

                RenderTexture.active = null; // 활성 렌더 텍스쳐 해제. (렌더링한 그림 이 나타남.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("앞으로가기 성공");
            }
        }
    }
}
