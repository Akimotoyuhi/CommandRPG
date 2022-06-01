using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Command
{
    public int DamageCoefficient { get; set; }
    public SkillUseType SkillUseType { get; set; }
    public AttackType AttackType { get; set; }
}
