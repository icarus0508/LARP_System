using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Info : MonoBehaviour
{
    public string Name;
    public string Photo;
    public Sprite PlayerPhoto;
    public Sprite PlayerPhotoOri;
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

    //Recore Player Img Transform
    public Vector3 PlayerImgPosition;
    public Vector3 PlayerImgScalellValue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadFromPlayerSaveInfo(Player_Save_Info psi)
    {
        SkillIndexes.Clear();

        Name = psi.Name;
        Photo = psi.Photo;
        Rank = psi.Rank;
        Clasz = psi.Clasz;
        Sided = psi.Sided;
        withHeavyEquip = psi.withHeavyEquip;
        withHelmet = psi.withHelmet;
        HP = psi.HP;
        ArrowCount = psi.ArrowCount;
        ThrowCount = psi.ThrowCount;
        MagicCount = psi.MagicCount;
        PlayerImgScalellValue = psi.PlayerImgScalellValue;
        PlayerImgPosition = psi.PlayerImgPosition;

        foreach (var s in psi.SkillNames)
        {
            for(int i=0;i<Skill_Info_Manager.Skill_List.Count;i++)
            {
                if(s == Skill_Info_Manager.Skill_List[i].Name)
                {
                    SkillIndexes.Add(i);
                }
            }
        }
    }
}

