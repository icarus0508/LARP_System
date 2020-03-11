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

    public GameObject ClinkBtn_GO = null;
    public GameObject ClickBtnHeavy_GO = null;

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
            OnSelectedWClass();
        }
        if (playerInfo.Clasz == "A")
        {
            AClass_Btn.GetComponent<Image>().material = DefaultMat;
            OnSelectedAClass();
        }
        if (playerInfo.Clasz == "M")
        {
            MClass_Btn.GetComponent<Image>().material = DefaultMat;
            OnSelectedMClass();
        }

        if (playerInfo.withHelmet)
        {
            ClinkBtn_GO.SetActive(true);
            playerInfo.withHelmet = true;
        }
        else
        {
            ClinkBtn_GO.SetActive(false);
            playerInfo.withHelmet = false;
        }
        if (playerInfo.withHeavyEquip)
        {
            ClickBtnHeavy_GO.SetActive(true);
            playerInfo.withHeavyEquip = true;
        }
        else
        {
            ClickBtnHeavy_GO.SetActive(false);
            playerInfo.withHeavyEquip = false;
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

        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).ForceSetNextPageBtnActive(true);
    }

    private void OnDisable()
    {

    }


    private void chooseClass(string clasz)
    {
        playerInfo.Clasz = clasz;

        //if(clasz=="W")
        //{
        //    playerInfo.ArrowCount = 0;
        //    playerInfo.ThrowCount = 0;
        //    playerInfo.MagicCount = 0;
        //}

        //if (clasz == "A")
        //{
        //    playerInfo.ArrowCount = 12;
        //    playerInfo.ThrowCount = 0;
        //    playerInfo.MagicCount = 0;
        //}

        //if (clasz == "M")
        //{
        //    playerInfo.ArrowCount = 0;
        //    playerInfo.ThrowCount = 0;
        //    playerInfo.MagicCount = 10;
        //}

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

    public void OnClickWithHelmet()
    {
        if(playerInfo.withHelmet)
        {
            ClinkBtn_GO.SetActive(false);
            playerInfo.withHelmet = false;
        }
        else
        {
            ClinkBtn_GO.SetActive(true);
            playerInfo.withHelmet = true;
        }
       
    }

    public void OnClickWithHeavyEquip()
    {
        if(playerInfo.withHeavyEquip)
        {
            ClickBtnHeavy_GO.SetActive(false);
            playerInfo.withHeavyEquip = false;
        }
        else
        {
            ClickBtnHeavy_GO.SetActive(true);
            playerInfo.withHeavyEquip = true;
        }
    }
}
