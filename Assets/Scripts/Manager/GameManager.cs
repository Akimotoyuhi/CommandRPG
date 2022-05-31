using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerData m_playerData;
    [SerializeField] BattleManager m_battleManager;
    public static GameManager Instance { get; private set; }
    public PlayerData PlayerData => m_playerData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_battleManager.Setup();
    }
}
