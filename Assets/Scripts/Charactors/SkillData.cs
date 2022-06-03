using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] List<SkillDataBase> m_dataBases;
    public List<SkillDataBase> DataBases => m_dataBases;
    public SkillDataBase GetSkillData(SkillID skillID)
    {
        foreach (var item in m_dataBases)
        {
            if (item.Id == skillID)
                return item;
        }
        throw new System.IndexOutOfRangeException($"使用されていないIDが使用されました 渡されたID{skillID}(IDNumber{(int)skillID})");
    }
}

[System.Serializable]
public class SkillDataBase
{
    [SerializeField] string m_name;
    [SerializeField] SkillID m_id;
    [SerializeField, TextArea] string m_tooltip;
    [SerializeField] int m_consumptionMp;
    [SerializeReference, SubclassSelector] List<ISkillCommand> m_commands;
    public string Name => m_name;
    public SkillID Id => m_id;
    public string Tooltip => m_tooltip;
    //public List<Command> SkillCommand
    //{
    //    get
    //    {
    //        var ret = new List<Command>();
    //        foreach (var c in m_command)
    //        {
    //            ret.Add(c.Execute());
    //        }
    //        return ret;
    //    }
    //}
    public List<ISkillCommand> Commands => m_commands;
}
public interface ISkillCommand
{
    void Execute(Charactor charator, int index);
}
public class AttackSkill : ISkillCommand
{
    [SerializeField] SkillUseType m_useType;
    [SerializeField] AttackType m_attackType;
    [SerializeField] int m_damageCoefficient;
    public void Execute(Charactor charator, int index)
    {
        Command ret = new Command();
        if (m_useType == SkillUseType.Dependence)
            ret.UseType = charator.IsPlayer ? SkillUseType.Enemy : SkillUseType.Player;
        else
            ret.UseType = m_useType;
        ret.UseCharctorIndex = index;
        switch (m_attackType)
        {
            case AttackType.Physics:
                ret.PhysicsDamage = charator.Power * m_damageCoefficient;
                break;
            case AttackType.Magic:
                ret.MagicDamage = charator.MagicPower * m_damageCoefficient;
                break;
        }
        GameManager.Instance.CommandExecutor.CommandExecute(ret);
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
    Dependence,
    Player,
    Enemy,
    AllPlayers,
    AllEnemies,
    Field,
}
