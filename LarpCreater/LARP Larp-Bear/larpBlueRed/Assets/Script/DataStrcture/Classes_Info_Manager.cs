using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;

public class Class_Info
{
    public string className;
    public string rankName;
    public string baseHP;
    public string baseSkillPoint;
    public string baseSurperSkillPoint;
    public string baseArrows;
    public string baseMP;
    public string baseThrowing;
}

[System.Serializable]
public class Class_Save_Info
{
    public string className;
    public string rankName;
    public string baseHP;
    public string baseSkillPoint;
    public string baseSurperSkillPoint;
    public string baseArrows;
    public string baseMP;
    public string baseThrowing;
}
[System.Serializable]
public class Class_Save_Info_List
{
    public List<Class_Save_Info> classList = new List<Class_Save_Info>();

    public string ConvertToJason()
    {
        return JsonUtility.ToJson(this, true);
    }
}

public class Classes_Info_Manager : MonoBehaviour
{
    public static List<Class_Info> classesBaseList = new List<Class_Info>();
    public bool DataIsReady = false;

    public  void LoadClassExcelToJason()
    {
        List<Class_Info> l_classesBaseList = new List<Class_Info>();

        string filePath = Application.dataPath + "\\Excel\\ClassesBase.xlsx";

        FileStream ExcelReader = File.Open(filePath, FileMode.Open);

        IExcelDataReader excelRead = ExcelReaderFactory.CreateOpenXmlReader(ExcelReader);

        DataSet result = excelRead.AsDataSet();
        string set = "Data";

        int columns = result.Tables[set].Columns.Count;
        int rows = result.Tables[set].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            Class_Info tclassInfo = new Class_Info();
            tclassInfo.className      = result.Tables[set].Rows[i][0].ToString();
            tclassInfo.rankName       = result.Tables[set].Rows[i][1].ToString();
            tclassInfo.baseHP         = result.Tables[set].Rows[i][2].ToString();
            tclassInfo.baseSkillPoint = result.Tables[set].Rows[i][3].ToString();
            tclassInfo.baseMP         = result.Tables[set].Rows[i][4].ToString();
            tclassInfo.baseArrows     = result.Tables[set].Rows[i][5].ToString();
            tclassInfo.baseThrowing   = result.Tables[set].Rows[i][6].ToString();
            tclassInfo.baseSurperSkillPoint = result.Tables[set].Rows[i][7].ToString();
            l_classesBaseList.Add(tclassInfo);
        }
        excelRead.Close();

        Class_Save_Info_List tCSIL = new Class_Save_Info_List();
        foreach (var c in l_classesBaseList)
        {
            Class_Save_Info tCSI = new Class_Save_Info();
            tCSI.className = c.className;
            tCSI.rankName = c.rankName;
            tCSI.baseHP = c.baseHP;
            tCSI.baseSkillPoint = c.baseSkillPoint;
            tCSI.baseArrows = c.baseArrows;
            tCSI.baseMP = c.baseMP;
            tCSI.baseThrowing = c.baseThrowing;
            tCSI.baseSurperSkillPoint = c.baseSurperSkillPoint;
            tCSIL.classList.Add(tCSI);
        }

        string tJson = tCSIL.ConvertToJason();
        CommonFunction.SaveJason(Application.dataPath + "/ClassesListJason.json", tJson);
    }

    public  void LoadJason()
    {
        DataIsReady = false;
        var jsonTextFile = Resources.Load<TextAsset>("ClassesListJason");
        Class_Save_Info_List tCSIL = JsonUtility.FromJson<Class_Save_Info_List>(jsonTextFile.text);

        classesBaseList.Clear();
        foreach(var c in tCSIL.classList)
        {
            Class_Info tclassInfo = new Class_Info();
            tclassInfo.className = c.className;
            tclassInfo.rankName = c.rankName;
            tclassInfo.baseHP = c.baseHP;
            tclassInfo.baseSkillPoint = c.baseSkillPoint;
            tclassInfo.baseArrows = c.baseArrows;
            tclassInfo.baseMP = c.baseMP;
            tclassInfo.baseThrowing = c.baseThrowing;
            tclassInfo.baseSurperSkillPoint = c.baseSurperSkillPoint;

            classesBaseList.Add(tclassInfo);
        }

        DataIsReady = true;
    }

    private void Start()
    {
        LoadJason();
    }
}