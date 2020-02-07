using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

// Include these namespaces to use BinaryFormatter
using System.Runtime.Serialization.Formatters.Binary;

using UnityEditor;

public class CreatePlayerScript : BasePageScript
{
    public GameObject playerIn_GO;
    private Player_Info playerInfo;

    public GameObject image_GO;
    private Image playerImage;

    public GameObject CameraCaputre_Go;

    public GameObject FinalImg_GO;

    public GameObject fileBrowserPrefab;

    public GameObject TargetImage_GO;

    public GameObject PlayerDisplayImgGO;

    public GameObject FinalCheckBtnGO;


    // Define a file extension
    public string[] FileExtensions;

    private Vector2 StartMouseTouchPosition = new Vector2();
    private Vector2 CurrentMouseTouchPosition = new Vector2();
    private Vector2 FetchMouseMouchPostion = new Vector2();
    private bool MoushTouchWorkingFlg = false;
    private float ScalellValue = 1.0f;
    private bool ImageCanBeScaleAndMove = false;

    private float initialFingersDistance ;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(ImageCanBeScaleAndMove)
        {
            if (InMoustTouchControllZone(Input.mousePosition))
            {
                FetchMouseMouchPostion.x = Input.GetAxis("Mouse X");
                FetchMouseMouchPostion.y = Input.GetAxis("Mouse Y");
                if (Input.GetMouseButtonDown(0))
                {
                    OnTouchMouseDown();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    OnTouchMouseUp();
                }
                if (HasMouseMoved())
                {
                    OnTouchMouseMove();
                }

                OnScrolling();
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    OnTouchMouseUp();
                }
            }

