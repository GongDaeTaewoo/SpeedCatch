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
        texturepaintbrush.PaintTexture.SetPixel(0, 0, texturepaintbrush.col2); //�Ͼ������ �ʱ�ȭ.
        texturepaintbrush.PaintTexture.Apply();
        pv.RPC("updateInitPaint", RpcTarget.Others);
    }    
    [PunRPC]
    public void updateInitPaint()
    {
        texturepaintbrush.col2 = Color.white;
        texturepaintbrush.PaintTexture.SetPixel(0, 0, texturepaintbrush.col2); //�Ͼ������ �ʱ�ȭ.
        texturepaintbrush.PaintTexture.Apply();
    }
    //����Ʈ
    public void Paint()
    {
        texturepaintbrush.PaintTexture.SetPixel(0, 0, texturepaintbrush.col2); //�Ͼ������ �ʱ�ȭ.
        texturepaintbrush.PaintTexture.Apply();

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)//�÷��̾�1�̸� ù��°���� 2�� �ι�°��������
        
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
        texturepaintbrush.PaintTexture.SetPixel(0, 0, texturepaintbrush.col2); //�Ͼ������ �ʱ�ȭ.
        texturepaintbrush.PaintTexture.Apply();
        if (who == 1)
        {
            RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
            Graphics.Blit(texturepaintbrush.PaintTexture, texturepaintbrush.paintTarget.renderTexture);
            RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
        }
        if (who == 2)
        {
            RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
            Graphics.Blit(texturepaintbrush.PaintTexture, texturepaintbrush.paintTarget2.renderTexture);
            RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
        }
        if (who == 3)
        {
            RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
            Graphics.Blit(texturepaintbrush.PaintTexture, texturepaintbrush.paintTarget3.renderTexture);
            RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
        }
        if (who == 4)
        {
            RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
            Graphics.Blit(texturepaintbrush.PaintTexture, texturepaintbrush.paintTarget4.renderTexture);
            RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
        }


        if (who == 1)//�÷��̾� 1�� ������ rpc�Լ��� 1�� ���� 2 �� �ι�°���ÿ�����
            texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);
        if (who == 2)
            texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);
        if (who == 3)
            texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);
        if (who == 4)
            texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.PaintTexture);
    }

    //�ڷΰ���
    public void Undo()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //������ ������� ���� ������
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("�ڷΰ��� ���� �����");
                    return;

                }

                // �������� ���� Ȱ�� ���� �ؽ��� �ӽ� �Ҵ�. (������ �� �ؽ�ó�� �����Ѵ�.) 
                RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
                Debug.Log("�ڷΰ��� ����");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo ���ÿ��� ���� ���� redo �迭�� ����
                //save_line_texture_undo�迭���� pop���� ���� �ؽ�ó�� save_line_texture_redo�迭�� push�� ����.


                pv.RPC("updateUndo", RpcTarget.Others, 1);
            }

        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //������ ������� ���� ������
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget2.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("�ڷΰ��� ���� �����");
                    return;

                }

                // �������� ���� Ȱ�� ���� �ؽ��� �ӽ� �Ҵ�. (������ �� �ؽ�ó�� �����Ѵ�.) 
                RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget2.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
                Debug.Log("�ڷΰ��� ����");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo ���ÿ��� ���� ���� redo �迭�� ����
                //save_line_texture_undo�迭���� pop���� ���� �ؽ�ó�� save_line_texture_redo�迭�� push�� ����.


                pv.RPC("updateUndo", RpcTarget.Others, 2);
            }

        }
        if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //������ ������� ���� ������
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget3.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("�ڷΰ��� ���� �����");
                    return;

                }

                // �������� ���� Ȱ�� ���� �ؽ��� �ӽ� �Ҵ�. (������ �� �ؽ�ó�� �����Ѵ�.) 
                RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget3.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
                Debug.Log("�ڷΰ��� ����");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo ���ÿ��� ���� ���� redo �迭�� ����
                //save_line_texture_undo�迭���� pop���� ���� �ؽ�ó�� save_line_texture_redo�迭�� push�� ����.


                pv.RPC("updateUndo", RpcTarget.Others, 3);
            }
            if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
            {
                if (texturepaintbrush.save_line_texture_undo.Count != 0) //������ ������� ���� ������
                {

                    texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                    texturepaintbrush.save_line_texture_undo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                    try
                    {
                        texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                    }
                    catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                    {
                        RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                        Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget4.renderTexture);

                        RenderTexture.active = null;
                        texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                        Debug.Log("�ڷΰ��� ���� �����");
                        return;

                    }

                    // �������� ���� Ȱ�� ���� �ؽ��� �ӽ� �Ҵ�. (������ �� �ؽ�ó�� �����Ѵ�.) 
                    RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                    Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget4.renderTexture);

                    RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
                    Debug.Log("�ڷΰ��� ����");

                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                    //undo ���ÿ��� ���� ���� redo �迭�� ����
                    //save_line_texture_undo�迭���� pop���� ���� �ؽ�ó�� save_line_texture_redo�迭�� push�� ����.


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
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //������ ������� ���� ������
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("�ڷΰ��� ���� �����");
                    return;

                }

                // �������� ���� Ȱ�� ���� �ؽ��� �ӽ� �Ҵ�. (������ �� �ؽ�ó�� �����Ѵ�.) 
                RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
                Debug.Log("�ڷΰ��� ����");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo ���ÿ��� ���� ���� redo �迭�� ����
                //save_line_texture_undo�迭���� pop���� ���� �ؽ�ó�� save_line_texture_redo�迭�� push�� ����.



            }

        }

        if (who == 2)
        {
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //������ ������� ���� ������
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget2.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("�ڷΰ��� ���� �����");
                    return;

                }

                // �������� ���� Ȱ�� ���� �ؽ��� �ӽ� �Ҵ�. (������ �� �ؽ�ó�� �����Ѵ�.) 
                RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget2.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
                Debug.Log("�ڷΰ��� ����");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo ���ÿ��� ���� ���� redo �迭�� ����
                //save_line_texture_undo�迭���� pop���� ���� �ؽ�ó�� save_line_texture_redo�迭�� push�� ����.



            }

        }
        if (who == 3)
        {
            if (texturepaintbrush.save_line_texture_undo.Count != 0) //������ ������� ���� ������
            {

                texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                texturepaintbrush.save_line_texture_undo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                try
                {
                    texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {
                    RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                    Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget3.renderTexture);

                    RenderTexture.active = null;
                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                    Debug.Log("�ڷΰ��� ���� �����");
                    return;

                }

                // �������� ���� Ȱ�� ���� �ؽ��� �ӽ� �Ҵ�. (������ �� �ؽ�ó�� �����Ѵ�.) 
                RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget3.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
                Debug.Log("�ڷΰ��� ����");

                texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                //undo ���ÿ��� ���� ���� redo �迭�� ����
                //save_line_texture_undo�迭���� pop���� ���� �ؽ�ó�� save_line_texture_redo�迭�� push�� ����.



            }
            if (who == 4)
            {
                if (texturepaintbrush.save_line_texture_undo.Count != 0) //������ ������� ���� ������
                {

                    texturepaintbrush.undopop = texturepaintbrush.save_line_texture_undo.Peek();
                    texturepaintbrush.save_line_texture_undo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                    try
                    {
                        texturepaintbrush.init = texturepaintbrush.save_line_texture_undo.Peek();
                    }
                    catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                    {
                        RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                        Graphics.Blit(texturepaintbrush.WhiteTexture, texturepaintbrush.paintTarget4.renderTexture);

                        RenderTexture.active = null;
                        texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);
                        Debug.Log("�ڷΰ��� ���� �����");
                        return;

                    }

                    // �������� ���� Ȱ�� ���� �ؽ��� �ӽ� �Ҵ�. (������ �� �ؽ�ó�� �����Ѵ�.) 
                    RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                    Graphics.Blit(texturepaintbrush.init, texturepaintbrush.paintTarget4.renderTexture);

                    RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)
                    Debug.Log("�ڷΰ��� ����");

                    texturepaintbrush.save_line_texture_redo.Push(texturepaintbrush.undopop);

                    //undo ���ÿ��� ���� ���� redo �迭�� ����
                    //save_line_texture_undo�迭���� pop���� ���� �ؽ�ó�� save_line_texture_redo�迭�� push�� ����.



                }

            }
        }
    }
    //������ ����
    public void Redo()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //������ ������� ���� ������
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                                                                   //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {

                    Debug.Log("�����ΰ��� ���� �����");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("�����ΰ��� ����");
                pv.RPC("updateRedo", RpcTarget.Others, 1);
            }
        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //������ ������� ���� ������
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {

                    Debug.Log("�����ΰ��� ���� �����");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget2.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("�����ΰ��� ����");
                pv.RPC("updateRedo", RpcTarget.Others, 2);
            }
        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //������ ������� ���� ������
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {

                    Debug.Log("�����ΰ��� ���� �����");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget3.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("�����ΰ��� ����");
                pv.RPC("updateRedo", RpcTarget.Others, 3);
            }
        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //������ ������� ���� ������
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {

                    Debug.Log("�����ΰ��� ���� �����");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget4.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("�����ΰ��� ����");
                pv.RPC("updateRedo", RpcTarget.Others, 4);
            }
        }
    }
    [PunRPC]
    public void updateRedo(int who)
    {
        if (who == 1)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //������ ������� ���� ������
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                                                                   //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {

                    Debug.Log("�����ΰ��� ���� �����");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("�����ΰ��� ����");

            }
        }

        if (who == 2)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //������ ������� ���� ������
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {

                    Debug.Log("�����ΰ��� ���� �����");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget2.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget2.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("�����ΰ��� ����");

            }
        }

        if (who == 3)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //������ ������� ���� ������
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {

                    Debug.Log("�����ΰ��� ���� �����");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget3.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget3.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("�����ΰ��� ����");
            }
        }

        if (who == 4)
        {
            if (texturepaintbrush.save_line_texture_redo.Count != 0) //������ ������� ���� ������
            {
                texturepaintbrush.redopop = texturepaintbrush.save_line_texture_redo.Peek();

                try
                {
                    texturepaintbrush.save_line_texture_redo.Pop();//pop->���ÿ��� �����ϰ� ��ȯ.
                                                                    //reinit = save_line_texture_redo.Peek();
                }
                catch (Exception e)//������ ����ٸ� ����ġ���� �Ͼ�� �������.
                {

                    Debug.Log("�����ΰ��� ���� �����");
                    return;
                }

                RenderTexture.active = texturepaintbrush.paintTarget4.renderTexture;
                Graphics.Blit(texturepaintbrush.redopop, texturepaintbrush.paintTarget4.renderTexture);

                RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����. (�������� �׸� �� ��Ÿ��.)

                texturepaintbrush.save_line_texture_undo.Push(texturepaintbrush.redopop);
                Debug.Log("�����ΰ��� ����");
            }
        }
    }
}
