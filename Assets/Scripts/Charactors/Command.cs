using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�N���ɉ�����t�^�����肷�鎞�Ɏg���X�L���R�}���h�̏��n���p</summary>
public struct Command
{
    /// <summary>�g�p�Ώ�</summary>
    public SkillUseType UseType { get; set; }
    /// <summary>���̃R�}���h����p����Ώ�list��index<br/>�S�̂Ȃ�-1�A���݂��Ȃ��ΏۂȂ�0�ƂȂ�</summary>
    public int UseCharctorIndex { get; set; }
    /// <summary>�^���镨���_���[�W</summary>
    public int PhysicsDamage { get; set; }
    /// <summary>�^���閂�@�_���[�W</summary>
    public int MagicDamage { get; set; }
}
