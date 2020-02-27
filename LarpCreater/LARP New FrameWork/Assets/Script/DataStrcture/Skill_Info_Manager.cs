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
[System.Serializable]
public class Skill_Save_Info
{
    public string Name;
    public string ImageName;
    public string Description;
    public string DetailDescription;
    public string AvaClass;
    public string PreSkillName;
    public string HPBuff;
    public string MPBuff;
    public string ArrowBuff;
    public string ThrowBuff;
    public string AvaRank;
    public string MaxCost;
}
[System.Serializable]
public class Skill_Save_Info_List
{
    public List<Skill_Save_Info> SkillList = new List<Skill_Save_Info>();

    public string ConvertToJason()
    {
        return JsonUtility.ToJson(this, true);
    }
}

public class Skill_Info_Manager : MonoBehaviour
{
    private string SkillIconDirectionPath = "\\Images\\Skill\\";
   // private string UIDirectionPath = "\\Images\\UI\\";
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
        string filePath = Application.dataPath + "\\Excel\\Skill.xlsx";

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

            Texture2D tex = CommonFunction.LoadPNG(Application.dataPath + SkillIconDirectionPath + tSkillInfo.ImageName+".png");
            tSkillInfo.Image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));

            Skill_List.Add(tSkillInfo);
        }

        excelRead.Close();

        SettingUpSkillList();

        DataIsReady = true;

        ExportSkillListToExcel();
    }
  
    private void SettingUpSkillList()
    {
        for (int i = 0; i < Skill_List.Count; i++)
        {
            if (Skill_List[i].PreSkillName != null)
            {
                for (int j = 0; j < Skill_List.Count; j++)
                {
                    if (Skill_List[j].Name == Skill_List[i].PreSkillName)
                    {
                        Skill_List[i].PreSkillIndex = j;
                        Skill_List[j].PostSkillIndex = i;
                    }
                }
            }
        }

        W_ClaszImg = CreateSpriteByImg("Images/UI/" + "戰_圓形");
        A_ClaszImg = CreateSpriteByImg("Images/UI/" + "弓_圓形");
        M_ClaszImg = CreateSpriteByImg("Images/UI/" + "法_圓形");
        N_ClaszImg = CreateSpriteByImg("Images/UI/" + "民_圓形");
        W_ClaszSkillImg = CreateSpriteByImg("Images/Skill/" + "硬甲");
        A_ClaszSkillImg = CreateSpriteByImg("Images/Skill/" + "韌甲");
        M_ClaszSkillImg = CreateSpriteByImg("Images/Skill/" + "博學者");

        S_RankImg = CreateSpriteByImg("Images/UI/" + "OuterFrameS");
        A_RankImg = CreateSpriteByImg("Images/UI/" + "OuterFrameA");
        B_RankImg = CreateSpriteByImg("Images/UI/" + "OuterFrameB");
        C_RankImg = CreateSpriteByImg("Images/UI/" + "OuterFrameC");
        N_RankImg = CreateSpriteByImg("Images/UI/" + "OuterFrameN");

        Side_BIImg = CreateSpriteByImg("Images/UI/" + "RaceEmpire");
        Side_ADImg = CreateSpriteByImg("Images/UI/" + "RaceHorde");
        Side_JDImg = CreateSpriteByImg("Images/UI/" + "RaceAsia");
        Side_DNImg = CreateSpriteByImg("Images/UI/" + "RaceMonster");
        Side_NTImg = CreateSpriteByImg("Images/UI/" + "RaceEmpire");
    }
    public void LoadSkillFromJason()
    {
        var jsonTextFile = Resources.Load<TextAsset>("SkillListJason");
        Skill_Save_Info_List tSSIL = JsonUtility.FromJson<Skill_Save_Info_List>(jsonTextFile.text);

        Skill_List.Clear();
        foreach(var s in tSSIL.SkillList)
        {
            Skill_info tSkillInfo = new Skill_info();
            tSkillInfo.Name =  s.Name;
            tSkillInfo.ImageName = s.ImageName;
            tSkillInfo.Description = s.Description;
            tSkillInfo.DetailDescription = s.DetailDescription;
            tSkillInfo.AvaClass = s.AvaClass;
            tSkillInfo.PreSkillName = s.PreSkillName;
            tSkillInfo.HPBuff = s.HPBuff;
            tSkillInfo.MPBuff = s.MPBuff;
            tSkillInfo.ArrowBuff = s.ArrowBuff;
            tSkillInfo.ThrowBuff = s.ThrowBuff;
            tSkillInfo.AvaRank = s.AvaRank;
            tSkillInfo.MaxCost = s.MaxCost;

            Texture2D tex = CommonFunction.LoadPNG_FromResources("Images/Skill/" + tSkillInfo.ImageName);
            tSkillInfo.Image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));

            Skill_List.Add(tSkillInfo);
        }


        SettingUpSkillList();
        DataIsReady = true;

    }
    public void ExportSkillListToExcel()
    {
        Skill_Save_Info_List tSSIL = new Skill_Save_Info_List();
        foreach(var s in Skill_List)
        {
            Skill_Save_Info tSSI = new Skill_Save_Info();
            tSSI.Name = s.Name;
            tSSI.ImageName = s.ImageName;
            tSSI.Description = s.Description;
            tSSI.DetailDescription = s.DetailDescription;
            tSSI.AvaClass = s.AvaClass;
            tSSI.PreSkillName = s.PreSkillName;
            tSSI.HPBuff = s.HPBuff;
            tSSI.MPBuff = s.MPBuff;
            tSSI.ArrowBuff = s.ArrowBuff;
            tSSI.ThrowBuff = s.ThrowBuff;
            tSSI.AvaRank = s.AvaRank;
            tSSI.MaxCost = s.MaxCost;
            tSSIL.SkillList.Add(tSSI);
        }

        string tJson = tSSIL.ConvertToJason();
        CommonFunction.SaveJason(Application.dataPath  + "/SkillListJason.json", tJson);
    }
    private Sprite CreateSpriteByImg(string imgPath)
    {
        Texture2D tImg = CommonFunction.LoadPNG_FromResources(imgPath);
        return Sprite.Create(tImg, new Rect(0, 0, tImg.width, tImg.height), new Vector2(0, 0));
    }
    // Start is called before the first frame update
    void Start()
    {
       // LoadSkill();
        LoadSkillFromJason();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

