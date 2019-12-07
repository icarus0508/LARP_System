using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletePageScript : MonoBehaviour
{

    public Image RankTag_C = null;
    public Image RankTag_B = null;
    public Image RankTag_A = null;
    public Image RankTag_S = null;
    public Image RankTag_SR = null;

    public Image PlayerPhoto = null;

    public Image PlayerClass_Arc = null;
    public Image PlayerClass_War = null;
    public Image PlayerClass_Mag = null;


    public GameObject PrefabeSkill = null;
    protected List<GameObject> SkillList = new List<GameObject>();

    public GameObject TheCanvas;

    public Image SideTag_Eest = null;
    public Image SideTag_West = null;
    public Image SideTag_Nor = null;
    public Image SideTag_Sou = null;
    public Image SideTag_Non = null;


    public Image AttributeTag = null;
    public Text HPText = null;
    public Text MgText = null;
    public Text ArrowText = null;
    public Text ThrowText = null;

    public Text PlayerName = null;

    public Image ComboResist = null;


    public GameObject PlayerInfoGO =null;
    protected PlayerData PlayerInfo = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    
    void SideTagEnable(bool E,bool W,bool N,bool S,bool NON)
    {
        if (E) SideTag_Eest.gameObject.SetActive(true); else SideTag_Eest.gameObject.SetActive(false);
        if (W) SideTag_West.gameObject.SetActive(true); else SideTag_West.gameObject.SetActive(false);
        if (N) SideTag_Nor.gameObject.SetActive(true); else SideTag_Nor.gameObject.SetActive(false);
        if (S) SideTag_Sou.gameObject.SetActive(true); else SideTag_Sou.gameObject.SetActive(false);
        if (NON) SideTag_Non.gameObject.SetActive(true); else SideTag_Non.gameObject.SetActive(false);
    }

    void RankTagEnable(bool C,bool B,bool A,bool S,bool SR)
    {
        if (C) RankTag_C.gameObject.SetActive(true); else RankTag_C.gameObject.SetActive(false);
        if (B) RankTag_B.gameObject.SetActive(true); else RankTag_B.gameObject.SetActive(false);
        if (A) RankTag_A.gameObject.SetActive(true); else RankTag_A.gameObject.SetActive(false);
        if (S) RankTag_S.gameObject.SetActive(true); else RankTag_S.gameObject.SetActive(false);
        if (SR) RankTag_SR.gameObject.SetActive(true); else RankTag_SR.gameObject.SetActive(false);
    }

    void ClassTagEnable(bool War,bool Arc,bool Mag)
    {
        if (War) PlayerClass_War.gameObject.SetActive(true); else PlayerClass_War.gameObject.SetActive(false);
        if (Arc) PlayerClass_Arc.gameObject.SetActive(true); else PlayerClass_Arc.gameObject.SetActive(false);
        if (Mag) PlayerClass_Mag.gameObject.SetActive(true); else PlayerClass_Mag.gameObject.SetActive(false);
    }

    void SetUpSkill( Image skill , Sprite tex,string text)
    {
        skill.gameObject.SetActive(true);
        skill.sprite = tex;
        skill.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = text;

    }
    private void OnEnable()
    {
        if(PlayerInfo==null)
            if (PlayerInfoGO)
            {
                PlayerInfo = PlayerInfoGO.GetComponent<PlayerData>();
            }

        if(PlayerInfo!=null)
        {
            if (PlayerPhoto)
                PlayerPhoto.sprite = PlayerInfo.PlayerImageSprite;

             switch(PlayerInfo.PlayerGroupIndex)
            {
                case GroupDataInfo.GroupIndex.EastGroup:
                    SideTagEnable(true, false, false, false, false);
                    break;
                case GroupDataInfo.GroupIndex.WestGroup:
                    SideTagEnable(false, true, false, false, false);
                    break;
                case GroupDataInfo.GroupIndex.NorGroup:
                    SideTagEnable(false, false, true, false, false);
                    break;
                case GroupDataInfo.GroupIndex.SouGroup:
                    SideTagEnable(false, false, false, true, false);
                    break;
                case GroupDataInfo.GroupIndex.FreeGroup:
                    SideTagEnable(false, false, false, false, true);
                    break;
            }

            if (PlayerInfo.PlayerLevel == "C") RankTagEnable(true, false, false, false, false);
            else if (PlayerInfo.PlayerLevel == "B") RankTagEnable(false, true, false, false, false);
            else if (PlayerInfo.PlayerLevel == "A") RankTagEnable(false, false, true, false, false);
            else if (PlayerInfo.PlayerLevel == "S") RankTagEnable(false, false, false, true, false);
            else /*(PlayerInfo.PlayerLevel == "SR")*/ RankTagEnable(false, false, false, false, true);


            if (PlayerInfo.PlayerClass == "War")
            {
                ClassTagEnable(true, false, false);
                AttributeTag.sprite = SkillDataInfo.HardSkinImg;
            }

            else if (PlayerInfo.PlayerClass == "Arc")
            {
                ClassTagEnable(false, true, false);
                AttributeTag.sprite = SkillDataInfo.HardSkinImg;
            }
            else /*(PlayerInfo.PlayerClass == "Mag")*/
            {
                ClassTagEnable(false, false, true);
                AttributeTag.sprite = SkillDataInfo.KnowAllImg;
            }
                

            if(PlayerInfo)
            {

                SkillList.Clear();

                int skillX=-195, skillY=-325;
                for (int i = 0; i < 6; i++)
                {
                    GameObject tGO =
                    Instantiate(PrefabeSkill, new Vector3(skillX, skillY, 0), Quaternion.identity);
                    tGO.transform.SetParent(TheCanvas.transform, false);
                    tGO.GetComponent<RectTransform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    tGO.SetActive(false);
                    SkillList.Add(tGO);
                    skillX += 78;
                }

                int count = 0;
                foreach (var i in PlayerInfo.PlayerSkillList)
                {
                    SetUpSkill(SkillList[count].GetComponent<Image>(), i.SkillImage, i.SkillName);

                    count++;
                }

                PlayerName.text = PlayerInfo.PlayerName;

                HPText.text = PlayerInfo.HP.ToString();
                int temp = PlayerInfo.MPCount * PlayerInfo.MultiplyMpCount;
                MgText.text = temp.ToString() ;
                ArrowText.text = PlayerInfo.ArrowCount.ToString();
                ThrowText.text = PlayerInfo.ThrowCount.ToString();

                TheCanvas.GetComponent<HPBarScript>().Apply();

                if (PlayerInfo.PlayerWearHamet)
                    ComboResist.gameObject.SetActive(true);
                else
                    ComboResist.gameObject.SetActive(false);


            }

        }

    }
    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ExportImg(string exportPath)
    {

    }
    public void ExportImage()
    {

    }
}
