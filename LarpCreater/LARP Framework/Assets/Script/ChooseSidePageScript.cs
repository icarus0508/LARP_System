using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSidePageScript : BasePageScript
{
    public GameObject playerIn_GO;
    private Player_Info playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        if (playerIn_GO)
        {
            playerInfo = playerIn_GO.GetComponent<Player_Info>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitialPage()
    {
        playerInfo.Sided = "";
    }
    private void OnEnable()
    {
        InitialPage();
    }

    private void OnDisable()
    {
        
    }

    private void ChooseSided(string side)
    {
        playerInfo.Sided = side;
    }

    public void ChooseBlueIron()
    {
        ChooseSided("BI");
    }

    public void ChooseAxeDragon()
    {
        ChooseSided("AD");
    }

    public void ChooseJadeDragon()
    {
        ChooseSided("JD");
    }

    public void ChooseDeamon()
    {
        ChooseSided("DN");
    }

    public void ChooseNoTeam()
    {
        ChooseSided("NT");
    }
}
