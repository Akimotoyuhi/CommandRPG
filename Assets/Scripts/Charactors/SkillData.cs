using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] List<SkillDataBase> m_dataBases;
    //public List<SkillDataBase> DataBases => m_dataBases;
    public SkillDataBase GetSkillData(SkillID skillID)
    {
        foreach (var item in m_dataBases)
        {
            if (item.Id == skillID)
                return item.Copy();
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
    public int ConsumptionMp => m_consumptionMp;
    public List<ISkillCommand> Commands => m_commands;
    public SkillDataBase Copy() => (SkillDataBase)MemberwiseClone();
    public void Execute(Charactor charator, int index)
    {
        m_commands.ForEach(c => c.Execute(charator, index));
    }
}
public interface ISkillCommand
{
    void Execute(Charactor charator, int index);
    SkillUseType UseType { get; }
    SkillUseType DependenceUseType { get; set; }
}
public class AttackSkill : ISkillCommand
{
    [SerializeField] SkillUseType m_useType;
    [SerializeField] AttackType m_attackType;
    [SerializeField] float m_damageCoefficient;
    private SkillUseType m_setUseType;
    public SkillUseType UseType { get => m_useType; }
    public SkillUseType DependenceUseType { get => m_setUseType; set => m_setUseType = value; }
    public void Execute(Charactor charator, int index)
    {
        Command ret = new Command();
        if (m_useType == SkillUseType.Dependence)
            ret.UseType = m_setUseType;
        else
            ret.UseType = m_useType;
        ret.UseCharctorIndex = index;
        float f;
        switch (m_attackType)
        {
            case AttackType.Physics:
                f = charator.Power * m_damageCoefficient;
                ret.PhysicsDamage = (int)f;
                break;
            case AttackType.Magic:
                f = charator.MagicPower * m_damageCoefficient;
                ret.MagicDamage = (int)f;
                break;
        }
        GameManager.Instance.CommandExecute(ret);
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
    RenzokuKougeki,
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
