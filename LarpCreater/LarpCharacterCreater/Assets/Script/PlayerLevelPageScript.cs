using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelPageScript : MonoBehaviour
{

    public GameObject isWearHametToggle = null;
    public GameObject PlayerInfoGO;
    private PlayerData PlayerInfo;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerInfoGO)
        {
            PlayerInfo = PlayerInfoGO.GetComponent<PlayerData>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleWearHametCheck()
    {
        if (isWearHametToggle.GetComponent<Toggle>().isOn)
        {
            PlayerInfo.PlayerWearHamet = true;
        }
        else
        {
            PlayerInfo.PlayerWearHamet = false;
        }
    }
    public void On_C_Class_Click()
    {
        PlayerInfo.PlayerLevel = "C";
        ToggleWearHametCheck();
    }

    public void On_B_Class_Click()
    {
        PlayerInfo.PlayerLevel = "B";
        ToggleWearHametCheck();
    }

    public void On_A_Class_Click()
    {
        PlayerInfo.PlayerLevel = "A";
        ToggleWearHametCheck();
    }
}
