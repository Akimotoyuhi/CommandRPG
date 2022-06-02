using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BattleManager : MonoBehaviour
{
    [SerializeField] PlayerManager m_playerManager;
    [SerializeField] EnemyManager m_enemyManager;
    private readonly AsyncSubject<List<SkillID>> m_showSkillSubject = new AsyncSubject<List<SkillID>>();
    public System.IObservable<List<SkillID>> ShowSkillSubject => m_showSkillSubject;
    public void Setup()
    {
        m_playerManager.Setup();
        m_enemyManager.Setup();
        //OnBattle();
    }

    public void OnBattle()
    {
        ShowSkills(0);
    }

    private void ShowSkills(int playerIndex)
    {
        m_showSkillSubject.OnNext(m_playerManager.CurrentPlayers[playerIndex].HaveSkills);
        m_showSkillSubject.OnCompleted();
    }
}
