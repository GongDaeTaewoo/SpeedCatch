# SpeedCatch


### 실행화면
![555](https://github.com/GongDaeTaewoo/SpeedCatch/assets/89184708/543fff44-4389-48fa-bfc1-f17bfc78ac50)

# 1.프로젝트 설명
오큘러스퀘스트2를 사용하는 전제하에 플레이어들이 별도의 VR기기의 컨트롤러 없이 손으로 그림을 그리고 정답을 맞추는 게임, 캐치마인드의 VR형태
## 1-2프로젝트 기간
2022/3/9~2022/6/14
# 2.기술스택
- C#
- Unity
- PUN2(Photon Unity Networking)
- Firebase

# 3.내가 담당한 기능
Photon Unity Network 와 Firebase를 활용한 음성채팅과
멀티플레이어 구현

# 4. 영상
https://www.youtube.com/watch?v=w0Q684dsij4

# 5. 코드의 일부(그림불러오기)
```
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
```

# 6.아쉬웠던점
## 6-1. 깃허브의 부재
당시 깃허브를 아무도 사용하지 않아서 협업에 어려움이 있었다. 
프로젝트가 진행될때마다 프로젝트의 버젼이 달라져서 코드를 다시짜야하는 경우가 있었다. 당시에는 잘 몰랐는데 깃허브의 부재가 문제였던것같다.
## 6-2. 오류
프로젝트중 수 많은 오류와 문제가 있었는데 그것을 마지막까지 전부 잡진 못하여서 스케치북이 검은색으로 나오는 오류가 남아있었던것이 아쉽다.
## 6-3. 부족한 실력
결과를 어떤 방식으로든 돌아가게 했을지라도 그 방식들은 체계적이거나 능숙하지 않고 많이 부족했다.

# 7.좋았던점
## 7-1. 문제극복
처음에는 다뤄보지 못했던 유니티와 포톤을 사용해서 프로젝트를 어떻게 마무리할수 있을까 걱정이 많았는데 시간이 조금 걸려도 여러 문제를 극복하고 결과를 냈다는것이 좋았다.
## 7-2. 팀원들과의 유대감
누군가와 같이 협업하는 일이 쉬운일이 아니지만 프로젝트가 진행되는 내내 
웃으면서 화기애애하게 진행되어서 팀원들에게도 감사를 표한다.
# 8.같은 팀원들
한성대학교
1871299 강미림
1636008 문신우
1971286 김태우
1871293 허진영
