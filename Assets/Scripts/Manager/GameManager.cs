using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerData m_playerData;
    [SerializeField] EnemyData m_enemyData;
    [SerializeField] SkillData m_skillData;
    [SerializeField] BattleManager m_battleManager;
    [SerializeField] GUIManager m_guiManager;
    public static GameManager Instance { get; private set; }
    public PlayerData PlayerData => m_playerData;
    public EnemyData EnemyData => m_enemyData;
    public SkillData SkillData => m_skillData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_battleManager.Setup();
        m_guiManager.Setup();
    }

    public void CommandExecute(Command cmd)
    {
        m_battleManager.CommandExecute(cmd);
    }
}
