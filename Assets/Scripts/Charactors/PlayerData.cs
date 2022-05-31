using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] List<CharactorDataBase> m_data;
    public List<CharactorDataBase> Data => m_data;
}

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
