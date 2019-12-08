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

}

public class Skill_Info_Manager : MonoBehaviour
{
    private string SkillIconDirectionPath = ".\\Assets\\Images\\Skill\\";
    static public List<Skill_info> Skill_List = new List<Skill_info>();
   
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

            Texture2D tex = CommonFunction.LoadPNG(SkillIconDirectionPath + tSkillInfo.ImageName);
            tSkillInfo.Image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));

            Skill_List.Add(tSkillInfo);
        }

        excelRead.Close();
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

