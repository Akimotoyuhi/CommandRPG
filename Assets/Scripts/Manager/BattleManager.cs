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

    /// <summary>�키�{�^���������ꂽ����<br/>unity�̃{�^������Ă΂�鎖��z�肵�Ă���</summary>
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
            while (m_skillSelectFlag) //�X�L���I�������܂ő҂�
                yield return null;
            Debug.Log($"{i}�l�ڂ̃X�L���I������");
        }
        Debug.Log("�S���̃X�L���I������");
        m_showSkillSubject.OnCompleted();
        m_showSkillSubject.Dispose();
    }

    /// <summary>�X�L����\��������C�x���g��ʒm</summary>
    /// <param name="playerIndex"></param>
    private void ShowSkills(int playerIndex)
    {
        m_showSkillSubject.OnNext(m_playerManager.CurrentPlayers[playerIndex].HaveSkills);
    }

    /// <summary>�X�L�����I�����ꂽ</summary>
    /// <param name="skillID"></param>
    public void SkillSelected(SkillID skillID)
    {
        m_playerManager.SelectSkills.Add(GameManager.Instance.SkillData.GetSkillData(skillID));
        m_skillSelectFlag = false;//�X�L���I������������
    }
}
