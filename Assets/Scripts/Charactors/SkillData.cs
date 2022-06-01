using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] List<SkillDataBase> m_dataBases;
    public List<SkillDataBase> DataBases => m_dataBases;
}

[System.Serializable]
public class SkillDataBase
{
    [SerializeField] string m_name;
    [SerializeField] SkillID m_id;
    [SerializeReference, SubclassSelector] List<ISkillCommand> m_command;
    public string Name => m_name;
    public List<Command> SkillCommand
    {
        get
        {
            var ret = new List<Command>();
            foreach (var c in m_command)
            {
                ret.Add(c.Execute());
            }
            return ret;
        }
    }
}
public interface ISkillCommand
{
    Command Execute();
}
public class AttackSkill : ISkillCommand
{
    [SerializeField] SkillUseType m_useType;
    [SerializeField] AttackType m_attackType;
    [SerializeField] int m_damageCoefficient;
    public Command Execute()
    {
        Command ret = new Command();
        ret.SkillUseType = m_useType;
        ret.AttackType = m_attackType;
        ret.DamageCoefficient = m_damageCoefficient;
        return ret;
    }
}
//public class BuffDebuffSkill : ISkillCommand
//{
//    [SerializeField] SkillUseType m_useType;

//    public int[] Execute()
//    {
//        return new int[] { };
//    }
//}

public enum SkillID
{
    NormalAttack,
    NormalMagic,
}
public enum AttackType
{
    Physics,
    Magic,
}
public enum SkillCommandType
{
    AttackSkill,
    BuffDebuffSkill,
}
public enum SkillUseType
{
    Fellow,
    Enemy,
    AllFellows,
    AllEnemies,
    Field,
}
