using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�N���ɉ�����t�^�����肷�鎞�Ɏg���X�L���R�}���h�̏��n���p</summary>
public struct Command
{
    public SkillUseType UseType { get; set; }
    /// <summary>���̃R�}���h����p����Ώۂ�index<br/>�S�̂Ȃ�-1�A���݂��Ȃ��ΏۂȂ�0�ƂȂ�</summary>
    public int UseCharctorIndex { get; set; }
    public int PhysicsDamage { get; set; }
    public int MagicDamage { get; set; }
}
