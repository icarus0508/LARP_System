using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPageScript : BasePageScript
{

    public GameObject StartSystemButton = null;

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
        }
    }

   private  bool CheckDataIsReady()
    {
        if (!gameObject.GetComponentInChildren<Skill_Info_Manager>(true).DataIsReady) return false;
        return true;
    }


}
