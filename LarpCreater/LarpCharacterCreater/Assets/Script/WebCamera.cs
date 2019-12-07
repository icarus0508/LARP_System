using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamera : MonoBehaviour
{

    private bool camAvailable;

    private List<WebCamTexture> CamList = new List<WebCamTexture>();
    private Texture defaultBackground;

    public RawImage backgroud;
    public AspectRatioFitter fit;

    private int CurrentWorkingCameraIndex = 0;

    public Sprite capturedPic = null;
    private void OnEnable()
    {
        defaultBackground = backgroud.texture;
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.Log("No cameara detected");
            camAvailable = false;
            return;
        }

        CamList.Clear();

        CurrentWorkingCameraIndex = 0;

        for (int i = 0; i < devices.Length; i++)
        {

            var tCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            CamList.Add(tCamera);
        }

        CamList[CurrentWorkingCameraIndex].Play();
        backgroud.texture = CamList[CurrentWorkingCameraIndex];

        camAvailable = true;
    }

    private void OnDisable()
    {


        foreach(var i in CamList)
        {
            i.Stop();
        }

        CamList.Clear();

        camAvailable = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!camAvailable)
            return;



        float ratio = (float)CamList[CurrentWorkingCameraIndex].width / (float)CamList[CurrentWorkingCameraIndex].height;
        fit.aspectRatio = ratio;

        float scaleY = CamList[CurrentWorkingCameraIndex].videoVerticallyMirrored ? -1f : 1f;
        backgroud.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -CamList[CurrentWorkingCameraIndex].videoRotationAngle;
        backgroud.rectTransform.localEulerAngles = new Vector3(0, 0, orient);


    }

    public void CapturePhoto()
    {
        Texture2D CapturedTex2D = CommonScript.TextureToTexture2D(backgroud.texture);

        //CapturedTex2D.ReadPixels(new Rect(100, 100, 300, 300), 0, 0);
        //CapturedTex2D.Apply();

        capturedPic = Sprite.Create(CapturedTex2D, new Rect(0, 0, backgroud.texture.width, backgroud.texture.height), new Vector2(0, 0));
    }

    public void SwitchCamera()
    {
        if (!camAvailable)
            return;

        if (CamList[CurrentWorkingCameraIndex].isPlaying)
                 CamList[CurrentWorkingCameraIndex].Stop();

        CurrentWorkingCameraIndex++;
        if(CurrentWorkingCameraIndex>= WebCamTexture.devices.Length)
        {
            CurrentWorkingCameraIndex = 0;
        }

        CamList[CurrentWorkingCameraIndex].Play();
        backgroud.texture = CamList[CurrentWorkingCameraIndex];
    }



}
