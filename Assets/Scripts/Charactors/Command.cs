using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>誰かに何かを付与したりする時に使うスキルコマンドの情報渡し用</summary>
public struct Command
{
    public SkillUseType UseType { get; set; }
    /// <summary>このコマンドが作用する対象のindex<br/>全体なら-1、存在しない対象なら0となる</summary>
    public int UseCharctorIndex { get; set; }
    public int PhysicsDamage { get; set; }
    public int MagicDamage { get; set; }
}
