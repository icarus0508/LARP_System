using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPage(ref GameObject ClosedObj, ref GameObject OpenObj)
    {
        ClosedObj.SetActive(false);
        OpenObj.SetActive(true);
    }
    public void StartPageToPhotoPage()
    {
        SwitchPage(ref StartUpPageObj,ref PhotoPageObj);
    }

    public void PhotoPageToChoosePage()
    {
        SwitchPage(ref PhotoPageObj, ref ChooseSidePageObj);
    }

    public void ChoosePageToPlayerLevelPage()
    {
        SwitchPage(ref ChooseSidePageObj, ref PlayerLevelPageObj);
    }

    public void PlayerLevelPageToPlayerClassPage()
    {
        SwitchPage(ref PlayerLevelPageObj, ref PLayerClassPageObj);
    }

    public void PlayerClassPageTOWorriorPage()
    {
        SwitchPage(ref PLayerClassPageObj, ref WorriorPageObj);
    }

    public void PlayerClassPageTOWArcherPage()
    {
        SwitchPage(ref PLayerClassPageObj, ref ArcherPageObj);
    }
    public void PlayerClassPageTOMagePage()
    {
        SwitchPage(ref PLayerClassPageObj, ref MagePageObj);
    }

    public void ArcherPageToCompletePage()
    {
        SwitchPage(ref ArcherPageObj, ref CompeletePageObj);
    }
    public void WarriorPageToCompletePage()
    {
        SwitchPage(ref WorriorPageObj, ref CompeletePageObj);
    }

    public void MagePageToCompletePage()
    {
        SwitchPage(ref MagePageObj, ref CompeletePageObj);
    }
    public GameObject StartUpPageObj;
    public GameObject PhotoPageObj;
    public GameObject ChooseSidePageObj;
    public GameObject PlayerLevelPageObj;
    public GameObject PLayerClassPageObj;
    public GameObject ArcherPageObj;
    public GameObject WorriorPageObj;
    public GameObject MagePageObj;
    public GameObject CompeletePageObj;
    public GameObject S_LevelPageObj;
}
