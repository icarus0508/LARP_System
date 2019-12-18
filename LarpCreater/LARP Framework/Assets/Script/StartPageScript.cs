using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPageScript : BasePageScript
{

    public GameObject StartSystemButton = null;
    public GameObject LoadSystemButton = null;

    public GameObject TransmitPage_GO = null;

    public GameObject PlayerInfoGO = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckDataIsReady())
        {
            StartSystemButton.SetActive(true);
            LoadSystemButton.SetActive(true);
        }
    }

   private  bool CheckDataIsReady()
    {
        if (!gameObject.GetComponentInChildren<Skill_Info_Manager>(true).DataIsReady) return false;

        return true;
    }

    public void StartGame()
    {
        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).NextPage();
    }

    public void LoadCharacterInfo()
    {
        Player_Save_Info tPSI = 
        CommonFunction.LoadJason("./Assets/PlayerData/testJason.jason");

        Player_Info tPI= PlayerInfoGO.GetComponent<Player_Info>();
        tPI.LoadFromPlayerSaveInfo(tPSI);

        
    }
}
