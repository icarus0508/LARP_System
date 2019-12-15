using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;

public class Skill_info
{
    public string Name;
    public string ImageName;
    public Sprite Image;
    public string Description;
    public string DetailDescription;
    public string AvaClass;
    public string PreSkillName;
    public string HPBuff;
    public string MPBuff;
    public string ArrowBuff;
    public string ThrowBuff;
    public string AvaRank;
    public string MaxCost ;

    public int PreSkillIndex = -1;
    public int PostSkillIndex = -1;
    

}

public class Skill_Info_Manager : MonoBehaviour
{
    private string SkillIconDirectionPath = ".\\Assets\\Images\\Skill\\";
    private string UIDirectionPath = ".\\Assets\\Images\\UI\\";
    static public List<Skill_info> Skill_List = new List<Skill_info>();
    public static Sprite W_ClaszImg = null;
    public static Sprite A_ClaszImg = null;
    public static Sprite M_ClaszImg = null;
    public static Sprite N_ClaszImg = null;
    public static Sprite W_ClaszSkillImg = null;
    public static Sprite A_ClaszSkillImg = null;
    public static Sprite M_ClaszSkillImg = null;
    public static Sprite S_RankImg = null;
    public static Sprite A_RankImg = null;
    public static Sprite B_RankImg = null;
    public static Sprite C_RankImg = null;
    public static Sprite N_RankImg = null;
    public static Sprite Side_BIImg = null;
    public static Sprite Side_ADImg = null;
    public static Sprite Side_JDImg = null;
    public static Sprite Side_DNImg = null;
    public static Sprite Side_NTImg = null;


    public bool DataIsReady = false;
   
    void LoadSkill()
    {
        string filePath = ".\\Assets\\Excel\\Skill.xlsx";
        FileStream ExcelReader = File.Open(filePath, FileMode.Open);

        IExcelDataReader excelRead = ExcelReaderFactory.CreateOpenXmlReader(ExcelReader);

        DataSet result = excelRead.AsDataSet();
        string set = "Data";

        int columns = result.Tables[set].Columns.Count;
        int rows = result.Tables[set].Rows.Count;

        for (int i=1;i<rows; i++)
        {
            Skill_info tSkillInfo = new Skill_info();
            tSkillInfo.Name = result.Tables[set].Rows[i][0].ToString();
            tSkillInfo.ImageName = result.Tables[set].Rows[i][1].ToString();
            tSkillInfo.Description = result.Tables[set].Rows[i][2].ToString();
            tSkillInfo.DetailDescription = result.Tables[set].Rows[i][3].ToString();
            tSkillInfo.AvaClass = result.Tables[set].Rows[i][4].ToString();
            tSkillInfo.PreSkillName = result.Tables[set].Rows[i][5].ToString();
            tSkillInfo.HPBuff = result.Tables[set].Rows[i][6].ToString();
            tSkillInfo.MPBuff = result.Tables[set].Rows[i][7].ToString();
            tSkillInfo.ArrowBuff = result.Tables[set].Rows[i][8].ToString();
            tSkillInfo.ThrowBuff = result.Tables[set].Rows[i][9].ToString();
            tSkillInfo.AvaRank = result.Tables[set].Rows[i][10].ToString();
            tSkillInfo.MaxCost = result.Tables[set].Rows[i][11].ToString();

            Texture2D tex = CommonFunction.LoadPNG(SkillIconDirectionPath + tSkillInfo.ImageName);
            tSkillInfo.Image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));

            Skill_List.Add(tSkillInfo);
        }

        excelRead.Close();

        for(int i=0;i<Skill_List.Count;i++)
        {
            if(Skill_List[i].PreSkillName!=null)
            {
                for(int j=0;j<Skill_List.Count;j++)
                {
                    if(Skill_List[j].Name ==  Skill_List[i].PreSkillName)
                    {
                        Skill_List[i].PreSkillIndex = j;
                        Skill_List[j].PostSkillIndex = i;
                    }
                }
            }
        }

        W_ClaszImg = CreateSpriteByImg(UIDirectionPath + "戰_圓形.png");
        A_ClaszImg = CreateSpriteByImg(UIDirectionPath + "弓_圓形.png");
        M_ClaszImg = CreateSpriteByImg(UIDirectionPath + "法_圓形.png");
        N_ClaszImg = CreateSpriteByImg(UIDirectionPath + "民_圓形.png");
        W_ClaszSkillImg =CreateSpriteByImg(SkillIconDirectionPath + "硬甲.png");
        A_ClaszSkillImg = CreateSpriteByImg(SkillIconDirectionPath + "韌甲.png");
        M_ClaszSkillImg = CreateSpriteByImg(SkillIconDirectionPath + "博學者.png");

        S_RankImg = CreateSpriteByImg(UIDirectionPath + "OuterFrameS.png");
        A_RankImg = CreateSpriteByImg(UIDirectionPath + "OuterFrameA.png");
        B_RankImg = CreateSpriteByImg(UIDirectionPath + "OuterFrameB.png");
        C_RankImg = CreateSpriteByImg(UIDirectionPath + "OuterFrameC.png");
        N_RankImg = CreateSpriteByImg(UIDirectionPath + "OuterFrameN.png");

        Side_BIImg = CreateSpriteByImg(UIDirectionPath + "RaceEmpire.png");
        Side_ADImg = CreateSpriteByImg(UIDirectionPath + "RaceHorde.png");
        Side_JDImg = CreateSpriteByImg(UIDirectionPath + "RaceAsia.png");
        Side_DNImg = CreateSpriteByImg(UIDirectionPath + "RaceMonster.png");
        Side_NTImg = CreateSpriteByImg(UIDirectionPath + "RaceEmpire.png");

        DataIsReady = true;
    }
  
    private Sprite CreateSpriteByImg(string imgPath)
    {
        Texture2D tImg = CommonFunction.LoadPNG(imgPath);
        return Sprite.Create(tImg, new Rect(0, 0, tImg.width, tImg.height), new Vector2(0, 0));
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadSkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

