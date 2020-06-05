using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ChooseSkillPageScript : BasePageScript
{
    public GameObject playerIn_GO;
    private Player_Info playerInfo;

    public GameObject WClaszGO;
    public GameObject AClaszGO;
    public GameObject MClaszGO;
    public GameObject VClaszGO;

    public GameObject NormalTitleGO;
    public GameObject SRankTitleGO;

    public GameObject SkillPointLeftGO;

    public GameObject ChangeToSBtnGO;
    public GameObject ChangeBtnToSGO;
    public GameObject ChangeBtnToNormalGO;

    public GameObject DetailPresenterGO;

    public GameObject Prfab_SkillGO = null;
    public GameObject ScrollViewGO = null;
    public GameObject ScrollViewContentGO = null;
    private bool InSRankSkillList = false;

    public GameObject SRankSkillListGo = null;
    public GameObject NormalSkillListGo = null;

    public GameObject DeciedeSkillBtn = null;

    void TestScrollBar()
    {
        int teSizX = -100;
        int teSizY = 20;
        for (int i = 1; i < 10; i++)
        {
            GameObject tGO = Instantiate(Prfab_SkillGO, new Vector2(teSizX, -teSizY), Quaternion.identity);
            tGO.transform.SetParent(ScrollViewContentGO.transform, false);

            teSizX += 200;
            if (i % 2 == 0)
            {
                teSizY += 100;
                teSizX = -100;
            }

        }
        //ScrollViewGO.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
    }
    bool CheckAvliable(int Index)
    {
       
        if(!CommonFunction.IsClassAva(playerInfo.Clasz, Skill_Info_Manager.Skill_List[Index].AvaClass))
        {
            return false;
        }
        if(!CommonFunction.IsRankAve(playerInfo.Rank,Skill_Info_Manager.Skill_List[Index].AvaRank))
        {
            return false;
        }
        if(!CommonFunction.IsSideAve(playerInfo.Sided,Skill_Info_Manager.Skill_List[Index].AvaSide))
        {
            return false;
        }
        if(Skill_Info_Manager.Skill_List[Index].PreSkillIndex!=-1)
        {
            return false;
        }
        return true;
    }
    public void ShowDetailPresent(int indexOfSkill)
    {
        DetailPresenterGO.SetActive(true);
        DetailPresenterGO.transform.Find("detail").GetComponent<Text>().text = Skill_Info_Manager.Skill_List[indexOfSkill].DetailDescription;
        DetailPresenterGO.transform.Find("Title").GetComponent<Text>().text = Skill_Info_Manager.Skill_List[indexOfSkill].Name;
    }
    void InitialSkillList()
    {
        int teSizX = -100;
        int teSizY = 20;
        int counter = 0;
        for (int i=0;i<Skill_Info_Manager.Skill_List.Count;i++)
        {

            if (!CheckAvliable(i)) continue;
            if (Skill_Info_Manager.Skill_List[i].AvaRank == "S") continue;

             GameObject tGO = Instantiate(Prfab_SkillGO, new Vector2(teSizX, -teSizY), Quaternion.identity);
            tGO.transform.SetParent(ScrollViewContentGO.transform, false);
            SkillButtonScript tSBS = tGO.GetComponentInChildren<SkillButtonScript>();
            tSBS.SkillMainImgGO.GetComponent<Image>().sprite = Skill_Info_Manager.Skill_List[i].Image;
            tSBS.SkillNameGO.GetComponent<Text>().text = Skill_Info_Manager.Skill_List[i].Name;
            tSBS.SkillDescription.GetComponent<Text>().text = Skill_Info_Manager.Skill_List[i].Description;
            tSBS.SkillAva.GetComponent<Text>().text = "0";
            tSBS.SkillMax.GetComponent<Text>().text = Skill_Info_Manager.Skill_List[i].MaxCost;
            tSBS.SkillListIndex = i;
            tSBS.playerInfo = playerInfo;
            tSBS.DetailPresentAction = ShowDetailPresent;


            counter++;
            teSizX += 200;
            if (counter % 2 == 0)
            {
                teSizY += 100;
                teSizX = -100;
            }
        }

        MoveSListToDisplay();

         teSizX = -100;
         teSizY = 20;
         counter = 0;
        for (int i =0;i<Skill_Info_Manager.Skill_List.Count;i++)
        {

            if (!CheckAvliable(i)) continue;
            if (Skill_Info_Manager.Skill_List[i].AvaRank != "S") continue;

            GameObject tGO = Instantiate(Prfab_SkillGO, new Vector2(teSizX, -teSizY), Quaternion.identity);
            tGO.transform.SetParent(ScrollViewContentGO.transform, false);
            SkillButtonScript tSBS = tGO.GetComponentInChildren<SkillButtonScript>();
            tSBS.SkillMainImgGO.GetComponent<Image>().sprite = Skill_Info_Manager.Skill_List[i].Image;
            tSBS.SkillNameGO.GetComponent<Text>().text = Skill_Info_Manager.Skill_List[i].Name;
            tSBS.SkillDescription.GetComponent<Text>().text = Skill_Info_Manager.Skill_List[i].Description;
            tSBS.SkillAva.GetComponent<Text>().text = "0";
            tSBS.SkillMax.GetComponent<Text>().text = Skill_Info_Manager.Skill_List[i].MaxCost;
            tSBS.SkillListIndex = i;
            tSBS.playerInfo = playerInfo;
            tSBS.DetailPresentAction = ShowDetailPresent;
            tSBS.IsSskill = true;

            counter++;
            teSizX += 200;
            if (counter % 2 == 0)
            {
                teSizY += 100;
                teSizX = -100;
            }
        }

        MoveNormalListToDispaly();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(InSRankSkillList)
        {
            SkillPointLeftGO.GetComponent<Text>().text = playerInfo.SuperPointAva.ToString();
        }
        else
        {
            SkillPointLeftGO.GetComponent<Text>().text = playerInfo.skillPointAva.ToString();
        }

        if(CheckIfDecideBtnAvaliable())
        {
            DeciedeSkillBtn.SetActive(true);
        }
        else
        {
            DeciedeSkillBtn.SetActive(false);
        }
    }

    private void OnEnable()
    {
        InSRankSkillList = false;

        if (!playerInfo)
            if (playerIn_GO)
            {
                playerInfo = playerIn_GO.GetComponent<Player_Info>();
            }

        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).ForceSetNextPageBtnActive(false);

       Class_Info tCI = CommonFunction.GetProperBClassBasic(playerInfo.Clasz, playerInfo.Rank);
        if(tCI!=null)
        {
            playerInfo.skillPointAva = int.Parse(tCI.baseSkillPoint);
            playerInfo.skillPointAvaMax = playerInfo.skillPointAva;

            playerInfo.SuperPointAva = int.Parse(tCI.baseSurperSkillPoint);
            playerInfo.SuperPointAvaMax = playerInfo.SuperPointAva;

            OnResetSkillPoint();
        }

 
    }

    private void OnDisable()
    {
        
    }
    public void OnResetSkillPoint()
    {


        foreach (Transform child in ScrollViewContentGO.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in NormalSkillListGo.transform)
        {
            GameObject.Destroy(child.gameObject);
        }



        foreach (Transform child in SRankSkillListGo.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        playerInfo.SkillIndexes.Clear();

        playerInfo.skillPointAva = playerInfo.skillPointAvaMax;
        playerInfo.SuperPointAva = playerInfo.SuperPointAvaMax;


        InitialSkillList();

        // if(playerInfo.Rank =="S")
        if (playerInfo.SuperPointAvaMax != 0)
        {
            ChangeToSBtnGO.SetActive(true);
        }
        else
        {
            ChangeToSBtnGO.SetActive(false);
        }

        switch (playerInfo.Clasz)
        {
            case "W":
                WClaszGO.SetActive(true);
                AClaszGO.SetActive(false);
                MClaszGO.SetActive(false);
                VClaszGO.SetActive(false);
                break;
            case "A":
                WClaszGO.SetActive(false);
                AClaszGO.SetActive(true);
                MClaszGO.SetActive(false);
                VClaszGO.SetActive(false);
                break;
            case "M":
                WClaszGO.SetActive(false);
                AClaszGO.SetActive(false);
                MClaszGO.SetActive(true);
                VClaszGO.SetActive(false);
                break;
            case "N":
            default:
                WClaszGO.SetActive(false);
                AClaszGO.SetActive(false);
                MClaszGO.SetActive(false);
                VClaszGO.SetActive(true);
                break;
        }
    }

    public void OnChangeRank()
    {
        if(!InSRankSkillList)
        {
            ChangeBtnToSGO.SetActive(false);
            ChangeBtnToNormalGO.SetActive(true);
            SRankTitleGO.SetActive(true);
            NormalTitleGO.SetActive(false);


            MoveSListToDisplay();

        }
        else
        {
            ChangeBtnToSGO.SetActive(true);
            ChangeBtnToNormalGO.SetActive(false);
            SRankTitleGO.SetActive(false);
            NormalTitleGO.SetActive(true);

            MoveNormalListToDispaly();
        }

        InSRankSkillList = !InSRankSkillList;
    }

    public void OnLeaveDetail()
    {
        DetailPresenterGO.SetActive(false);
        DetailPresenterGO.GetComponentInChildren<Text>().text = "";
    }

    private  void MoveNormalListToDispaly()
    {
        while (ScrollViewContentGO.transform.childCount > 0)
        {
            ScrollViewContentGO.transform.GetChild(0).SetParent(SRankSkillListGo.transform);
        }

        while (NormalSkillListGo.transform.childCount > 0)
        {
            NormalSkillListGo.transform.GetChild(0).SetParent(ScrollViewContentGO.transform);
        }
    }

    private void MoveSListToDisplay()
    {
        while (ScrollViewContentGO.transform.childCount > 0)
        {
            ScrollViewContentGO.transform.GetChild(0).SetParent(NormalSkillListGo.transform);
        }

        while (SRankSkillListGo.transform.childCount > 0)
        {
            SRankSkillListGo.transform.GetChild(0).SetParent(ScrollViewContentGO.transform);
        }
    }

    public void OnDecideSkill()
    {
        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).NextPage();
    }

    private bool CheckIfDecideBtnAvaliable()
    {
        if (playerInfo.Rank == "N") return true;
        if(playerInfo.skillPointAva ==0)
        {
            if (playerInfo.SuperPointAva == 0)
                return true; 
        }
        return false;
    }
}
