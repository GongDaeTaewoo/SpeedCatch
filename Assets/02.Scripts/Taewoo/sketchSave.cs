using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;

using Photon.Pun;
using Photon.Realtime;
//�Ҹ�
using System.Threading.Tasks;
using Rito.TexturePainter;

public class sketchSave : MonoBehaviour
//���ڵ�� save�Լ����� ����� Texture2D texture�� �迭�� �ٲ㾲���� �������� �׷����׸��� ����Ʈ�迭�ιٲ�����
{
    public Texture2D Usertexture;
    //�� �̹����� �׸��̳�Ÿ��
    public Image image;
    public int wid =512;
    public int hei=512;
    public RenderTexture rentex;

    // Start is called before the first frame update
    void Start()
    {

        //save();
       // download();

    }

    // Update is called once per frame
    void Update()
    {

    }
    //���̾�̽����� ����������
    public void download()
    {
        //�ʱ�ȭ �ȵǰ� ���۷����� ���Ǹ�ȵǼ� �׽�Ʈ���ʱ�ȭ
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");


        //�ӽ�

        //����1�� �÷��̾� ����1�� ���ù�����
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        
        {//player4/1.jpg������ �ֳĸ� �÷��̾�1�� �÷��̾�4�� �׸��׸��������ϱ�
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player1/1.jpg"); //4/1�ε� ���1/1�ιٲ�
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player1/1.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player2/1.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player3/1.jpg");
        }

        const long maxAllowedSize = 1 * 1024 * 1024;
        reference.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task => {
            if (task.IsFaulted || task.IsCanceled)
            {
                //Debug.LogException(task.Exception);
                // Uh-oh, an error occurred!

            }
            else
            {
                byte[] fileContents = task.Result;
                //������� ���� ����Ʈ�迭�����

                /* Texture2D bmp;
                 bmp = new Texture2D(8, 8);
                 //bmp.LoadRawTextureData(recevBuffer);
                 bmp.LoadImage(fileContents, false);
                 //������� �������ιٲٱ�
                 Sprite sprite = Sprite.Create(bmp, new Rect(0, 0, bmp.width, bmp.height), new Vector2(0.5f, 0.5f));
                 image.sprite = sprite;
                 */

                Texture2D bmp;
                bmp = new Texture2D(wid, hei);
                //bmp.LoadRawTextureData(recevBuffer);
                bmp.LoadImage(fileContents, false);
                //������� �������ιٲٱ�

                //Sprite sprite = Sprite.Create(bmp, new Rect(0, 0, bmp.width, bmp.height), new Vector2(0.5f, 0.5f));
                Texture2D downtexture = new Texture2D(wid, hei, TextureFormat.RGB24, false);
                downtexture.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
                downtexture.Apply();
                Usertexture = downtexture;



            }
        });


    }
    public void download2()
    {

        //�ʱ�ȭ �ȵǰ� ���۷����� ���Ǹ�ȵǼ� �׽�Ʈ���ʱ�ȭ
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com.jpg");
        //����1�� �÷��̾� ����1�� ���ù�����
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {//player1/1.jpg������
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player4/2.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player1/2.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player2/2.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player3/2.jpg");
        }
        const long maxAllowedSize = 1 * 1024 * 1024;
        reference.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task => {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogException(task.Exception);
                // Uh-oh, an error occurred!
                
            }
            else
            {
                byte[] fileContents = task.Result;
                //������� ���� ����Ʈ�迭�����

                Texture2D bmp;
                bmp = new Texture2D(wid, hei);
                //bmp.LoadRawTextureData(recevBuffer);
                bmp.LoadImage(fileContents, false);
                //������� �������ιٲٱ�

                //Sprite sprite = Sprite.Create(bmp, new Rect(0, 0, bmp.width, bmp.height), new Vector2(0.5f, 0.5f));
                Texture2D downtexture = new Texture2D(wid, hei, TextureFormat.RGB24, false);
                downtexture.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
                downtexture.Apply();
                Usertexture = downtexture;

            }
        });

    }
    public void download3()
    {

        //�ʱ�ȭ �ȵǰ� ���۷����� ���Ǹ�ȵǼ� �׽�Ʈ���ʱ�ȭ
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");
        //����1�� �÷��̾� ����1�� ���ù�����
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {//player1/1.jpg������
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player4/3.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player1/3.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player2/3.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player3/3.jpg");
        }
        const long maxAllowedSize = 1 * 1024 * 1024;
        reference.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task => {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogException(task.Exception);
                // Uh-oh, an error occurred!
                
            }
            else
            {
                byte[] fileContents = task.Result;
                //������� ���� ����Ʈ�迭�����

                Texture2D bmp;
                bmp = new Texture2D(wid, hei);
                //bmp.LoadRawTextureData(recevBuffer);
                bmp.LoadImage(fileContents, false);
                //������� �������ιٲٱ�

                //Sprite sprite = Sprite.Create(bmp, new Rect(0, 0, bmp.width, bmp.height), new Vector2(0.5f, 0.5f));
                Texture2D downtexture = new Texture2D(wid, hei, TextureFormat.RGB24, false);
                downtexture.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
                downtexture.Apply();
                Usertexture = downtexture;
            }
        });

    }
    public void download4()
    {

        //�ʱ�ȭ �ȵǰ� ���۷����� ���Ǹ�ȵǼ� �׽�Ʈ���ʱ�ȭ
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");
        //����1�� �÷��̾� ����1�� ���ù�����
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {//player1/1.jpg������
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player4/4.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player1/4.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player2/4.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player3/4.jpg");
        }
        const long maxAllowedSize = 1 * 1024 * 1024;
        reference.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task => {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogException(task.Exception);
                // Uh-oh, an error occurred!
            }
            else
            {
                byte[] fileContents = task.Result;
                //������� ���� ����Ʈ�迭�����

                Texture2D bmp;
                bmp = new Texture2D(wid, hei);
                //bmp.LoadRawTextureData(recevBuffer);
                bmp.LoadImage(fileContents, false);
                //������� �������ιٲٱ�

                //Sprite sprite = Sprite.Create(bmp, new Rect(0, 0, bmp.width, bmp.height), new Vector2(0.5f, 0.5f));
                Texture2D downtexture = new Texture2D(wid, hei, TextureFormat.RGB24, false);
                downtexture.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
                downtexture.Apply();
                Usertexture = downtexture;

            }
        });

    }











    public void save()
    {
        //����Ʈ�迭


        //�������� �����߰�
        rentex = gameObject.GetComponent<TexturePaintTarget>().renderTexture;
        Texture2D texture2d = new Texture2D(wid, hei, TextureFormat.RGB24, false);
        texture2d.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
        texture2d = RenderTextureTo2DTexture(rentex);
        texture2d.Apply();

        byte[] pngBytes = texture2d.EncodeToJPG();
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");

        //���������(�÷��̾� �˾Ƴ��� ����)

       
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player1/1.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player2/1.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player3/1.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player4/1.jpg");
        }

        saveRef.PutBytesAsync(pngBytes).ContinueWith((Task<StorageMetadata> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
            }
            else
            {
                // Metadata contains file metadata such as size, content-type, and md5hash.
                // StorageMetadata metadata = task.Result;
                //string md5Hash = metadata.Md5Hash;
            }
        }
        );
    }
    public void save2()
    {
        //����Ʈ�迭

        rentex = gameObject.GetComponent<TexturePaintTarget>().renderTexture;
        Texture2D texture2d = new Texture2D(wid, hei, TextureFormat.RGB24, false);
        texture2d.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
        texture2d = RenderTextureTo2DTexture(rentex);

        texture2d.Apply();

        byte[] pngBytes = texture2d.EncodeToJPG();
        //�� ����Ʈ�迭�� �׷��� �ؽ�ó2d�� ����Ʈ�迭�� �Ŀ� ��ü�ؾ���  
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");

        //���������(�÷��̾� �˾Ƴ��� ����)

        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player1/2.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player2/2.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player3/2.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player4/2.jpg");
        }

        saveRef.PutBytesAsync(pngBytes).ContinueWith((Task<StorageMetadata> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
            }
            else
            {
                // Metadata contains file metadata such as size, content-type, and md5hash.
                // StorageMetadata metadata = task.Result;
                //string md5Hash = metadata.Md5Hash;
            }
        }
        );
    }
    public void save3()
    {
        //����Ʈ�迭

        rentex = gameObject.GetComponent<TexturePaintTarget>().renderTexture;
        Texture2D texture2d = new Texture2D(wid, hei, TextureFormat.RGB24, false);
        texture2d.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
        texture2d = RenderTextureTo2DTexture(rentex);

        texture2d.Apply();

        byte[] pngBytes = texture2d.EncodeToJPG();
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");

        //���������(�÷��̾� �˾Ƴ��� ����)

        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player1/3.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player2/3.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player3/3.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player4/3.jpg");
        }

        saveRef.PutBytesAsync(pngBytes).ContinueWith((Task<StorageMetadata> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
            }
            else
            {
                // Metadata contains file metadata such as size, content-type, and md5hash.
                // StorageMetadata metadata = task.Result;
                //string md5Hash = metadata.Md5Hash;
            }
        }
        );
    }
    public void save4()
    {
        //����Ʈ�迭
        rentex = gameObject.GetComponent<TexturePaintTarget>().renderTexture;
        Texture2D texture2d = new Texture2D(wid, hei, TextureFormat.RGB24, false);
        texture2d.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
        texture2d = RenderTextureTo2DTexture(rentex);

        texture2d.Apply();

        byte[] pngBytes = texture2d.EncodeToJPG();
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");

        //���������(�÷��̾� �˾Ƴ��� ����)

        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player1/1.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player2/1.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[2].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player3/1.jpg");
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[3].NickName)
        {
            saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player4/1.jpg");
        }

        saveRef.PutBytesAsync(pngBytes).ContinueWith((Task<StorageMetadata> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
            }
            else
            {
                // Metadata contains file metadata such as size, content-type, and md5hash.
                // StorageMetadata metadata = task.Result;
                //string md5Hash = metadata.Md5Hash;
            }
        }
        );
    }

    private Texture2D RenderTextureTo2DTexture(RenderTexture rt)//���� �ؽ�ó�� 2Dtexture�� ����ȯ�ϴ� �Լ�.
    {
        var texture = new Texture2D(rt.width, rt.height, rt.graphicsFormat, 0);
        RenderTexture.active = rt;
        texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture.Apply();

        RenderTexture.active = null;

        return texture;
    }



}
