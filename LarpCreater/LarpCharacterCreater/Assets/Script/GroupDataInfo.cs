using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;

public class GroupDataInfo : MonoBehaviour
{
    public  enum GroupIndex
    {
        EastGroup,
        WestGroup,
        NorGroup,
        SouGroup,
        FreeGroup,
        TotalNum,
    }
    public class GroudInfo
    {
        public string GroupName;
        public string GroupDescription;
        public string GoupPic;
    }

    public List<GroudInfo> GourdList = new List<GroudInfo>();
    // Start is called before the first frame update
    void Start()
    {
        string filePath = ".\\Assets\\種族文件.xlsx";
        FileStream ExcelReader = File.Open(filePath, FileMode.Open);

        IExcelDataReader excelRead = ExcelReaderFactory.CreateOpenXmlReader(ExcelReader);

        DataSet result = excelRead.AsDataSet();
        string set = "Data";

        int columns = result.Tables[set].Columns.Count;
        int rows = result.Tables[set].Rows.Count;

        // 將資料讀取出來
        for (int i = 1; i < rows; i++)
        {

            GroudInfo tGoudInfo = new GroudInfo();
            tGoudInfo.GroupName = result.Tables[set].Rows[i][0].ToString();
            tGoudInfo.GroupDescription = result.Tables[set].Rows[i][1].ToString();
            tGoudInfo.GoupPic = result.Tables[set].Rows[i][2].ToString();

            GourdList.Add(tGoudInfo);

        }



        // 讀取完後一定要關閉
        excelRead.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
