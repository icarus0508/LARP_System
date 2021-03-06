﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class ChooseRankPageScript : BasePageScript
{

    public GameObject playerIn_GO;
    private Player_Info playerInfo;

    public GameObject RankC_Btn = null;
    public GameObject RankB_Btn = null;
    public GameObject RankA_Btn = null;
    public GameObject RankS_Btn = null;
    public GameObject RankN_Btn = null;

    public Material DefaultMat = null;
    public Material GrayScaleMat = null;

    public GameObject NextPageForFigher = null;
    public GameObject NextPageForVillager = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void InitialPage()
    {
        RankC_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankB_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankA_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankS_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankN_Btn.GetComponent<Image>().material = GrayScaleMat;

        if(playerInfo.Clasz == "W")
        {

            RankC_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.War_RankC_BtnImg;
            RankB_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.War_RankB_BtnImg;
            RankA_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.War_RankA_BtnImg;
            RankS_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.War_RankS_BtnImg;
        }

        if (playerInfo.Clasz == "A")
        {

            RankC_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.Arc_RankC_BtnImg;
            RankB_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.Arc_RankB_BtnImg;
            RankA_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.Arc_RankA_BtnImg;
            RankS_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.Arc_RankS_BtnImg;
        }

        if (playerInfo.Clasz == "M")
        {

            RankC_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.Mag_RankC_BtnImg;
            RankB_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.Mag_RankB_BtnImg;
            RankA_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.Mag_RankA_BtnImg;
            RankS_Btn.GetComponent<Image>().sprite = Skill_Info_Manager.Mag_RankS_BtnImg;
        }


        if (playerInfo.Rank == "C")
        {
            RankC_Btn.GetComponent<Image>().material = DefaultMat;
            OnSelectRankC();
        }

        if (playerInfo.Rank == "B")
        {
            RankB_Btn.GetComponent<Image>().material = DefaultMat;
            OnSelectRankB();
        }

        if (playerInfo.Rank == "A")
        {
            RankA_Btn.GetComponent<Image>().material = DefaultMat;
            OnSelectRankA();
        }

        if (playerInfo.Rank == "S")
        {
            RankS_Btn.GetComponent<Image>().material = DefaultMat;
            OnSelectRankS();
        }

        if (playerInfo.Rank == "N")
        {
            RankN_Btn.GetComponent<Image>().material = DefaultMat;
            OnSelectRankN();
        }


    }

    private void OnEnable()
    {
        if (!playerInfo)
            if (playerIn_GO)
            {
                playerInfo = playerIn_GO.GetComponent<Player_Info>();
            }

        InitialPage();
        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).ForceSetNextPageBtnActive(false);
    }

    private void OnDisable()
    {
        
    }

    private void chooseRank(string rank)
    {
        playerInfo.Rank = rank;
    }

    public void OnSelectRankN()
    {
        chooseRank("N");

        RankC_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankB_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankA_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankS_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankN_Btn.GetComponent<Image>().material = DefaultMat;

        playerInfo.skillPointAva = 6;
        playerInfo.HP = 1;
        playerInfo.SuperPointAva = 0;
        playerInfo.skillPointAvaMax = playerInfo.skillPointAva;
        playerInfo.SuperPointAvaMax = playerInfo.SuperPointAva;

        playerInfo.Clasz = "V";
    }

    public void OnSelectRankC()
    {
        chooseRank("C");

        RankC_Btn.GetComponent<Image>().material = DefaultMat;
        RankB_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankA_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankS_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankN_Btn.GetComponent<Image>().material = GrayScaleMat;

        //playerInfo.skillPointAva = 1;
        //playerInfo.HP = 2;
        //playerInfo.SuperPointAva = 0;
        //playerInfo.skillPointAvaMax = playerInfo.skillPointAva;
        //playerInfo.SuperPointAvaMax = playerInfo.SuperPointAva;
    }

    public void OnSelectRankB()
    {
        chooseRank("B");

        RankC_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankB_Btn.GetComponent<Image>().material = DefaultMat;
        RankA_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankS_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankN_Btn.GetComponent<Image>().material = GrayScaleMat;

        //playerInfo.skillPointAva = 2;
        //playerInfo.HP = 3;
        //playerInfo.SuperPointAva = 0;
        //playerInfo.skillPointAvaMax = playerInfo.skillPointAva;
        //playerInfo.SuperPointAvaMax = playerInfo.SuperPointAva;
    }

    public void OnSelectRankA()
    {
        chooseRank("A");

        RankC_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankB_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankA_Btn.GetComponent<Image>().material = DefaultMat;
        RankS_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankN_Btn.GetComponent<Image>().material = GrayScaleMat;

        //playerInfo.skillPointAva = 3;
        //playerInfo.HP = 5;
        //playerInfo.SuperPointAva = 0;
        //playerInfo.skillPointAvaMax = playerInfo.skillPointAva;
        //playerInfo.SuperPointAvaMax = playerInfo.SuperPointAva;
    }

    public void OnSelectRankS()
    {
        chooseRank("S");

        RankC_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankB_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankA_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankS_Btn.GetComponent<Image>().material = DefaultMat;
        RankN_Btn.GetComponent<Image>().material = GrayScaleMat;

        //playerInfo.skillPointAva = 3;
        //playerInfo.HP = 7;
        //playerInfo.SuperPointAva = 1;
        //playerInfo.skillPointAvaMax = playerInfo.skillPointAva;
        //playerInfo.SuperPointAvaMax = playerInfo.SuperPointAva;
    }

    public void OnClickNextPage()
    {
        if (playerInfo.Rank != "N")
        {
            this.NextPage = NextPageForFigher;

        }
        else
        {
            this.NextPage = NextPageForVillager;
        }

        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).NextPage();
    }
}
