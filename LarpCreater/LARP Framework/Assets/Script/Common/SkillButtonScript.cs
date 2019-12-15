using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Events;

public class SkillButtonScript : MonoBehaviour
{
   

    public Player_Info playerInfo;

    public GameObject SkillMainImgGO=null;

    public GameObject SkillNameGO = null;
    public GameObject SkillDescription = null;
    public GameObject SkillAva = null;
    public GameObject SkillMax = null;

    public int SkillListIndex = -1;

    public Material DefaultMat = null;
    public Material GrayScaleMat = null;

    public bool IsSskill = false;

    public delegate void detailPresentAction(int indexOfSkill);
    public detailPresentAction DetailPresentAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickDetail()
    {
        DetailPresentAction( SkillListIndex);
    }

    public void OnClickSelect()
    {
        int tSkillMax = int.Parse(SkillMax.GetComponent<Text>().text);
        int tAvaSkill = int.Parse(SkillAva.GetComponent<Text>().text);

        if (tAvaSkill >= tSkillMax) return;

        if(IsSskill)
        {
            if (playerInfo.SuperPointAva <= 0) return;
        }
        else
        {
            if (playerInfo.skillPointAva <= 0) return;
        }
            
        if(Skill_Info_Manager.Skill_List[SkillListIndex].PreSkillIndex!=-1)
        {
            int indexOfPreSkillInCurrentPlayerSkillList = playerInfo.SkillIndexes.IndexOf(Skill_Info_Manager.Skill_List[SkillListIndex].PreSkillIndex);
           if (indexOfPreSkillInCurrentPlayerSkillList != -1)
            {
                playerInfo.SkillIndexes.RemoveAt(indexOfPreSkillInCurrentPlayerSkillList);
            }
        }
            
        playerInfo.SkillIndexes.Add(SkillListIndex);

        tAvaSkill++;
        SkillAva.GetComponent<Text>().text = tAvaSkill.ToString();

        if(IsSskill)
              playerInfo.SuperPointAva--;
        else
            playerInfo.skillPointAva--;

        if (Skill_Info_Manager.Skill_List[SkillListIndex].PreSkillIndex != -1)
        {
            SkillMainImgGO.GetComponent<Image>().sprite = Skill_Info_Manager.Skill_List[SkillListIndex].Image;
            SkillNameGO.GetComponent<Text>().text = Skill_Info_Manager.Skill_List[SkillListIndex].Name;
            SkillDescription.GetComponent<Text>().text = Skill_Info_Manager.Skill_List[SkillListIndex].Description;
        }

        if (Skill_Info_Manager.Skill_List[SkillListIndex].PostSkillIndex!=-1)
        {
            int PostSkillINdex = Skill_Info_Manager.Skill_List[SkillListIndex].PostSkillIndex;
            SkillListIndex = PostSkillINdex;      
        }

        SkillMainImgGO.GetComponent<Image>().material = DefaultMat;
    }

    public void Reset()
    {
        SkillMainImgGO.GetComponent<Image>().material = GrayScaleMat;
        SkillAva.GetComponent<Text>().text = "0";
    }
}
