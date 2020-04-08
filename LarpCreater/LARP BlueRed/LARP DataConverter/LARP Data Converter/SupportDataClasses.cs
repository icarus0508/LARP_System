using System;
using System.Collections.Generic;
using System.Text;

namespace LARP_Data_Converter
{
    public class Class_Save_Info
    {
        public string className { get; set; }
        public string rankName { get; set; }
        public string baseHP { get; set; }
        public string baseSkillPoint { get; set; }
        public string baseSurperSkillPoint { get; set; }
        public string baseArrows { get; set; }
        public string baseMP { get; set; }
        public string baseThrowing { get; set; }
    }
    public class Class_Save_Info_List
    {
        public List<Class_Save_Info> classList = new List<Class_Save_Info>();

    }

    public class Skill_Save_Info
    {
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string DetailDescription { get; set; }
        public string AvaClass { get; set; }
        public string PreSkillName { get; set; }
        public string HPBuff { get; set; }
        public string MPBuff { get; set; }
        public string ArrowBuff { get; set; }
        public string ThrowBuff { get; set; }
        public string AvaRank { get; set; }
        public string MaxCost { get; set; }
        public string AvaSide { get; set; }
    }

    public class Skill_Save_Info_List
    {
        public List<Skill_Save_Info> SkillList = new List<Skill_Save_Info>();

    }
}