            if(Input.touchCount>0)
            {
                Touch judageTouch = Input.GetTouch(0);
                Vector3 tPosForJudage = new Vector3(judageTouch.position.x, judageTouch.position.y, 0.0f);
                if (InMoustTouchControllZone(tPosForJudage))
                {
                    Touch touch = Input.GetTouch(0);

                    Vector2 pos = touch.position;
                    FetchMouseMouchPostion.x = pos.x;
                    FetchMouseMouchPostion.y = pos.y;
                    if (Input.touchCount == 1)
                    {
                        if(touch.phase == TouchPhase.Began)
                        {
                            OnTouchMouseDown();
                        }
                        if(touch.phase == TouchPhase.Ended)
                        {
                            OnTouchMouseUp();
                        }
                        if (touch.phase == TouchPhase.Moved)
                        {
                            OnTouchMouseMove();
                        }

                    }
                    else
                    {
                        if (touch.phase == TouchPhase.Ended)
                        {
                            OnTouchMouseUp();
                        }
                    }

                    if (Input.touchCount == 2)
                    {

                        //First set the initial distance between fingers so you can compare.
                        if (touch.phase == TouchPhase.Began)
                        {
                            initialFingersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                        }
                        else
                        {
                            float currentFingersDistance  = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

                            float scaleFactor  = currentFingersDistance / initialFingersDistance;


                            if (scaleFactor >= 1)
                            {
                                ScalellValue += 0.01f;
                                if (ScalellValue > 3.0f)
                                {
                                    ScalellValue = 3.0f;
                                }
                            }

                            if (scaleFactor < 1)
                            {
                                ScalellValue -= 0.01f;
                                if (ScalellValue < 0.2f)
                                {
                                    ScalellValue = 0.2f;
                                }
                            }

                            TargetImage_GO.transform.localScale = new Vector3(ScalellValue, ScalellValue, ScalellValue);
                        }
                    }
                }
 
            }

        }

        if(IsReadyToNextPage())
        {
            FinalCheckBtnGO.SetActive(true);
        }
        else
        {
            FinalCheckBtnGO.SetActive(false);
        }
    }

    private bool IsReadyToNextPage()
    {
        if (playerInfo.Name =="")
        {
            return false;
        }
        return true;
        
    }
    private bool HasMouseMoved()
    {
        //I feel dirty even doing this 
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }

    public void OnClienCapturePhotoForCamera()
    {
        CameraCaputre_Go.SetActive(true);

    }

    public void OnClickCapture()
    {

        TargetImage_GO.GetComponent<Image>().sprite = CameraCaputre_Go.GetComponent<WebCamera>().capturedPic;
        CameraCaputre_Go.SetActive(false);


        TargetImage_GO.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, TargetImage_GO.GetComponent<Image>().sprite.texture.width*2);
        TargetImage_GO.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, TargetImage_GO.GetComponent<Image>().sprite.texture.height*2);
        ImageCanBeScaleAndMove = true;
    }

    public void CancelCapturePhoto()
    {
        CameraCaputre_Go.SetActive(false);
    }


    private void OpenFileBrowser()
    {
        GameObject fileBrowserObject = Instantiate(fileBrowserPrefab, transform);
        fileBrowserObject.name = "FileBrowser";
        GracesGames.SimpleFileBrowser.Scripts.FileBrowser fileBrowserScript =
            fileBrowserObject.GetComponent<GracesGames.SimpleFileBrowser.Scripts.FileBrowser>();

        fileBrowserScript.SetupFileBrowser(GracesGames.SimpleFileBrowser.Scripts.ViewMode.Portrait);

        fileBrowserScript.OpenFilePanel(FileExtensions);
        fileBrowserScript.OnFileSelect += LoadFileUsingPath;

    }
    private void LoadFileUsingPath(string path)
    {
        ImageCanBeScaleAndMove = true;
        Image tImgGO = TargetImage_GO.GetComponent<Image>();
        Texture2D tImg = CommonFunction.LoadPNG(path);
        tImgGO.sprite = Sprite.Create(tImg, new Rect(0, 0, tImg.width, tImg.height), new Vector2(0, 0));
        TargetImage_GO.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tImg.width);
        TargetImage_GO.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tImg.height);

    }
    public void OnLoadPicture()
    {
        OpenFileBrowser();
    }


    private void OnGUI()
    {

    }


    private bool InMoustTouchControllZone(Vector3 JudgePos)
    {
        if(JudgePos.x > 80 && JudgePos.x < 423)
        {
            if(JudgePos.y > 345 && JudgePos.y < 764)
            {
                return true;
            }
        }
        return false;
    }
    private void OnTouchMouseDown()
    {
        MoushTouchWorkingFlg = true;

        StartMouseTouchPosition.x = FetchMouseMouchPostion.x;
        StartMouseTouchPosition.y = FetchMouseMouchPostion.y;
    }
    private void  OnTouchMouseUp()
    {
        MoushTouchWorkingFlg = false;
        StartMouseTouchPosition.x = 0;
        StartMouseTouchPosition.y = 0;
        CurrentMouseTouchPosition.x = 0;
        CurrentMouseTouchPosition.y = 0;
    }

    private void  OnTouchMouseMove()
    {
        if(MoushTouchWorkingFlg)
        {
            CurrentMouseTouchPosition.x = FetchMouseMouchPostion.x;
            CurrentMouseTouchPosition.y = FetchMouseMouchPostion.y;

            Vector3 tPosition = new Vector3(TargetImage_GO.transform.position.x + (CurrentMouseTouchPosition.x - StartMouseTouchPosition.x),
                                            TargetImage_GO.transform.position.y + (CurrentMouseTouchPosition.y - StartMouseTouchPosition.y),0);

            if (tPosition.x < 358)
                tPosition.x = 358;
            if (tPosition.x > 365)
                tPosition.x = 365;
            if (tPosition.y < 382)
                tPosition.y = 382;
            if (tPosition.y > 391)
                tPosition.y = 391;


            TargetImage_GO.transform.SetPositionAndRotation(tPosition, Quaternion.identity);

        }
    }

    private void OnScrolling()
    {

        if (Input.mouseScrollDelta.y > 0)
        {
            ScalellValue += 0.1f;
            if(ScalellValue > 3.0f)
            {
                ScalellValue = 3.0f;
            }
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            ScalellValue -= 0.1f;
            if(ScalellValue < 0.2f)
            {
                ScalellValue = 0.2f;
            }
        }
        TargetImage_GO.transform.localScale = new Vector3(ScalellValue, ScalellValue, ScalellValue);
    }
    public void OnClickConfirmBtn()
    {

        Texture2D tTex = CommonFunction.TextureToTexture2D(PlayerDisplayImgGO.GetComponent<RawImage>().texture);
        playerInfo.PlayerPhoto = Sprite.Create(tTex, new Rect(0, 0, tTex.width, tTex.height), new Vector2(0, 0));
        playerInfo.PlayerPhotoOri = TargetImage_GO.GetComponent<Image>().sprite;
        playerInfo.PlayerImgScalellValue = TargetImage_GO.transform.localScale;
        playerInfo.PlayerImgPosition = TargetImage_GO.transform.position;
        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).NextPage();
    }

 

    public void OnEndofType(Text playerName)
    {
        playerInfo.Name = playerName.text;
    }

    private void OnEnable()
    {
        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).ForceSetNextPageBtnActive(false);
        FinalCheckBtnGO.SetActive(false);


        if (playerImage==null)
        {
            playerImage = image_GO.GetComponent<Image>();

        }
        if (playerInfo==null)
        {
            playerInfo = playerIn_GO.GetComponent<Player_Info>();
        }

        if(playerInfo.Photo != "")
        {
            Image tImgGO = TargetImage_GO.GetComponent<Image>();
            Texture2D tImg = CommonFunction.LoadPNG(Application.dataPath + playerInfo.Photo);
            tImgGO.sprite = Sprite.Create(tImg, new Rect(0, 0, tImg.width, tImg.height), new Vector2(0, 0));
            //TargetImage_GO.transform.localScale = playerInfo.PlayerImgScalellValue;
            //Vector3 tPosition = new Vector3(TargetImage_GO.transform.localPosition.x,
            //                    TargetImage_GO.transform.localPosition.y, 0);
            //TargetImage_GO.transform.localPosition = tPosition;
            RectTransform rt = TargetImage_GO.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(tImg.width, tImg.height);
            ScalellValue = playerInfo.PlayerImgScalellValue.x;
            TargetImage_GO.transform.localScale = new Vector3(ScalellValue, ScalellValue, ScalellValue);

            ImageCanBeScaleAndMove = true;
        }

    }

}
