using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

using UnityEditor;

public class CreatePlayerScript : MonoBehaviour
{
    public GameObject playerIn_GO;
    private Player_Info playerInfo;

    public GameObject image_GO;
    private Image playerImage;

    public GameObject CameraCaputre_Go;

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
}
