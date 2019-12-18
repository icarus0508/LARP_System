using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Player_Save_Info
{
    public string Name;
    public string Photo;
    public string Rank = "C";
    public string Clasz = "W";
    public string Sided = "BI";
    public bool withHelmet = false;
    public bool withHeavyEquip = false;

    public int HP = 0;
    public int ArrowCount = 0;
    public int ThrowCount = 0;
    public int MagicCount = 0;

    public List<string> SkillNames = new List<string>();


    public string SaveToString()
    {
        return JsonUtility.ToJson(this,true);
    }
}
