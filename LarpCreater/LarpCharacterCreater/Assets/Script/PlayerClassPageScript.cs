using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassPageScript : MonoBehaviour
{
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

    public void OnClickWorrior()
    {
        if (PlayerInfo) PlayerInfo.PlayerClass = "War";
    }

    public void OnClickArcher()
    {
        if (PlayerInfo) PlayerInfo.PlayerClass = "Arc";
    }

    public void OnClickMage()
    {
        if (PlayerInfo) PlayerInfo.PlayerClass = "Mag";
    }

    
}
