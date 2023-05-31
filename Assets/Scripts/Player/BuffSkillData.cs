using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BuffSkillData
{
    public float cdBuff;
    public BuffSkillStyle buffSkillStyle;
    public bool buffReady;
    public string introSpriteName;
    public string avatarSpriteName;

    public BuffSkillData(BuffSkill buffSkill)
    {
        if (buffSkill == null)
            return;
        cdBuff = buffSkill.cdBuff;
        buffSkillStyle = buffSkill.buffSkillStyle;
        buffReady = buffSkill.buffReady;
        introSpriteName = buffSkill.intro.name;
        avatarSpriteName = buffSkill.intro.name;
    }

    public void BuffSkill(BuffSkill buffSkill)
    {
        buffSkill.cdBuff = this.cdBuff;
        buffSkill.buffSkillStyle = this.buffSkillStyle;
        buffSkill.buffReady = this.buffReady;
        buffSkill.intro = Resources.Load<Sprite>(@"UI\Intro\Intro Buff\" + this.introSpriteName);
        buffSkill.avatar = Resources.Load<Sprite>(@"UI\Skill Button\" + this.avatarSpriteName);
    }
}