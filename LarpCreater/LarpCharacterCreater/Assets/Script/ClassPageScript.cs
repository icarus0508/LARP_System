using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassPageScript : MonoBehaviour
{

    public GameObject PerfabButton;
    public GameObject PlayerInfoGO;
    protected PlayerData PlayerInfo;

    public GameObject SkillPointShowerGO = null;
    protected Text SkillPointLeftTextShower = null;



    public GameObject TheCanvas;

    private List<GameObject> SkillButtonList = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
 
        
    }
    protected virtual void OnEnableThisPage()
    {
        if (PlayerInfoGO)
        {
            PlayerInfo = PlayerInfoGO.GetComponent<PlayerData>();
        }

        if(PlayerInfo)
        {
            if(PlayerInfo.PlayerLevel =="A")
            {             
                if(PlayerInfo.PlayerClass =="War")
                {
                    PlayerInfo.SkillPointLeft = PlayerInfo.SkillPointMax = 3;
                    PlayerInfo.HP = 5;
                    PlayerInfo.MPCount = 0;
                    PlayerInfo.ArrowCount = 0;
                    PlayerInfo.ThrowCount = 0;
                }
                else if(PlayerInfo.PlayerClass =="Arc")
                {
                    PlayerInfo.SkillPointLeft = PlayerInfo.SkillPointMax = 3;
                    PlayerInfo.HP = 5;
                    PlayerInfo.MPCount = 0;
                    PlayerInfo.ArrowCount = 12;
                    PlayerInfo.ThrowCount = 0;
                }
                else //if(PlayerInfo.PlayerClass =="Mag")
                {
                    PlayerInfo.SkillPointLeft = PlayerInfo.SkillPointMax = 4;
                    PlayerInfo.HP = 4;
                    PlayerInfo.MPCount = 10;
                    PlayerInfo.ArrowCount = 0;
                    PlayerInfo.ThrowCount = 0;
                }
            }
            else if (PlayerInfo.PlayerLevel == "B")
            {
                if (PlayerInfo.PlayerClass == "War")
                {
                    PlayerInfo.SkillPointLeft = PlayerInfo.SkillPointMax = 2;
                    PlayerInfo.HP = 3;
                    PlayerInfo.MPCount = 0;
                    PlayerInfo.ArrowCount = 0;
                    PlayerInfo.ThrowCount = 0;
                }
                else if (PlayerInfo.PlayerClass == "Arc")
                {
                    PlayerInfo.SkillPointLeft = PlayerInfo.SkillPointMax = 2;
                    PlayerInfo.HP = 3;
                    PlayerInfo.MPCount = 0;
                    PlayerInfo.ArrowCount = 12;
                    PlayerInfo.ThrowCount = 0;
                }
                else //if(PlayerInfo.PlayerClass =="Mag")
                {
                    PlayerInfo.SkillPointLeft = PlayerInfo.SkillPointMax = 3;
                    PlayerInfo.HP = 2;
                    PlayerInfo.MPCount = 10;
                    PlayerInfo.ArrowCount = 0;
                    PlayerInfo.ThrowCount = 0;
                }
            }
            else //C
            {
                if (PlayerInfo.PlayerClass == "War")
                {
                    PlayerInfo.SkillPointLeft = PlayerInfo.SkillPointMax = 1;
                    PlayerInfo.HP = 2;
                    PlayerInfo.MPCount = 0;
                    PlayerInfo.ArrowCount = 0;
                    PlayerInfo.ThrowCount = 0;
                }
                else if (PlayerInfo.PlayerClass == "Arc")
                {
                    PlayerInfo.SkillPointLeft = PlayerInfo.SkillPointMax = 1;
                    PlayerInfo.HP = 2;
                    PlayerInfo.MPCount = 0;
                    PlayerInfo.ArrowCount = 12;
                    PlayerInfo.ThrowCount = 0;
                }
                else //if(PlayerInfo.PlayerClass =="Mag")
                {
                    PlayerInfo.SkillPointLeft = PlayerInfo.SkillPointMax = 2;
                    PlayerInfo.HP = 1;
                    PlayerInfo.MPCount = 10;
                    PlayerInfo.ArrowCount = 0;
                    PlayerInfo.ThrowCount = 0;
                }
            }

            
        }

        if(SkillPointShowerGO)
        {
            SkillPointLeftTextShower = SkillPointShowerGO.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SkillPointLeftTextShower && PlayerInfo)
        {
            SkillPointLeftTextShower.text = "SP: " + PlayerInfo.SkillPointLeft;
        }
    }

    protected bool IsRankAvalable( SkillDataInfo.SkillData sd)
    {
        if (PlayerInfo)
        {
            if (PlayerInfo.PlayerLevel == "S")
            {
                if (sd.isSskill > 0) return true;
                return false;
            }
            else if (PlayerInfo.PlayerLevel == "A")
            {
                if (sd.isSskill>0) return false;
                if (sd.RankAAvabliab>0)
                    return true;
            }
            else if (PlayerInfo.PlayerLevel == "B")
            {
                if (sd.isSskill > 0) return false;
                if (sd.RankBAvabliab > 0)
                    return true;
            }
            else //(PlayerInfo.PlayerLevel == "C")
            {
                if (sd.isSskill > 0) return false;
                if (sd.RankCAvabliab > 0)
                    return true;
            }
        }

        return false;
    }
    protected bool IsTheClassSkill(string class_Name,SkillDataInfo.SkillData sd)
    {
        if(class_Name =="Gen")
        {
            if (sd.GeneralAvlable > 0)
                return true;
        }
        else if (class_Name == "War")
        {
            if (sd.WorriorAvalable > 0 || sd.GeneralAvlable > 0)
                return true;
        }
        else if (class_Name == "Arc" )
        {
            if (sd.ArcherAvalable > 0 || sd.GeneralAvlable > 0)
                return true;
        }
        else if (class_Name == "Mag")
        {
            if (sd.MageAvalable > 0 || sd.GeneralAvlable > 0)
                return true;
        }
        else
        {
            return false;
        }
        return false;
    }
    protected void RenderSkillList(string class_Name)
    {
        SkillButtonList.Clear();

        if (TheCanvas)
        {
            int x_p = -300, y_p = 200, num_ofList = 0;
            foreach (var i in SkillDataInfo.SkillDataInfoList)
            {

                if (!IsTheClassSkill(class_Name, i)) continue;
                if (!IsRankAvalable(i)) continue;
                if (num_ofList % 5 == 0 && num_ofList != 0)
                {
                    y_p -= 60;
                    x_p = -300;
                }

                GameObject tGO =
                Instantiate(PerfabButton, new Vector3(x_p, y_p, 0), Quaternion.identity);
                tGO.transform.SetParent(TheCanvas.transform, false);
                tGO.GetComponent<SkillButtonScript>().PlayerInfo = PlayerInfo;
                tGO.GetComponent<SkillButtonScript>().SkillInfo = i;
               // tGO.GetComponent<SkillButtonScript>().Inital();
                tGO.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

                SkillButtonList.Add(tGO);

                x_p += 50;

                num_ofList++;
            }

            BuildPreviousLinkOfSkill();
        }
    }

    SkillDataInfo.SkillData FindInSkillDataList(string skillName)
    {
        foreach (var i in SkillDataInfo.SkillDataInfoList)
        {
            if(i.SkillName ==skillName)
            {
                return i;
            }
        }
        return null;
    }

    protected void FindPreviousGameObj(GameObject tGO)
    {
        if (tGO == null) return;

        GameObject tGameObj = null;
        var tempName = FindInSkillDataList(tGO.GetComponent<SkillButtonScript>().SkillInfo.SkillName).PreviousSkill;
        if (tempName != "無")
        {
            foreach (var subSkill in SkillButtonList)
            {
                if (subSkill.GetComponent<SkillButtonScript>().SkillInfo.SkillName == tempName)
                {
                    tGameObj = subSkill;
                    break;
                }
            }

            tGO.GetComponent<SkillButtonScript>().PreviousSkill = tGameObj;

       //     FindPreviousGameObj(tGameObj);
        }
    }
    protected void BuildPreviousLinkOfSkill()
    {
        foreach(var skill in SkillButtonList)
        {
            FindPreviousGameObj(skill);
            skill.GetComponent<SkillButtonScript>().Inital();
        }
    }
    protected void OnDisabelThisPage()
    {
        foreach(var i in SkillButtonList)
        {
            Destroy(i);
        }
        SkillButtonList.Clear();
    }
    private void OnDestroy()
    {
       
    }

    private void OnDisable()
    {
        OnDisabelThisPage();
    }

    private void OnEnable()
    {
        OnEnableThisPage();
    }

    public virtual void OnClickDecide()
    {
        if(PlayerInfo)
        {
            PlayerInfo.PlayerSkillList.Clear();

            foreach (var i in SkillButtonList)
            {
                if (i.GetComponent<SkillButtonScript>().IsClick)
                {
                    PlayerData.SkillInfo skillInfo = new PlayerData.SkillInfo();
                    skillInfo.SkillName = i.GetComponent<SkillButtonScript>().SkillInfo.SkillName;
                    skillInfo.SkillImage = i.GetComponent<SkillButtonScript>().ButtonImage.sprite;
                    PlayerInfo.PlayerSkillList.Add(skillInfo);
                }
            }
        }

    }

}

