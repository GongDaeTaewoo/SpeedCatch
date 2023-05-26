using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;

using Photon.Pun;
using Photon.Realtime;
//불명
using System.Threading.Tasks;
using Rito.TexturePainter;

public class sketchSave : MonoBehaviour
//이코드는 save함수에서 현재는 Texture2D texture을 배열로 바꿔쓰지만 실제에선 그려진그림을 바이트배열로바꿔써야함
{
    public Texture2D Usertexture;
    //이 이미지에 그림이나타남
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
    //파이어베이스에서 사진가져옴
    public void download()
    {
        //초기화 안되고 레퍼런스가 사용되면안되서 테스트로초기화
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");


        //임시

        //앞의1은 플레이어 뒤의1은 제시문순서
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        
        {//player4/1.jpg가져옴 왜냐면 플레이어1은 플레이어4가 그린그림가져오니까
            reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/player1/1.jpg"); //4/1인데 잠깐1/1로바꿈
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
                //여기까지 파일 바이트배열에담기

                /* Texture2D bmp;
                 bmp = new Texture2D(8, 8);
                 //bmp.LoadRawTextureData(recevBuffer);
                 bmp.LoadImage(fileContents, false);
                 //여기까지 사진으로바꾸기
                 Sprite sprite = Sprite.Create(bmp, new Rect(0, 0, bmp.width, bmp.height), new Vector2(0.5f, 0.5f));
                 image.sprite = sprite;
                 */

                Texture2D bmp;
                bmp = new Texture2D(wid, hei);
                //bmp.LoadRawTextureData(recevBuffer);
                bmp.LoadImage(fileContents, false);
                //여기까지 사진으로바꾸기

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

        //초기화 안되고 레퍼런스가 사용되면안되서 테스트로초기화
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com.jpg");
        //앞의1은 플레이어 뒤의1은 제시문순서
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {//player1/1.jpg가져옴
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
                //여기까지 파일 바이트배열에담기

                Texture2D bmp;
                bmp = new Texture2D(wid, hei);
                //bmp.LoadRawTextureData(recevBuffer);
                bmp.LoadImage(fileContents, false);
                //여기까지 사진으로바꾸기

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

        //초기화 안되고 레퍼런스가 사용되면안되서 테스트로초기화
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");
        //앞의1은 플레이어 뒤의1은 제시문순서
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {//player1/1.jpg가져옴
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
                //여기까지 파일 바이트배열에담기

                Texture2D bmp;
                bmp = new Texture2D(wid, hei);
                //bmp.LoadRawTextureData(recevBuffer);
                bmp.LoadImage(fileContents, false);
                //여기까지 사진으로바꾸기

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

        //초기화 안되고 레퍼런스가 사용되면안되서 테스트로초기화
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference reference = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");
        //앞의1은 플레이어 뒤의1은 제시문순서
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {//player1/1.jpg가져옴
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
                //여기까지 파일 바이트배열에담기

                Texture2D bmp;
                bmp = new Texture2D(wid, hei);
                //bmp.LoadRawTextureData(recevBuffer);
                bmp.LoadImage(fileContents, false);
                //여기까지 사진으로바꾸기

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
        //바이트배열


        //오류떄매 구문추가
        rentex = gameObject.GetComponent<TexturePaintTarget>().renderTexture;
        Texture2D texture2d = new Texture2D(wid, hei, TextureFormat.RGB24, false);
        texture2d.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
        texture2d = RenderTextureTo2DTexture(rentex);
        texture2d.Apply();

        byte[] pngBytes = texture2d.EncodeToJPG();
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");

        //참조만들기(플레이어 알아내서 저장)

       
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
        //바이트배열

        rentex = gameObject.GetComponent<TexturePaintTarget>().renderTexture;
        Texture2D texture2d = new Texture2D(wid, hei, TextureFormat.RGB24, false);
        texture2d.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
        texture2d = RenderTextureTo2DTexture(rentex);

        texture2d.Apply();

        byte[] pngBytes = texture2d.EncodeToJPG();
        //이 바이트배열을 그려진 텍스처2d의 바이트배열로 후에 교체해야함  
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");

        //참조만들기(플레이어 알아내서 저장)

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
        //바이트배열

        rentex = gameObject.GetComponent<TexturePaintTarget>().renderTexture;
        Texture2D texture2d = new Texture2D(wid, hei, TextureFormat.RGB24, false);
        texture2d.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
        texture2d = RenderTextureTo2DTexture(rentex);

        texture2d.Apply();

        byte[] pngBytes = texture2d.EncodeToJPG();
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");

        //참조만들기(플레이어 알아내서 저장)

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
        //바이트배열
        rentex = gameObject.GetComponent<TexturePaintTarget>().renderTexture;
        Texture2D texture2d = new Texture2D(wid, hei, TextureFormat.RGB24, false);
        texture2d.ReadPixels(new Rect(0, 0, wid, hei), 0, 0);
        texture2d = RenderTextureTo2DTexture(rentex);

        texture2d.Apply();

        byte[] pngBytes = texture2d.EncodeToJPG();
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference saveRef = storage.GetReferenceFromUrl("gs://test-adf07.appspot.com/test.jpg");

        //참조만들기(플레이어 알아내서 저장)

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

    private Texture2D RenderTextureTo2DTexture(RenderTexture rt)//렌더 텍스처를 2Dtexture로 형변환하는 함수.
    {
        var texture = new Texture2D(rt.width, rt.height, rt.graphicsFormat, 0);
        RenderTexture.active = rt;
        texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture.Apply();

        RenderTexture.active = null;

        return texture;
    }



}
