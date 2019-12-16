using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Info : MonoBehaviour
{
    public string Name;
    public string Photo;
    public string Rank = "C";
    public string Clasz = "W";
    public string Sided = "BI";
    public bool withHelmet = false;
    public bool withHeavyEquip = false;
    public List<int> SkillIndexes = new List<int>();

    public int skillPointAva = 0;
    public int HP = 0;
    public int SuperPointAva = 0;
    public int ArrowCount = 0;
    public int ThrowCount = 0;
    public int MagicCount = 0;

    public int skillPointAvaMax = 0;
    public int SuperPointAvaMax = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlayerInfoFromFile()
    {

    }

}
