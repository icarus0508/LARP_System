using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSidePageScript : MonoBehaviour
{

    public GameObject PlayerInfoGO;
    private PlayerData PlayerInfo;

    public GameObject LoaderGO;
    private GroupDataInfo GroupData;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerInfoGO)
        {
            PlayerInfo = PlayerInfoGO.GetComponent<PlayerData>();
        }

        if(LoaderGO)
        {
            GroupData = LoaderGO.GetComponent<GroupDataInfo>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickEast()
    {
      if(PlayerInfo &&GroupData)
        {
            PlayerInfo.PlayerGroup = GroupData.GourdList[(int)GroupDataInfo.GroupIndex.EastGroup].GroupName;
            PlayerInfo.PlayerGroupDescriotion = GroupData.GourdList[(int)GroupDataInfo.GroupIndex.EastGroup].GroupDescription;
            PlayerInfo.PlayerGroupIndex = GroupDataInfo.GroupIndex.EastGroup;
        }
    }

    public void OnClickWest()
    {
        if (PlayerInfo && GroupData)
        {
            PlayerInfo.PlayerGroup = GroupData.GourdList[(int)GroupDataInfo.GroupIndex.WestGroup].GroupName;
            PlayerInfo.PlayerGroupDescriotion = GroupData.GourdList[(int)GroupDataInfo.GroupIndex.WestGroup].GroupDescription;
            PlayerInfo.PlayerGroupIndex = GroupDataInfo.GroupIndex.WestGroup;
        }
    }

    public void OnClickNorn()
    {
        if (PlayerInfo && GroupData)
        {
            PlayerInfo.PlayerGroup = GroupData.GourdList[(int)GroupDataInfo.GroupIndex.NorGroup].GroupName;
            PlayerInfo.PlayerGroupDescriotion = GroupData.GourdList[(int)GroupDataInfo.GroupIndex.NorGroup].GroupDescription;
            PlayerInfo.PlayerGroupIndex = GroupDataInfo.GroupIndex.NorGroup;
        }
    }

    public void OnClickSourth()
    {
        if (PlayerInfo && GroupData)
        {
            PlayerInfo.PlayerGroup = GroupData.GourdList[(int)GroupDataInfo.GroupIndex.SouGroup].GroupName;
            PlayerInfo.PlayerGroupDescriotion = GroupData.GourdList[(int)GroupDataInfo.GroupIndex.SouGroup].GroupDescription;
            PlayerInfo.PlayerGroupIndex = GroupDataInfo.GroupIndex.SouGroup;
        }
    }

    public void OnClickNon()
    {
        if (PlayerInfo && GroupData)
        {
            PlayerInfo.PlayerGroup = GroupData.GourdList[(int)GroupDataInfo.GroupIndex.FreeGroup].GroupName;
            PlayerInfo.PlayerGroupDescriotion = GroupData.GourdList[(int)GroupDataInfo.GroupIndex.FreeGroup].GroupDescription;
            PlayerInfo.PlayerGroupIndex = GroupDataInfo.GroupIndex.FreeGroup;
        }
    }
}
