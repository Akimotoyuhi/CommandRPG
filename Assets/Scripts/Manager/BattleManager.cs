using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] PlayerManager m_playerManager;
    [SerializeField] EnemyManager m_enemyManager;
    public void Setup()
    {
        m_playerManager.Setup();
        m_enemyManager.Setup();
    }
}
