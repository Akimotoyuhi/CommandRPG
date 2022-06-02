using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BattleManager : MonoBehaviour
{
    [SerializeField] PlayerManager m_playerManager;
    [SerializeField] EnemyManager m_enemyManager;
    private bool m_skillSelectFlag = false;
    private Subject<List<SkillID>> m_showSkillSubject = new Subject<List<SkillID>>();
    public System.IObservable<List<SkillID>> ShowSkillSubject => m_showSkillSubject;
    public void Setup()
    {
        m_playerManager.Setup();
        m_enemyManager.Setup();
    }

    /// <summary>戦うボタンが押された処理<br/>unityのボタンから呼ばれる事を想定している</summary>
    public void OnSkillSelect()
    {
        StartCoroutine(OnSkillSelectAsync());
    }

    private IEnumerator OnSkillSelectAsync()
    {
        for (int i = 0; i < m_playerManager.CurrentPlayers.Count; i++)
        {
            ShowSkills(i);
            m_skillSelectFlag = true;
            while (m_skillSelectFlag) //スキル選択完了まで待つ
                yield return null;
            Debug.Log($"{i}人目のスキル選択完了");
        }
        Debug.Log("全員のスキル選択完了");
        m_showSkillSubject.OnCompleted();
        m_showSkillSubject.Dispose();
    }

    /// <summary>スキルを表示させるイベントを通知</summary>
    /// <param name="playerIndex"></param>
    private void ShowSkills(int playerIndex)
    {
        m_showSkillSubject.OnNext(m_playerManager.CurrentPlayers[playerIndex].HaveSkills);
    }

    /// <summary>スキルが選択された</summary>
    /// <param name="skillID"></param>
    public void SkillSelected(SkillID skillID)
    {
        m_playerManager.SelectSkills.Add(GameManager.Instance.SkillData.GetSkillData(skillID));
        m_skillSelectFlag = false;//スキル選択を完了する
    }
}
