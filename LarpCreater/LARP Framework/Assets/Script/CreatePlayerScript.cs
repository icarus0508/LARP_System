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



    // Define a file extension
    public string[] FileExtensions;

    private Vector2 StartMouseTouchPosition = new Vector2();
    private Vector2 CurrentMouseTouchPosition = new Vector2();
    private Vector2 FetchMouseMouchPostion = new Vector2();
    private bool MoushTouchWorkingFlg = false;
    private float ScalellValue = 1.0f;
    private bool ImageCanBeScaleAndMove = false;

    // Start is called before the first frame update
    void Start()
    {
        if (image_GO)
        {
            playerImage = image_GO.GetComponent<Image>();

        }
        if (playerIn_GO)
        {
            playerInfo = playerIn_GO.GetComponent<Player_Info>();
        }

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
                Touch touch = Input.GetTouch(0);

                // Move the cube if the screen has the finger moving.
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 pos = touch.position;
                    pos.x = (pos.x - 500) / 500;
                    pos.y = (pos.y - 800) / 800;

                    Vector3 tPosition = new Vector3(pos.x, pos.y,0.0f);
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

                if (Input.touchCount == 2)
                {
                    touch = Input.GetTouch(1);

                    if (touch.phase == TouchPhase.Began)
                    {
                        // Halve the size of the cube.
                        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        // Restore the regular size of the cube.
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                }
            }
        }

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

        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).NextPage();
    }

 

    public void OnEndofType(Text playerName)
    {
        playerInfo.Name = playerName.text;
    }

    private void OnEnable()
    {
        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).ForceSetNextPageBtnActive(false);
    }

}
