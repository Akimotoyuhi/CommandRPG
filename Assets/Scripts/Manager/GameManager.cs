using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerData m_playerData;
    [SerializeField] EnemyData m_enemyData;
    [SerializeField] SkillData m_skillData;
    [SerializeField] BattleManager m_battleManager;
    [SerializeField] GUIManager m_guiManager;
    private CommandExecutor m_commandExecutor;
    public static GameManager Instance { get; private set; }
    public PlayerData PlayerData => m_playerData;
    public EnemyData EnemyData => m_enemyData;
    public SkillData SkillData => m_skillData;
    public CommandExecutor CommandExecutor => m_commandExecutor;

    private void Awake()
    {
        Instance = this;
        m_commandExecutor = new CommandExecutor();
    }

    private void Start()
    {
        m_battleManager.Setup();
        m_guiManager.Setup();
    }
}
