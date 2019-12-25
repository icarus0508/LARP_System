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
    private bool MoushTouchWorkingFlg = false;
    private float ScalellValue = 1.0f;

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

        if(Input.GetMouseButtonDown(0))
        {
            OnTouchMouseDown();
        }
        if(Input.GetMouseButtonUp(0))
        {
            OnTouchMouseUp();
        }
        if(HasMouseMoved())
        {
            OnTouchMouseMove();
        }

        OnScrolling();
    }

    private bool HasMouseMoved()
    {
        //I feel dirty even doing this 
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }

    public void OnClienCapturePhotoForCamera()
    {
        CameraCaputre_Go.SetActive(true);


        for (int i = 0; i < CameraCaputre_Go.transform.childCount; i++)
        {
            CameraCaputre_Go.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void OnClickCapture()
    {
        if (playerImage)
            playerImage.sprite = CameraCaputre_Go.GetComponent<WebCamera>().capturedPic;


        CameraCaputre_Go.SetActive(false);


        for (int i = 0; i < CameraCaputre_Go.transform.childCount; i++)
        {
            CameraCaputre_Go.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void CancelCapturePhoto()
    {
        CameraCaputre_Go.SetActive(false);


        for (int i = 0; i < CameraCaputre_Go.transform.childCount; i++)
        {
            CameraCaputre_Go.transform.GetChild(i).gameObject.SetActive(false);
        }
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

   

    private void OnTouchMouseDown()
    {
        MoushTouchWorkingFlg = true;

        StartMouseTouchPosition.x = Input.GetAxis("Mouse X");
        StartMouseTouchPosition.y = Input.GetAxis("Mouse Y");
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
            CurrentMouseTouchPosition.x = Input.GetAxis("Mouse X");
            CurrentMouseTouchPosition.y = Input.GetAxis("Mouse Y");

            Vector3 tPosition = new Vector3(TargetImage_GO.transform.position.x + (CurrentMouseTouchPosition.x - StartMouseTouchPosition.x),
                                            TargetImage_GO.transform.position.y + (CurrentMouseTouchPosition.y - StartMouseTouchPosition.y),0);
            TargetImage_GO.transform.SetPositionAndRotation(tPosition, Quaternion.identity);

        }
    }

    private void OnScrolling()
    {

        if (Input.mouseScrollDelta.y > 0)
        {
            ScalellValue += 0.1f;
            if(ScalellValue > 5.0f)
            {
                ScalellValue = 5.0f;
            }
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            ScalellValue -= 0.1f;
            if(ScalellValue < 0.1f)
            {
                ScalellValue = 0.1f;
            }
        }
        TargetImage_GO.transform.localScale = new Vector3(ScalellValue, ScalellValue, ScalellValue);
    }
    public void OnClickConfirmBtn()
    {

        Texture2D tTex = CommonFunction.TextureToTexture2D(PlayerDisplayImgGO.GetComponent<RawImage>().texture);
        playerInfo.PlayerPhoto = Sprite.Create(tTex, new Rect(0, 0, tTex.width, tTex.height), new Vector2(0, 0));

    }

 

    public void OnEndofType(Text playerName)
    {
        playerInfo.Name = playerName.text;
    }

}
