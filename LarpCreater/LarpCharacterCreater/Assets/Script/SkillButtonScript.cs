using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SkillButtonScript : MonoBehaviour
{

    public bool IsClick = false;
    public GameObject ButtonGO = null;
    public Image ButtonImage =null;

    public PlayerData PlayerInfo=null;
    public SkillDataInfo.SkillData SkillInfo = null;

    public GameObject PreviousSkill = null;

    public delegate void OnTurnOnOffEvent(bool isOn);
    public OnTurnOnOffEvent onTurnOnOffEventCallBack;


    //BackUp
    int Ori_HP = 0;
    int Ori_MP = 0;
    int Ori_Multi_MP = 1;
    int Ori_Arrow = 0;
    int Ori_Thorw = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(ButtonGO!=null)
        {
            ButtonImage= ButtonGO.GetComponent<Image>();
        }
    }

    private void OnDestroy()
    {
        if (PreviousSkill)
            PreviousSkill.GetComponent<SkillButtonScript>().onTurnOnOffEventCallBack -= OnHandleTurnOnOffEvent;
    }
    public void Inital()
    {
        if(ButtonGO)
        {
            ButtonGO.GetComponent<Image>().sprite = SkillInfo.SkillIcon;

        }

       
        IsClick = false;
        if (PreviousSkill)
            PreviousSkill.GetComponent<SkillButtonScript>().onTurnOnOffEventCallBack += OnHandleTurnOnOffEvent;
    }

  
    // Update is called once per frame
    void Update()
    {
 
        if(PreviousSkill)
            if (!PreviousSkill.GetComponent<SkillButtonScript>().IsClick)
            {
                if (ButtonImage != null)
                {
                    ButtonImage.color = Color.gray;
                }
            }
            else
            {
                if(IsClick)
                {
                    if (ButtonImage != null)
                    {
                        ButtonImage.color = Color.red;
                    }
                }
                else
                {
                    if (ButtonImage != null)
                    {
                        ButtonImage.color = Color.white;
                    }
                }

            }
    }

    private void SkillStateChange()
    {
        
        if (IsClick)
        {
            if (PlayerInfo.SkillPointLeft <= 0) return;

            PlayerInfo.SkillPointLeft--;

            if (ButtonImage != null)
            {
                ButtonImage.color = Color.red;
            }

            ModifiedAttributeBySkill(true);
        }
        else
        {
            if (PlayerInfo.SkillPointLeft > PlayerInfo.SkillPointMax) return;

            PlayerInfo.SkillPointLeft++;

            if (ButtonImage != null)
            {
                ButtonImage.color = Color.white;
            }
            ModifiedAttributeBySkill(false);

            
        }

        onTurnOnOffEventCallBack?.Invoke(IsClick);
    }
    public void OnHandleTurnOnOffEvent(bool isclick)
    {
        if(!isclick)
        {
            if (!IsClick) return;

            IsClick = false;

            SkillStateChange();
        }
        else
        {
            if (IsClick) return;

            if (ButtonImage != null)
            {
                ButtonImage.color = Color.white;
            }

            //if (IsClick)
            //{
            //    if (ButtonImage != null)
            //    {
            //        ButtonImage.color = Color.red;
            //    }
            //}
            //else
            //{
            //    if (ButtonImage != null)
            //    {
            //        ButtonImage.color = Color.white;
            //    }
            //}
        }
    }
    public void OnClickSkillButton()
    {
        if (!havePreviousSkillAlreadyClick())
        {
            if (ButtonImage != null)
            {
                ButtonImage.color = Color.gray;
            }
            return;
        }

        IsClick = !IsClick;

        SkillStateChange();
    }

    private void ModifiedAttributeBySkill(bool Apply)
    {
        if(Apply)
        {
            Ori_HP = PlayerInfo.HP;
            Ori_MP = PlayerInfo.MPCount;
            Ori_Multi_MP = PlayerInfo.MultiplyMpCount;
            Ori_Arrow = PlayerInfo.ArrowCount;
            Ori_Thorw = PlayerInfo.ThrowCount;

            var temp = SkillInfo.HpModified;
            if(temp!="")
              PlayerInfo.HP += int.Parse(temp);

            temp = SkillInfo.MPModified;
            if (temp != "")
                if (temp == "x2")
                    PlayerInfo.MultiplyMpCount = 2;
                else
                    PlayerInfo.MPCount = int.Parse(temp);

            temp = SkillInfo.ArrowModified;
            if (temp != "")
                if (temp == "無限")
                    PlayerInfo.ArrowCount = 100;

            temp = SkillInfo.ThrowingModified;
            if (temp != "")
                PlayerInfo.ThrowCount = int.Parse(temp);



        }
        else
        {
            PlayerInfo.HP = Ori_HP;
            PlayerInfo.MPCount = Ori_MP;
            PlayerInfo.MultiplyMpCount = Ori_Multi_MP;
            PlayerInfo.ArrowCount = Ori_Arrow;
            PlayerInfo.ThrowCount = Ori_Thorw;

        }

        Debug.Log("----------------");
        Debug.Log("HP: " + PlayerInfo.HP);
    }

    private bool havePreviousSkillAlreadyClick()
    {
        if (PreviousSkill!= null)
            if(PreviousSkill.GetComponent<SkillButtonScript>().IsClick)
                return true;
            else
            {
                return false;
            }

        return true;
    }
}
