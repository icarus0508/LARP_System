using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPageScript : ClassPageScript
{




    protected  override void OnEnableThisPage()
    {

        base.OnEnableThisPage();

        RenderSkillList("Arc");
    }
}
