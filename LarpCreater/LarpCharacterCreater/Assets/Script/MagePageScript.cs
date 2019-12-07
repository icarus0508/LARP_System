using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagePageScript : ClassPageScript
{
    protected override void OnEnableThisPage()
    {

        base.OnEnableThisPage();

        RenderSkillList("Mag");
    }
}
