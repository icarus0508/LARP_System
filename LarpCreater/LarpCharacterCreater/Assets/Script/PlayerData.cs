using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public class SkillInfo
    {
        public string SkillName;
        public Sprite SkillImage;
    }

    public string PlayerName;
    public string PlayerDescription;

    public string PlayerGroup;
    public string PlayerGroupDescriotion;
    public GroupDataInfo.GroupIndex PlayerGroupIndex;

    public Sprite PlayerImageSprite;
    public string PlayerImageName;

    public string PlayerLevel;

    public bool PlayerWearHamet;

    public string PlayerClass;
  
    public List<SkillInfo> PlayerSkillList = new List<SkillInfo>();

    public int HP = 0;
    public int ArrowCount = 0;
    public int MPCount = 0;
    public int ThrowCount = 0;

    public int MultiplyMpCount = 1;

    //Helper member
    public int SkillPointLeft = 3;
    public int SkillPointMax = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
