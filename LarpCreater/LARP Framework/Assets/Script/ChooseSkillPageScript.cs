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

    public GameObject NormalTitleGO;
    public GameObject SRankTitleGO;

    public GameObject SkillPointLeftGO;

    public GameObject ChangeBtnToSGO;
    public GameObject ChangeBtnToNormalGO;

    public GameObject DetailPresenterGO;

    public GameObject Prfab_SkillGO = null;
    public GameObject ScrollViewGO = null;
    public GameObject ScrollViewContentGO = null;
    private bool InSRankSkillList = false;

    public GameObject SRankSkillListGo = null;
    public GameObject NormalSkillListGo = null;

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
        if(Skill_Info_Manager.Skill_List[Index].PreSkillIndex!=-1)
        {
            return false;
        }
        return true;
    }
    public void ShowDetailPresent(int indexOfSkill)
    {
        DetailPresenterGO.SetActive(true);
        DetailPresenterGO.GetComponentInChildren<Text>().text = Skill_Info_Manager.Skill_List[indexOfSkill].DetailDescription;
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
        // TestScrollBar();
        InitialSkillList();
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
    }

    private void OnEnable()
    {
        InSRankSkillList = false;

        if (!playerInfo)
            if (playerIn_GO)
            {
                playerInfo = playerIn_GO.GetComponent<Player_Info>();
            }
    }

    public void OnResetSkillPoint()
    {
        for(int i=0;i<ScrollViewContentGO.transform.childCount;i++)
        {
            ScrollViewContentGO.transform.GetChild(i).GetComponentInChildren<SkillButtonScript>().Reset();
        }

        for (int i = 0; i < NormalSkillListGo.transform.childCount; i++)
        {
            NormalSkillListGo.transform.GetChild(i).GetComponentInChildren<SkillButtonScript>().Reset();
        }

        for (int i = 0; i < SRankSkillListGo.transform.childCount; i++)
        {
            SRankSkillListGo.transform.GetChild(i).GetComponentInChildren<SkillButtonScript>().Reset();
        }

        playerInfo.SkillIndexes.Clear();

        playerInfo.skillPointAva = playerInfo.skillPointAvaMax;
        playerInfo.SuperPointAva = playerInfo.SuperPointAvaMax;
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
}
