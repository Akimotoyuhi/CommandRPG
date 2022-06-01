using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] List<PlayerDataBase> m_dataBases;
    public List<PlayerDataBase> DataBases => m_dataBases;
}

/// <summary>
/// 各キャラクターの基本ステータス
/// </summary>
[System.Serializable]
public class CharactorDataBase
{
    [SerializeField] string m_name;
    [SerializeField] Sprite m_sprite;
    [SerializeField] int m_life;
    [SerializeField] int m_magicPoint;
    [SerializeField] int m_power;
    [SerializeField] int m_magicPower;
    [SerializeField] int m_defence;
    [SerializeField] int m_magicDefence;
    [SerializeField] float m_speed;
    public string Name => m_name;
    public Sprite Sprite => m_sprite;
    public int Life => m_life;
    public int MagicPoint => m_magicPoint;
    public int Power => m_power;
    public int MagicPower => m_magicPower;
    public int Defence => m_defence;
    public int MagicDefence => m_magicDefence;
    public float Speed => m_speed;
}
[System.Serializable]
public class PlayerDataBase
{
    [SerializeField] CharactorDataBase m_dataBase;
    [SerializeField] PlayerSkillData m_skillData;
    public CharactorDataBase DataBase => m_dataBase;
    public PlayerSkillData SkillData => m_skillData;
}
[System.Serializable]
public class PlayerSkillData
{
    [SerializeField] List<SkillID> m_haveSkills;
    public List<SkillID> HaveSkills => m_haveSkills;
}