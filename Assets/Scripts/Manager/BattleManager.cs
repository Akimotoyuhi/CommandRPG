using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;
using Cysharp.Threading.Tasks;

/// <summary>�o�g���S�̂��Ǘ�����</summary>
public class BattleManager : MonoBehaviour
{
    [SerializeField] PlayerManager m_playerManager;
    [SerializeField] EnemyManager m_enemyManager;
    private int m_currentTurn;
    private bool m_skillSelectFlag = false;
    private Subject<List<SkillID>> m_showSkillSubject = new Subject<List<SkillID>>();
    private Subject<Command> m_playerDamageSubject = new Subject<Command>();
    private Subject<Command> m_enemyDamageSubject = new Subject<Command>();
    public IObservable<Command> PlayerDamageSubject => m_playerDamageSubject;
    public IObservable<Command> EnemyDamageSubject => m_enemyDamageSubject;
    /// <summary>�X�L����\������</summary>
    public IObservable<List<SkillID>> ShowSkillSubject => m_showSkillSubject;
    public void Setup()
    {
        m_currentTurn = 0;
        m_playerManager.Setup();
        m_playerDamageSubject.Subscribe(_ => m_playerManager.GetDamage(_)).AddTo(m_playerManager);
        m_enemyManager.Setup();
        m_enemyDamageSubject.Subscribe(_ => m_enemyManager.GetDamage(_)).AddTo(m_enemyManager);
    }

    /// <summary>�키�{�^���������ꂽ����<br/>unity�̃{�^������Ă΂�鎖��z�肵�Ă���</summary>
    public async void OnSkillSelect()
    {
        m_currentTurn++;
        await OnSkillSelectAsync();
        //UniTask.Void(async () =>
        //{
        //    for (int i = 0; i < m_playerManager.CurrentPlayers.Count; i++)
        //    {
        //        List<SkillID> vs = new List<SkillID>();
        //        m_playerManager.CurrentPlayers[i].HaveSkills.ForEach(s => vs.Add(s.Id));
        //        m_showSkillSubject.OnNext(vs);
        //        m_skillSelectFlag = true;
        //        while (m_skillSelectFlag) //�X�L���I�������܂ő҂�
        //            await UniTask.Yield();
        //        Debug.Log($"{i}�l�ڂ̃X�L���I������");
        //    }
        //    Debug.Log("�S���̃X�L���I������");
        //    m_playerManager.SetSkills();
        //});
        ActionExecute(SortCharactor());
    }

    private async UniTask OnSkillSelectAsync()
    {
        for (int i = 0; i < m_playerManager.CurrentPlayers.Count; i++)
        {
            List<SkillID> vs = new List<SkillID>();
            m_playerManager.CurrentPlayers[i].HaveSkills.ForEach(s => vs.Add(s.Id));
            m_showSkillSubject.OnNext(vs);
            m_skillSelectFlag = true;
            while (m_skillSelectFlag) //�X�L���I�������܂ő҂�
                await UniTask.Yield();
            Debug.Log($"{i}�l�ڂ̃X�L���I������");
        }
        Debug.Log("�S���̃X�L���I������");
        m_playerManager.SetSkills();
    }

    /// <summary>�X�L�����I�����ꂽ</summary>
    /// <param name="skillID"></param>
    public void SkillSelected(SkillID skillID)
    {
        m_playerManager.SelectSkills.Add(skillID);
        m_skillSelectFlag = false; //�X�L���I��������
    }

    /// <summary>�퓬��ʂ̃L�����N�^�[�B�𑪓x���ŕ��ёւ�</summary>
    /// <returns>���ёւ����list</returns>
    private List<Charactor> SortCharactor()
    {
        List<Charactor> chars = new List<Charactor>();
        m_playerManager.CurrentPlayers.ForEach(p => chars.Add(p));
        m_enemyManager.CurrentEnemys.ForEach(e => chars.Add(e));
        var ret = chars.OrderByDescending(c => c.CurrentSpeed).ToList();
        return ret;
    }

    /// <summary>�L�����N�^�[�B���s��������</summary>
    /// <param name="charactors"></param>
    private void ActionExecute(List<Charactor> charactors)
    {
        charactors.ForEach(c =>
        {
            if (!c.IsDead)
                c.Action(m_currentTurn);
        });
    }

    public void CommandExecute(Command cmd)
    {
        switch (cmd.UseType)
        {
            case SkillUseType.Player:
                m_playerDamageSubject.OnNext(cmd);
                break;
            case SkillUseType.Enemy:
                m_enemyDamageSubject.OnNext(cmd);
                break;
            case SkillUseType.AllPlayers:
                m_playerDamageSubject.OnNext(cmd);
                break;
            case SkillUseType.AllEnemies:
                m_enemyDamageSubject.OnNext(cmd);
                break;
            case SkillUseType.Field:
                m_playerDamageSubject.OnNext(cmd);
                m_enemyDamageSubject.OnNext(cmd);
                break;
        }
    }
}
