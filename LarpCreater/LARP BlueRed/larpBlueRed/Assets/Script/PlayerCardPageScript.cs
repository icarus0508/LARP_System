using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

using System.IO;

using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

public class PlayerCardPageScript : BasePageScript
{
    public GameObject playerIn_GO;
    private Player_Info playerInfo;

    public GameObject MainPlayerCardGO;

    public GameObject PlayerImgGO;
    public GameObject SkillObjectsGO;
    public GameObject _RankGO;
    public GameObject _ClaszGO;
    public GameObject HPGO;
    public GameObject ArrowGO;
    public GameObject MagicGO;
    public GameObject ThorwGO;
    public GameObject ClazeSpecialSkillGO;
    public GameObject NamePlateGO;
    public GameObject SideImageGO;
    public GameObject ContinueHitGO;
    public GameObject HeavyLoadGO;

    public GameObject HPBarGO;
    public GameObject PrefabsHPBarEelelment;


    public GameObject CompletedPlayerCardBtn;
    private string playerPath = "/PlayerData";

    public GameObject BackToFirstPage ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        CompletedPlayerCardBtn.SetActive(false);
        if (!playerInfo)
            if (playerIn_GO)
            {
                playerInfo = playerIn_GO.GetComponent<Player_Info>();
            }

        Initial();

        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).ForceSetNextPageBtnActive(false);
    }

    private void OnDisable()
    {
        
    }
    private bool DecideIfSkillIsLast(int SkillIndex)
    {

        return true;

    }

    private int OperatingSkill(string InString ,int targetNumber)
    {
        CommonFunction.OperatorEnm fetchOperator = CommonFunction.ExtractOperator(InString);
        int OutNumber = targetNumber;
        string OutNumberString = InString;
        switch (fetchOperator)
        {
            case CommonFunction.OperatorEnm.REPLACE:
                OutNumber = int.Parse(OutNumberString);
                break;
            case CommonFunction.OperatorEnm.ADD:
                OutNumberString=OutNumberString.Remove(0, 1);
                OutNumber += int.Parse(OutNumberString);
                break;
            case CommonFunction.OperatorEnm.SUB:
                OutNumberString = OutNumberString.Remove(0, 1);
                OutNumber -= int.Parse(OutNumberString);
                break;
            case CommonFunction.OperatorEnm.MUL:
                OutNumberString = OutNumberString.Remove(0, 1);
                OutNumber *= int.Parse(OutNumberString);
                break;
            case CommonFunction.OperatorEnm.DIV:
                OutNumberString = OutNumberString.Remove(0, 1);
                OutNumber /= int.Parse(OutNumberString);
                break;
        }

        return OutNumber;
    }
    private List<int> LateHandleSkillIndex = new List<int>();
    private void CalutlateFinialPlayerProperty()
    {
        LateHandleSkillIndex.Clear();
        foreach (var s in playerInfo.SkillIndexes)
        {
            string tempHP = Skill_Info_Manager.Skill_List[s].HPBuff;
            if (tempHP != "")
            {
              
                if (tempHP[0] == 'x' || tempHP[0] == 'X' || tempHP[0] == '/')
                {
                    LateHandleSkillIndex.Add(s);
                }
                else
                {
                    playerInfo.HP = OperatingSkill(tempHP, playerInfo.HP);
                }
            }

            string tempArrow = Skill_Info_Manager.Skill_List[s].ArrowBuff;
            //Already 無限
            if(playerInfo.ArrowCount !=-1 )
            {
                if (tempArrow != "" && tempArrow != "無限")
                {
                   
                    if (tempArrow[0] == 'x' || tempArrow[0] == 'X' || tempArrow[0] == '/')
                    {
                        LateHandleSkillIndex.Add(s);
                    }
                    else
                    {
                        playerInfo.ArrowCount = OperatingSkill(tempArrow, playerInfo.ArrowCount);
                    }
                }
                if (tempArrow == "無限")
                {
                    playerInfo.ArrowCount = -1;//Stand for infinity
                }
            }


            string tempThrow = Skill_Info_Manager.Skill_List[s].ThrowBuff;
            if (tempThrow != "")
            {
                if (tempThrow[0] == 'x' || tempThrow[0] == 'X' || tempThrow[0] == '/')
                {
                    LateHandleSkillIndex.Add(s);
                }
                else
                {
                    playerInfo.ThrowCount = OperatingSkill(tempThrow, playerInfo.ThrowCount);
                }
                
            }

            string tempMagic = Skill_Info_Manager.Skill_List[s].MPBuff;
            if(tempMagic !="")
            {
                if(tempMagic[0]=='x' || tempMagic[0]=='X' || tempMagic[0] == '/')
                {
                    LateHandleSkillIndex.Add(s);
                }
                else
                {
                    playerInfo.MagicCount = OperatingSkill(tempMagic, playerInfo.MagicCount);
                }
                
            }

        }

        //Late handle
        foreach(var s in LateHandleSkillIndex)
        {
            string tempMagic = Skill_Info_Manager.Skill_List[s].MPBuff;
            if(tempMagic != "")
            {
                playerInfo.MagicCount = OperatingSkill(tempMagic, playerInfo.MagicCount);
            }
        }

        if(playerInfo.withHeavyEquip)
        {
            playerInfo.HP += 1;
        }
    }
    public void InitialHPBar()
    {
        int teSizX = -124;
        for (int i=0;i<playerInfo.HP;i++)
        {
            GameObject tGO = Instantiate(PrefabsHPBarEelelment, new Vector2(teSizX,0), Quaternion.identity);
            tGO.transform.SetParent(HPBarGO.transform, false);

            teSizX += 24;
        }

        if(playerInfo.Sided!="NT")
        {
            GameObject tSpecitlGO = Instantiate(PrefabsHPBarEelelment, new Vector2(teSizX, 0), Quaternion.identity);
            tSpecitlGO.transform.SetParent(HPBarGO.transform, false);
            tSpecitlGO.transform.GetChild(0).gameObject.SetActive(false);
            tSpecitlGO.transform.GetChild(1).gameObject.SetActive(true);
        }

        

    }
    public void InitalPlayerBasePropertySetup()
    {
        Class_Info tCI = CommonFunction.GetProperBClassBasic(playerInfo.Clasz, playerInfo.Rank);
        if(tCI!=null)
        {
            playerInfo.HP = int.Parse( tCI.baseHP);
            playerInfo.ArrowCount = int.Parse(tCI.baseArrows);
            playerInfo.MagicCount = int.Parse(tCI.baseMP);
            playerInfo.ThrowCount = int.Parse(tCI.baseThrowing);
           
        }
    }
    public void InitPlayerSkillAndPropoertyGrid()
    {
        CalutlateFinialPlayerProperty();

        HPGO.transform.GetChild(0).GetComponent<Text>().text = playerInfo.HP.ToString();
        if (playerInfo.Sided != "NT")
        {
            HPGO.transform.GetChild(0).GetComponent<Text>().text += "+1";
        }

        ThorwGO.transform.GetChild(0).GetComponent<Text>().text = playerInfo.ThrowCount.ToString();

        if(playerInfo.ArrowCount == -1) //無限 
        {
            ArrowGO.transform.GetChild(0).GetComponent<Text>().text = "無限";
        }
        else
        {
            ArrowGO.transform.GetChild(0).GetComponent<Text>().text = playerInfo.ArrowCount.ToString();
        }
        
        MagicGO.transform.GetChild(0).GetComponent<Text>().text = playerInfo.MagicCount.ToString();
    }
    public void Initial()
    {
        for (int i = 0; i < SkillObjectsGO.transform.childCount; i++)
        {
            SkillObjectsGO.transform.GetChild(i).GetComponent<Image>().sprite = null;
            SkillObjectsGO.transform.GetChild(i).GetComponentInChildren<Text>().text = "";
            SkillObjectsGO.transform.GetChild(i).gameObject.SetActive(false);
        }

        int SkillCount = 0;
        for (int i = 0; i < playerInfo.SkillIndexes.Count; i++)
        {
            if (DecideIfSkillIsLast(playerInfo.SkillIndexes[i]))
            {
                SkillObjectsGO.transform.GetChild(SkillCount).GetComponent<Image>().sprite = Skill_Info_Manager.Skill_List[playerInfo.SkillIndexes[i]].Image;
                SkillObjectsGO.transform.GetChild(SkillCount).GetComponentInChildren<Text>().text = Skill_Info_Manager.Skill_List[playerInfo.SkillIndexes[i]].Name;
                SkillObjectsGO.transform.GetChild(SkillCount).gameObject.SetActive(true);
                SkillCount++;
            }
        }

        if (playerInfo.Clasz == "W")
        {
            _ClaszGO.GetComponent<Image>().sprite = Skill_Info_Manager.W_ClaszImg;
            ClazeSpecialSkillGO.GetComponent<Image>().sprite = Skill_Info_Manager.W_ClaszSkillImg;
            ClazeSpecialSkillGO.SetActive(true);
        }

        if (playerInfo.Clasz == "A")
        {
            _ClaszGO.GetComponent<Image>().sprite = Skill_Info_Manager.A_ClaszImg;
            ClazeSpecialSkillGO.GetComponent<Image>().sprite = Skill_Info_Manager.A_ClaszSkillImg;
            ClazeSpecialSkillGO.SetActive(true);
        }

        if (playerInfo.Clasz == "M")
        {
            _ClaszGO.GetComponent<Image>().sprite = Skill_Info_Manager.M_ClaszImg;
            ClazeSpecialSkillGO.GetComponent<Image>().sprite = Skill_Info_Manager.M_ClaszSkillImg;
            ClazeSpecialSkillGO.SetActive(true);
        }

        if (playerInfo.Clasz == "V")
        {
            _ClaszGO.GetComponent<Image>().sprite = Skill_Info_Manager.N_ClaszImg;
            ClazeSpecialSkillGO.SetActive(false);
        }

        if (playerInfo.Rank == "S")
        {
            _RankGO.GetComponent<Image>().sprite = Skill_Info_Manager.S_RankImg;
        }
        if (playerInfo.Rank == "A")
        {
            _RankGO.GetComponent<Image>().sprite = Skill_Info_Manager.A_RankImg;
        }
        if (playerInfo.Rank == "B")
        {
            _RankGO.GetComponent<Image>().sprite = Skill_Info_Manager.B_RankImg;
        }
        if (playerInfo.Rank == "C")
        {
            _RankGO.GetComponent<Image>().sprite = Skill_Info_Manager.C_RankImg;
        }
        if (playerInfo.Rank == "N")
        {
            _RankGO.GetComponent<Image>().sprite = Skill_Info_Manager.N_RankImg;
        }

        if (playerInfo.Sided == "BL")
        {
            SideImageGO.GetComponent<Image>().sprite = Skill_Info_Manager.Side_BIImg;
        }

        if (playerInfo.Sided == "AD")
        {
            SideImageGO.GetComponent<Image>().sprite = Skill_Info_Manager.Side_ADImg;
        }

        if (playerInfo.Sided == "JD")
        {
            SideImageGO.GetComponent<Image>().sprite = Skill_Info_Manager.Side_JDImg;
        }

        if (playerInfo.Sided == "DN")
        {
            SideImageGO.GetComponent<Image>().sprite = Skill_Info_Manager.Side_DNImg;
        }

        if (playerInfo.Sided == "NT")
        {
            SideImageGO.SetActive(false);
        }

        InitalPlayerBasePropertySetup();
        InitPlayerSkillAndPropoertyGrid();

        if (playerInfo.withHelmet)
        {
            ContinueHitGO.SetActive(true);
        }
        else
        {
            ContinueHitGO.SetActive(false);
        }

        if(playerInfo.withHeavyEquip)
        {
            HeavyLoadGO.SetActive(true);
        }
        else
        {
            HeavyLoadGO.SetActive(false);
        }

        PlayerImgGO.GetComponent<Image>().sprite = playerInfo.PlayerPhoto;

        NamePlateGO.GetComponent<Text>().text = playerInfo.Name;

        InitialHPBar();
    }

    private void ExportPlayerCard()
    {


        var temTx = CommonFunction.TextureToTexture2D(MainPlayerCardGO.GetComponent<RawImage>().texture);
        CommonFunction.SaveImg(Application.dataPath + playerPath + "/" + playerInfo.Name + playerInfo.SeriesNumber + ".png", temTx);

        playerInfo.Photo = playerPath + "/PlayerMainPic/" + playerInfo.Name+ playerInfo.SeriesNumber + ".png";
        CommonFunction.SaveImg(Application.dataPath + playerInfo.Photo, playerInfo.PlayerPhotoOri.texture);
    }
    private void ExportPlayerInfo()
    {

        Player_Save_Info tPSI = new Player_Save_Info();
        tPSI.Name = playerInfo.Name;
        tPSI.Photo = playerInfo.Photo;
        tPSI.Rank = playerInfo.Rank;
        tPSI.Clasz = playerInfo.Clasz;
        tPSI.Sided = playerInfo.Sided;
        tPSI.withHelmet = playerInfo.withHelmet;
        tPSI.withHeavyEquip = playerInfo.withHeavyEquip;
        tPSI.HP = playerInfo.HP;
        tPSI.ArrowCount = playerInfo.ArrowCount;
        tPSI.ThrowCount = playerInfo.ThrowCount;
        tPSI.MagicCount = playerInfo.MagicCount;
        tPSI.PlayerImgScalellValue = playerInfo.PlayerImgScalellValue;
        tPSI.PlayerImgPosition = playerInfo.PlayerImgPosition;
        tPSI.SeriesNumber = playerInfo.SeriesNumber;

        foreach(var s in playerInfo.SkillIndexes)
        {
           string tS= Skill_Info_Manager.Skill_List[s].Name;
            tPSI.SkillNames.Add(tS);
        }

        string tJason = tPSI.SaveToString();


        CommonFunction.SaveJason(Application.dataPath + playerPath +"/"+ playerInfo.Name+ playerInfo.SeriesNumber + ".jason", tJason);
    }
    public void OnExoprtData()
    {
        if (playerInfo.SeriesNumber =="")
        {
            playerInfo.SeriesNumber =
                                  "_" + 
                                  //System.DateTime.Now.Year.ToString() +
                                  //System.DateTime.Now.Month.ToString() +
                                  //System.DateTime.Now.Day.ToString() +
                                  System.DateTime.Now.Hour.ToString() +
                                  System.DateTime.Now.Minute.ToString() +
                                  System.DateTime.Now.Second.ToString();
            
        }
        ExportPlayerCard();
        ExportPlayerInfo();
        SendEmailToServer();

        CompletedPlayerCardBtn.SetActive(true);
    }

    public void SendEmailToServer()
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("icarus0508@hotmail.com");
        mail.To.Add("icarus0508@hotmail.com");
        mail.Subject = "TestMail_Subject";
        mail.Body = "This is for Testing SMTP mail";

        Attachment JsonData = new Attachment(Application.dataPath + playerPath + "/" + playerInfo.Name+ playerInfo.SeriesNumber + ".jason");
        mail.Attachments.Add(JsonData);

        Attachment UserFacePngData = new Attachment(Application.dataPath + playerInfo.Photo);
        mail.Attachments.Add(UserFacePngData);

        Attachment PlayerCardPngData = new Attachment(Application.dataPath + playerPath + "/" + playerInfo.Name+ playerInfo.SeriesNumber + ".png");
        mail.Attachments.Add(PlayerCardPngData);


        SmtpClient smtpClient = new SmtpClient("smtp.live.com");
        smtpClient.Port = 587;
        smtpClient.Credentials = new System.Net.NetworkCredential("icarus0508@hotmail.com", "s0300211s0300211") as ICredentialsByHost;
        smtpClient.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

        smtpClient.Send(mail);
        
    }

    public void OnClickCompletedPlayerCardBtn()
    {
        CompletedPlayerCardBtn.SetActive(false);
    }

    public void OnClickBackToTopPageBtn()
    {
        this.NextPage = BackToFirstPage;
        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).NextPage();
    }
}
