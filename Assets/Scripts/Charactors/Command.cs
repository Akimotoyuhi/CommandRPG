using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>誰かに何かを付与したりする時に使うスキルコマンドの情報渡し用</summary>
public struct Command
{
    /// <summary>使用対象</summary>
    public SkillUseType UseType { get; set; }
    /// <summary>このコマンドが作用する対象listのindex<br/>全体なら-1、存在しない対象なら0となる</summary>
    public int UseCharctorIndex { get; set; }
    /// <summary>与える物理ダメージ</summary>
    public int PhysicsDamage { get; set; }
    /// <summary>与える魔法ダメージ</summary>
    public int MagicDamage { get; set; }
}
