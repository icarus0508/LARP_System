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

    private Texture2D sampleTexture;
    public Texture2D maskTexture;


    public GameObject TargetImage_GO;

    public GameObject PlayerDisplayImgGO;

    // Define a file extension
    public string[] FileExtensions;

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

        sampleTexture = new Texture2D(maskTexture.width, maskTexture.height);


    
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void SelectedImgRect()
    {
      //  playerImage.sprite.texture.ReadPixels(new Rect(300, 0, 500, 799),0,0);
       // FinalImg_GO.GetComponent<Image>().sprite.texture = playerImage.sprite.texture;
       // FinalImg_GO.SetActive(true);

    }

    private void OnGUI()
    {
        //if(GUI.Button(new Rect(10,70,50,30),"Click"))
        //{
        //    //  SelectedImgRect();
        //    CutOutFace();
        //}
    }

    public void CutOutFace()
    {
        
        Texture2D destTexture = 
            new Texture2D(playerImage.sprite.texture.width, playerImage.sprite.texture.height, TextureFormat.ARGB32, false);
        
        Color[] textureData = playerImage.sprite.texture.GetPixels();
        
        destTexture.SetPixels(textureData);
        
        destTexture.Apply();

        destTexture.filterMode = FilterMode.Bilinear;
       

        textureData = destTexture.GetPixels();
        
        sampleTexture.SetPixels(textureData);
       
        sampleTexture.Apply();

       
        Color[] maskPixels = maskTexture.GetPixels();
        
        Color[] curPixels = sampleTexture.GetPixels();

        int index = 0;
        for (int y = 0; y < maskTexture.height; y++)
        {
            for (int x = 0; x < maskTexture.width; x++)
            {
                if (maskPixels[index] != maskPixels[0])
                {
                    curPixels[index] = Color.clear;
                }
                index++;
            }
        }
        sampleTexture.SetPixels(curPixels, 0);
        sampleTexture.Apply(false);

         FinalImg_GO.GetComponent<Image>().sprite=
            Sprite.Create(sampleTexture,
                            new Rect(0, 0,
                            sampleTexture.width, sampleTexture.height), 
                            new Vector2(0, 0));
        FinalImg_GO.SetActive(true);

    }

}
