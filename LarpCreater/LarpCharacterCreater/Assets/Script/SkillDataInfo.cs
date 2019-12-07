using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;

public class SkillDataInfo : MonoBehaviour
{

    private string SkillIconDirectionPath = ".\\Assets\\Library\\icon\\";
    private string SkillDectionImgDirectionPath = ".\\Assets\\Library\\skill\\";
    public class SkillData
        {
        public string SkillName;
        public string SkillPicLocation;
        public string SkillDescripstion;
        public int GeneralAvlable=0;
        public int ArcherAvalable=0;
        public int WorriorAvalable=0;
        public int MageAvalable=0;
        public int isSskill = 0;
        public string PreviousSkill;
        public string HpModified ;
        public string MPModified ;
        public string ArrowModified ;
        public string ThrowingModified ;
        public int RankAAvabliab = 0;
        public int RankBAvabliab = 0;
        public int RankCAvabliab = 0;

        public Sprite SkillIcon = null;
        public Sprite SkillDescrionImg = null;
            
        }

    static public  List<SkillData> SkillDataInfoList = new List<SkillData>();
   static public Sprite HardSkinImg = null;
   static public Sprite KnowAllImg = null;
    // Start is called before the first frame update
    void Start()
    {
        string filePath = ".\\Assets\\SkillList.xlsx";
        FileStream ExcelReader = File.Open(filePath, FileMode.Open);

        IExcelDataReader excelRead = ExcelReaderFactory.CreateOpenXmlReader(ExcelReader);

        DataSet result = excelRead.AsDataSet();
        string set = "Data";

        int columns = result.Tables[set].Columns.Count;
        int rows = result.Tables[set].Rows.Count;

        // 將資料讀取出來
        for (int i = 1; i < rows; i++)
        {

                SkillData tSkillData = new SkillData();
                tSkillData.SkillName = result.Tables[set].Rows[i][0].ToString();
                tSkillData.SkillDescripstion = result.Tables[set].Rows[i][1].ToString();
                tSkillData.SkillPicLocation = result.Tables[set].Rows[i][2].ToString();
                

                string  temp = result.Tables[set].Rows[i][6].ToString();
                 if(temp!="")
                    tSkillData.GeneralAvlable = int.Parse(temp);

                     temp = result.Tables[set].Rows[i][3].ToString();
                    if (temp != "")
                        tSkillData.WorriorAvalable = int.Parse(temp);

                    temp = result.Tables[set].Rows[i][4].ToString();
                    if (temp != "")
                        tSkillData.ArcherAvalable = int.Parse(temp);

                    temp = result.Tables[set].Rows[i][5].ToString();
                    if (temp != "")
                        tSkillData.MageAvalable = int.Parse(temp);



                    temp = result.Tables[set].Rows[i][7].ToString();
                    if (temp != "")
                        tSkillData.isSskill = int.Parse(temp);

                     tSkillData.PreviousSkill = result.Tables[set].Rows[i][8].ToString(); ;

                  tSkillData.HpModified = result.Tables[set].Rows[i][9].ToString();
                  tSkillData.MPModified = result.Tables[set].Rows[i][10].ToString();
                  tSkillData.ArrowModified = result.Tables[set].Rows[i][11].ToString();
                  tSkillData.ThrowingModified = result.Tables[set].Rows[i][12].ToString();


                    temp = result.Tables[set].Rows[i][13].ToString();
                    if (temp != "")
                        tSkillData.RankAAvabliab = int.Parse(temp);

                    temp = result.Tables[set].Rows[i][14].ToString();
                    if (temp != "")
                        tSkillData.RankBAvabliab = int.Parse(temp);

                    temp = result.Tables[set].Rows[i][15].ToString();
                    if (temp != "")
                        tSkillData.RankCAvabliab = int.Parse(temp);


            Texture2D tex = CommonScript.LoadPNG(SkillIconDirectionPath + tSkillData.SkillPicLocation);
                    tSkillData.SkillIcon = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));

            SkillDataInfoList.Add(tSkillData);
        }

        // 讀取完後一定要關閉
        excelRead.Close();

        Texture2D HSSkin = CommonScript.LoadPNG(SkillIconDirectionPath+ "硬甲.png");
        HardSkinImg = Sprite.Create(HSSkin, new Rect(0, 0, HSSkin.width, HSSkin.height), new Vector2(0, 0));

        Texture2D KnowAll = CommonScript.LoadPNG(SkillIconDirectionPath + "博學者.png");
        KnowAllImg = Sprite.Create(KnowAll, new Rect(0, 0, KnowAll.width, KnowAll.height), new Vector2(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
