using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayerCardPageScript : BasePageScript
{
    public GameObject playerIn_GO;
    private Player_Info playerInfo;

    public GameObject PlayerImgGO;
    public GameObject SkillObjectsGO;
    public GameObject _RankGO;
    public GameObject _ClaszGO;
    public GameObject HPGO;
    public GameObject ArrowGO;
    public GameObject MagicGO;
    public GameObject ThorwGO;
    public GameObject ClazeSpecialSkillGO;
    public GameObject NamePlateGO;
    public GameObject SideImageGO;
    public GameObject ContinueHitGO;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (!playerInfo)
            if (playerIn_GO)
            {
                playerInfo = playerIn_GO.GetComponent<Player_Info>();
            }

        Initial();
    }

    private bool DecideIfSkillIsLast(int SkillIndex)
    {

        return true;

    }

    private void CalutlateFinialPlayerProperty()
    {
        foreach(var s in playerInfo.SkillIndexes)
        {
            string tempHP = Skill_Info_Manager.Skill_List[s].HPBuff;
            if(tempHP!="")
            {
                playerInfo.HP += int.Parse(tempHP);
            }

            string tempArrow = Skill_Info_Manager.Skill_List[s].ArrowBuff;
            if (tempArrow != "")
            {
                playerInfo.ArrowCount += int.Parse(tempArrow);
            }

            string tempThrow = Skill_Info_Manager.Skill_List[s].ThrowBuff;
            if (tempThrow != "")
            {
                playerInfo.ThrowCount += int.Parse(tempThrow);
            }

        }
    }
    public void Initial()
    {
        for(int i=0;i<SkillObjectsGO.transform.childCount;i++)
        {
            SkillObjectsGO.transform.GetChild(i).GetComponent<Image>().sprite = null;
            SkillObjectsGO.transform.GetChild(i).GetComponentInChildren<Text>().text = "";
            SkillObjectsGO.transform.GetChild(i).gameObject.SetActive(false);
        }

        int SkillCount = 0;
        for (int i = 0; i < playerInfo.SkillIndexes.Count; i++)
        {
            if(DecideIfSkillIsLast(playerInfo.SkillIndexes[i]))
            {
                SkillObjectsGO.transform.GetChild(SkillCount).GetComponent<Image>().sprite = Skill_Info_Manager.Skill_List[playerInfo.SkillIndexes[i]].Image;
                SkillObjectsGO.transform.GetChild(SkillCount).GetComponentInChildren<Text>().text = Skill_Info_Manager.Skill_List[playerInfo.SkillIndexes[i]].Name;
                SkillObjectsGO.transform.GetChild(SkillCount).gameObject.SetActive(true);
                SkillCount++;
            }           
        }

        if(playerInfo.Clasz =="W")
        {
            _ClaszGO.GetComponent<Image>().sprite = Skill_Info_Manager.W_ClaszImg;
            ClazeSpecialSkillGO.GetComponent<Image>().sprite = Skill_Info_Manager.W_ClaszSkillImg;
            ClazeSpecialSkillGO.SetActive(true);
        }

        if (playerInfo.Clasz == "A")
        {
            _ClaszGO.GetComponent<Image>().sprite = Skill_Info_Manager.A_ClaszImg;
            ClazeSpecialSkillGO.GetComponent<Image>().sprite = Skill_Info_Manager.A_ClaszSkillImg;
            ClazeSpecialSkillGO.SetActive(true);
        }

        if (playerInfo.Clasz == "M")
        {
            _ClaszGO.GetComponent<Image>().sprite = Skill_Info_Manager.M_ClaszImg;
            ClazeSpecialSkillGO.GetComponent<Image>().sprite = Skill_Info_Manager.M_ClaszSkillImg;
            ClazeSpecialSkillGO.SetActive(true);
        }

        if (playerInfo.Clasz == "N")
        {
            _ClaszGO.GetComponent<Image>().sprite = Skill_Info_Manager.N_ClaszImg;
            ClazeSpecialSkillGO.SetActive(false);
        }

        if(playerInfo.Rank =="S")
        {
            _RankGO.GetComponent<Image>().sprite = Skill_Info_Manager.S_RankImg;
        }
        if (playerInfo.Rank == "A")
        {
            _RankGO.GetComponent<Image>().sprite = Skill_Info_Manager.A_RankImg;
        }
        if (playerInfo.Rank == "B")
        {
            _RankGO.GetComponent<Image>().sprite = Skill_Info_Manager.B_RankImg;
        }
        if (playerInfo.Rank == "C")
        {
            _RankGO.GetComponent<Image>().sprite = Skill_Info_Manager.C_RankImg;
        }
        if (playerInfo.Rank == "N")
        {
            _RankGO.GetComponent<Image>().sprite = Skill_Info_Manager.N_RankImg;
        }

        if(playerInfo.Sided =="BL")
        {
            SideImageGO.GetComponent<Image>().sprite = Skill_Info_Manager.Side_BIImg;
        }

        if (playerInfo.Sided == "AD")
        {
            SideImageGO.GetComponent<Image>().sprite = Skill_Info_Manager.Side_ADImg;
        }

        if (playerInfo.Sided == "JD")
        {
            SideImageGO.GetComponent<Image>().sprite = Skill_Info_Manager.Side_JDImg;
        }

        if (playerInfo.Sided == "DN")
        {
            SideImageGO.GetComponent<Image>().sprite = Skill_Info_Manager.Side_DNImg;
        }

        if (playerInfo.Sided == "NT")
        {
            SideImageGO.GetComponent<Image>().sprite = Skill_Info_Manager.Side_NTImg;
        }

        CalutlateFinialPlayerProperty();

        HPGO.transform.GetChild(0).GetComponent<Text>().text = playerInfo.HP.ToString();
        ThorwGO.transform.GetChild(0).GetComponent<Text>().text = playerInfo.ThrowCount.ToString();
        ArrowGO.transform.GetChild(0).GetComponent<Text>().text = playerInfo.ArrowCount.ToString();
        MagicGO.transform.GetChild(0).GetComponent<Text>().text = playerInfo.MagicCount.ToString();

        if(playerInfo.withHelmet)
        {
            ContinueHitGO.SetActive(true);
        }
        else
        {
            ContinueHitGO.SetActive(false);
        }
        
    }
}
