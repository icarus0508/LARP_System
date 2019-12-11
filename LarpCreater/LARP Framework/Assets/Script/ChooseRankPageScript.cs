using System.Collections;
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

        if (playerInfo.Rank == "C")
        {
            RankC_Btn.GetComponent<Image>().material = DefaultMat;
        }

        if (playerInfo.Rank == "B")
        {
            RankB_Btn.GetComponent<Image>().material = DefaultMat;
        }

        if (playerInfo.Rank == "A")
        {
            RankA_Btn.GetComponent<Image>().material = DefaultMat;
        }

        if (playerInfo.Rank == "S")
        {
            RankS_Btn.GetComponent<Image>().material = DefaultMat;
        }

        if (playerInfo.Rank == "N")
        {
            RankN_Btn.GetComponent<Image>().material = DefaultMat;
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
    }

    public void OnSelectRankC()
    {
        chooseRank("C");

        RankC_Btn.GetComponent<Image>().material = DefaultMat;
        RankB_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankA_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankS_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankN_Btn.GetComponent<Image>().material = GrayScaleMat;
    }

    public void OnSelectRankB()
    {
        chooseRank("B");

        RankC_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankB_Btn.GetComponent<Image>().material = DefaultMat;
        RankA_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankS_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankN_Btn.GetComponent<Image>().material = GrayScaleMat;
    }

    public void OnSelectRankA()
    {
        chooseRank("A");

        RankC_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankB_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankA_Btn.GetComponent<Image>().material = DefaultMat;
        RankS_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankN_Btn.GetComponent<Image>().material = GrayScaleMat;
    }

    public void OnSelectRankS()
    {
        chooseRank("S");

        RankC_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankB_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankA_Btn.GetComponent<Image>().material = GrayScaleMat;
        RankS_Btn.GetComponent<Image>().material = DefaultMat;
        RankN_Btn.GetComponent<Image>().material = GrayScaleMat;
    }
}
