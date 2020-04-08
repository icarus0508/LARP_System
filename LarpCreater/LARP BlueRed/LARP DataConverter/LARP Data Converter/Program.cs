using System;
using System.Data;
using ExcelDataReader;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LARP_Data_Converter
{
    class Program
    {
        static public string AppPath;
        static int Main(string[] args)
        {
            AppPath = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine("Base Directory: " + AppPath);

            LoadSkillExcelToJson();
            LoadClassExcelToJson();
            return 0;
        }

        public static void SaveJason(string path, string tjson)
        {
            string FinalPath = AppPath + "\\ExportJson\\" + path;
            Console.WriteLine("Save Jason To: " + FinalPath);
            StreamWriter sw = File.CreateText(FinalPath);          
            sw.Close();

            File.WriteAllText(FinalPath, tjson);
        }
        
        public static DataSet LoadExcel(string ExcelFilePath)
        {
            DataSet ds;

            string file = AppPath+"\\Excel\\"+ExcelFilePath;
                if (File.Exists(file))
                {
                    var extension = Path.GetExtension(file).ToLower();
                    Console.WriteLine("Load file：" + file);
                    using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        IExcelDataReader reader = null;
                        if (extension == ".xls")
                        {
                            Console.WriteLine(" => XLS");
                            reader = ExcelReaderFactory.CreateBinaryReader(stream, new ExcelReaderConfiguration()
                            {
                                FallbackEncoding = Encoding.GetEncoding("big5")
                            });
                        }
                        else if (extension == ".xlsx")
                        {
                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                            Console.WriteLine(" => XLSX");
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream, new ExcelReaderConfiguration()
                            {
                                FallbackEncoding = Encoding.GetEncoding("big5")
                            });
                        }
                        else if (extension == ".csv")
                        {
                            Console.WriteLine(" => CSV");
                            reader = ExcelReaderFactory.CreateCsvReader(stream, new ExcelReaderConfiguration()
                            {
                                FallbackEncoding = Encoding.GetEncoding("big5")
                            });
                        }
                        else if (extension == ".txt")
                        {
                            Console.WriteLine(" => Text(Tab Separated)");
                            reader = ExcelReaderFactory.CreateCsvReader(stream, new ExcelReaderConfiguration()
                            {
                                FallbackEncoding = Encoding.GetEncoding("big5"),
                                AutodetectSeparators = new char[] { '\t' }
                            });
                        }

                        if (reader == null)
                        {
                            Console.WriteLine("Unknown file extension：" + extension);
                            Console.ReadKey();
                            return null ;
                        }
                        Console.WriteLine(" => progressing");
                        using (reader)
                        {

                            ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                UseColumnDataType = false,
                                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = false
                                }
                            });


                        }
                    }

                    return ds;
                }
                else
                {
                Console.WriteLine("File "+ ExcelFilePath+" Not exist");
                return null ;
                }

            
        }
        public static void LoadSkillExcelToJson()
        {
             DataSet ds ;
            ds = LoadExcel("Skill.xlsx");
            if (ds == null) return; 

            var table = ds.Tables[0];
            Skill_Save_Info_List tSSIL = new Skill_Save_Info_List();

            for (int i = 1; i < table.Rows.Count; i++)
            {
                Skill_Save_Info tSkillInfo = new Skill_Save_Info();
                tSkillInfo.Name = table.Rows[i][0].ToString();
                tSkillInfo.ImageName = table.Rows[i][1].ToString();
                tSkillInfo.Description = table.Rows[i][2].ToString();
                tSkillInfo.DetailDescription = table.Rows[i][3].ToString();
                tSkillInfo.AvaClass = table.Rows[i][4].ToString();
                tSkillInfo.PreSkillName = table.Rows[i][5].ToString();
                tSkillInfo.HPBuff = table.Rows[i][6].ToString();
                tSkillInfo.MPBuff = table.Rows[i][7].ToString();
                tSkillInfo.ArrowBuff = table.Rows[i][8].ToString();
                tSkillInfo.ThrowBuff = table.Rows[i][9].ToString();
                tSkillInfo.AvaRank = table.Rows[i][10].ToString();
                tSkillInfo.MaxCost = table.Rows[i][11].ToString();
                tSkillInfo.AvaSide = table.Rows[i][12].ToString();
                tSSIL.SkillList.Add(tSkillInfo);
            }

            string json = JsonConvert.SerializeObject(tSSIL, Formatting.Indented);

            SaveJason("SkillListJason.json", json);
        }
        public static void LoadClassExcelToJson()
        {
            DataSet ds ;
            ds= LoadExcel("ClassesBase.xlsx");
            if (ds == null) return;

            var table = ds.Tables[0];
            Class_Save_Info_List tCSIL = new Class_Save_Info_List();

            for (int i = 1; i < table.Rows.Count; i++)
            {
                Class_Save_Info tclassInfo = new Class_Save_Info();
                tclassInfo.className = table.Rows[i][0].ToString();
                tclassInfo.rankName = table.Rows[i][1].ToString();
                tclassInfo.baseHP = table.Rows[i][2].ToString();
                tclassInfo.baseSkillPoint = table.Rows[i][3].ToString();
                tclassInfo.baseMP = table.Rows[i][4].ToString();
                tclassInfo.baseArrows = table.Rows[i][5].ToString();
                tclassInfo.baseThrowing = table.Rows[i][6].ToString();
                tclassInfo.baseSurperSkillPoint = table.Rows[i][7].ToString();
                tCSIL.classList.Add(tclassInfo);
            }

            string json = JsonConvert.SerializeObject(tCSIL, Formatting.Indented);

            SaveJason("ClassesListJason.json", json);
        }
    }
}
