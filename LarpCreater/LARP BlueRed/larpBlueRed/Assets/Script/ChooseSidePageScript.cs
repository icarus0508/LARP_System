using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

public class ChooseSidePageScript : BasePageScript
{
    public GameObject playerIn_GO;
    private Player_Info playerInfo;

    public GameObject BI_Btn = null;
    public GameObject AD_Btn = null;
    public GameObject JD_Btn = null;
    public GameObject DN_Btn = null;
    public GameObject NT_Btn = null;

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

        BI_Btn.GetComponent<Image>().material = GrayScaleMat;
        AD_Btn.GetComponent<Image>().material = GrayScaleMat;
        JD_Btn.GetComponent<Image>().material = GrayScaleMat;
        DN_Btn.GetComponent<Image>().material = GrayScaleMat;
        NT_Btn.GetComponent<Image>().material = GrayScaleMat;

        if(playerInfo.Sided =="BI")
        {
            BI_Btn.GetComponent<Image>().material = DefaultMat;
            ChooseBlueIron();
        }

        if (playerInfo.Sided == "AD")
        {
            AD_Btn.GetComponent<Image>().material = DefaultMat;
            ChooseAxeDragon();
        }

        if (playerInfo.Sided == "JD")
        {
            JD_Btn.GetComponent<Image>().material = DefaultMat;
            ChooseJadeDragon();
        }

        if (playerInfo.Sided == "DN")
        {
            DN_Btn.GetComponent<Image>().material = DefaultMat;
            ChooseDeamon();
        }

        if (playerInfo.Sided == "NT")
        {
            NT_Btn.GetComponent<Image>().material = DefaultMat;
            ChooseNoTeam();
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

    private void ChooseSided(string side)
    {
        playerInfo.Sided = side;
    }

    public void ChooseBlueIron()
    {
        ChooseSided("BL");
        BI_Btn.GetComponent<Image>().material = DefaultMat;
        AD_Btn.GetComponent<Image>().material = GrayScaleMat;
        JD_Btn.GetComponent<Image>().material = GrayScaleMat;
        DN_Btn.GetComponent<Image>().material = GrayScaleMat;
        NT_Btn.GetComponent<Image>().material = GrayScaleMat;
    }

    public void ChooseAxeDragon()
    {
        ChooseSided("AD");
        BI_Btn.GetComponent<Image>().material = GrayScaleMat;
        AD_Btn.GetComponent<Image>().material = DefaultMat;
        JD_Btn.GetComponent<Image>().material = GrayScaleMat;
        DN_Btn.GetComponent<Image>().material = GrayScaleMat;
        NT_Btn.GetComponent<Image>().material = GrayScaleMat;
    }

    public void ChooseJadeDragon()
    {
        ChooseSided("JD");
        BI_Btn.GetComponent<Image>().material = GrayScaleMat;
        AD_Btn.GetComponent<Image>().material = GrayScaleMat;
        JD_Btn.GetComponent<Image>().material = DefaultMat;
        DN_Btn.GetComponent<Image>().material = GrayScaleMat;
        NT_Btn.GetComponent<Image>().material = GrayScaleMat;
    }

    public void ChooseDeamon()
    {
        ChooseSided("DN");
        BI_Btn.GetComponent<Image>().material = GrayScaleMat;
        AD_Btn.GetComponent<Image>().material = GrayScaleMat;
        JD_Btn.GetComponent<Image>().material = GrayScaleMat;
        DN_Btn.GetComponent<Image>().material = DefaultMat;
        NT_Btn.GetComponent<Image>().material = GrayScaleMat;
    }

    public void ChooseNoTeam()
    {
        ChooseSided("NT");
        BI_Btn.GetComponent<Image>().material = GrayScaleMat;
        AD_Btn.GetComponent<Image>().material = GrayScaleMat;
        JD_Btn.GetComponent<Image>().material = GrayScaleMat;
        DN_Btn.GetComponent<Image>().material = GrayScaleMat;
        NT_Btn.GetComponent<Image>().material = DefaultMat;
    }

    
}
