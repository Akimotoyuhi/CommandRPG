using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

/// <summary>
/// ゲーム全体を管理するシングルトンクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerData m_playerData;
    [SerializeField] EnemyData m_enemyData;
    [SerializeField] SkillData m_skillData;
    [SerializeField] BattleManager m_battleManager;
    [SerializeField] GUIManager m_guiManager;
    private ReactiveProperty<GameState> m_gameState = new ReactiveProperty<GameState>();
    public static GameManager Instance { get; private set; }
    public PlayerData PlayerData => m_playerData;
    public EnemyData EnemyData => m_enemyData;
    public SkillData SkillData => m_skillData;
    public IObservable<GameState> GameStateObsarvable => m_gameState;

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

/// <summary>ゲーム中の状態</summary>
public enum GameState
{
    Battle,
    StageSelect,
}