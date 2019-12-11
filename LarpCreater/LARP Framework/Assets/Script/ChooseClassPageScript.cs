using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ChooseClassPageScript : BasePageScript
{
    public GameObject playerIn_GO;
    private Player_Info playerInfo;

    public GameObject WClass_Btn = null;
    public GameObject AClass_Btn = null;
    public GameObject MClass_Btn = null;

    public Material DefaultMat = null;
    public Material GrayScaleMat = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitialPage()
    {
        WClass_Btn.GetComponent<Image>().material = GrayScaleMat;
        AClass_Btn.GetComponent<Image>().material = GrayScaleMat;
        MClass_Btn.GetComponent<Image>().material = GrayScaleMat;

        if (playerInfo.Clasz == "W")
        {
            WClass_Btn.GetComponent<Image>().material = DefaultMat;
        }
        if (playerInfo.Clasz == "A")
        {
            AClass_Btn.GetComponent<Image>().material = DefaultMat;
        }
        if (playerInfo.Clasz == "M")
        {
            MClass_Btn.GetComponent<Image>().material = DefaultMat;
        }
    }
    private void OnEnable()
    {
        if (!playerInfo)
            if (playerIn_GO)
            {
                playerInfo = playerIn_GO.GetComponent<Player_Info>();
            }

        InitialPage();
    }

    private void OnDisable()
    {

    }


    private void chooseClass(string clasz)
    {
        playerInfo.Clasz = clasz;
    }
    public void OnSelectedWClass()
    {
        chooseClass("W");
        WClass_Btn.GetComponent<Image>().material = DefaultMat;
        AClass_Btn.GetComponent<Image>().material = GrayScaleMat;
        MClass_Btn.GetComponent<Image>().material = GrayScaleMat;
    }
    public void OnSelectedAClass()
    {
        chooseClass("A");
        WClass_Btn.GetComponent<Image>().material = GrayScaleMat;
        AClass_Btn.GetComponent<Image>().material = DefaultMat;
        MClass_Btn.GetComponent<Image>().material = GrayScaleMat;
    }
    public void OnSelectedMClass()
    {
        chooseClass("M");
        WClass_Btn.GetComponent<Image>().material = GrayScaleMat;
        AClass_Btn.GetComponent<Image>().material = GrayScaleMat;
        MClass_Btn.GetComponent<Image>().material = DefaultMat;
    }
}
