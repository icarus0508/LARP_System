﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClassPage : ClassPageScript
{


    protected override void OnEnableThisPage()
    {

        base.OnEnableThisPage();

        RenderSkillList("War");
    }
}
