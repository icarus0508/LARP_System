using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

using UnityEditor;


public class PhtoPageScript : MonoBehaviour
{

    public GameObject PlayerInfoGO;
    private PlayerData PlayerInfo;

    public GameObject ImageGO;
    private Image PlayerImage;
    private Texture2D tex;

    public GameObject InputTextGO;
    private InputField inputfield;

    public GameObject CameraCaputreGO;

    // Start is called before the first frame update
    void Start()
    {
        if(ImageGO)
        {
            PlayerImage = ImageGO.GetComponent<Image>();

        }
        if (PlayerInfoGO)
        {
            PlayerInfo = PlayerInfoGO.GetComponent<PlayerData>();
        }
        if(InputTextGO)
        {
            inputfield = InputTextGO.GetComponent<InputField>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    public void LoadImage()
    {
        if(PlayerImage)
        {
            tex = CommonScript.LoadPNG(".\\Assets\\Library\\CareerWarrior.png");

            PlayerImage.sprite =

            Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));

            //if(PlayerInfo)
            //{
            //   // PlayerInfo.PlayerImageName = inputfield.text;
            //    PlayerInfo.PlayerImageSprite = PlayerImage.sprite;
            //}
        }
    }

    public void OnClickDecide()
    {
        if (PlayerInfo)
        {
            PlayerInfo.PlayerName = inputfield.text;
            PlayerInfo.PlayerImageSprite = PlayerImage.sprite;
        }
    }

    public void OnClienCapturePhotoForCamera()
    {
        CameraCaputreGO.SetActive(true);
       

        for(int i=0; i < CameraCaputreGO.transform.childCount; i++)
        {
            CameraCaputreGO.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

 

    public void OnClickCapure()
    {
        if (PlayerImage)
            PlayerImage.sprite = CameraCaputreGO.GetComponent<WebCamera>().capturedPic;


        CameraCaputreGO.SetActive(false);


        for (int i = 0; i < CameraCaputreGO.transform.childCount; i++)
        {
            CameraCaputreGO.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void CancelCapturePhoto()
    {
        CameraCaputreGO.SetActive(false);


        for (int i = 0; i < CameraCaputreGO.transform.childCount; i++)
        {
            CameraCaputreGO.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
