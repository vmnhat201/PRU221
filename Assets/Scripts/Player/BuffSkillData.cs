using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BuffSkillData
{
    public float cdBuff;
    public BuffSkillStyle buffskill;
    public bool buffReady;
    public string introSpriteName;
    public string avatarSpriteName;

    public BuffSkillData(BuffSkill buffSkill)
    {
        if (buffSkill == null)
        {
            Debug.Log("BuffSkill is null");
            return;
        }
        cdBuff = buffSkill.cdBuff;
        buffskill = buffSkill.buffskill;
        buffReady = buffSkill.buffReady;
        introSpriteName = buffSkill.intro.name;
        avatarSpriteName = buffSkill.intro.name;
    }   
}