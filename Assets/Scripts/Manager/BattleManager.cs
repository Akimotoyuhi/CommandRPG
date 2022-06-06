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
    private int m_playerIndex = 0;
    private bool m_skillSelectFlag = false;
    /// <summary>Player�̃N���b�N�ҋ@�N���O</summary>
    private bool m_onPlayerClickStay = false;
    /// <summary>Enemy�̃N���b�N�ҋ@�t���O</summary>
    private bool m_onEnemyClickStay = false;
    private Subject<List<SkillDataBase>> m_showSkillSubject = new Subject<List<SkillDataBase>>();
    private Subject<Command> m_playerDamageSubject = new Subject<Command>();
    private Subject<Command> m_enemyDamageSubject = new Subject<Command>();
    private ReactiveProperty<int> m_onClickCharactorIndex = new ReactiveProperty<int>();
    public IObservable<Command> PlayerDamageSubject => m_playerDamageSubject;
    public IObservable<Command> EnemyDamageSubject => m_enemyDamageSubject;
    /// <summary>�X�L����\������</summary>
    public IObservable<List<SkillDataBase>> ShowSkillSubject => m_showSkillSubject;
    //public IObservable<int> OnClickCharactorIndex => m_onClickCharactorIndex;
    public void Setup()
    {
        m_currentTurn = 0;
        m_playerManager.Setup();
        m_playerDamageSubject.Subscribe(_ => m_playerManager.GetDamage(_)).AddTo(m_playerManager);
        m_enemyManager.Setup();
        m_enemyDamageSubject.Subscribe(_ => m_enemyManager.GetDamage(_)).AddTo(m_enemyManager);
        m_playerManager.CurrentPlayers.ForEach(p =>
        {
            p.OnCharactorClickSubject
            .Where(_ => m_onPlayerClickStay)
            .Subscribe(i =>
            {
                m_onClickCharactorIndex.Value = i;
                m_onPlayerClickStay = false;
            })
            .AddTo(this);
        });
        m_enemyManager.CurrentEnemys.ForEach(e =>
        {
            e.OnCharactorClickSubject
            .Where(_ => m_onEnemyClickStay)
            .Subscribe(i =>
            {
                m_onClickCharactorIndex.Value = i;
                m_onEnemyClickStay = false;
            })
            .AddTo(this);
        });
    }

    /// <summary>�키�{�^���������ꂽ����<br/>unity�̃{�^������Ă΂�鎖��z�肵�Ă���</summary>
    public async void OnSkillSelect()
    {
        m_currentTurn++;
        await OnSkillSelectAsync();
        ActionExecute(SortCharactor());
    }

    private async UniTask OnSkillSelectAsync()
    {
        for (m_playerIndex = 0; m_playerIndex < m_playerManager.CurrentPlayers.Count; m_playerIndex++)
        {
            List<SkillDataBase> vs = new List<SkillDataBase>();
            m_playerManager.CurrentPlayers[m_playerIndex].HaveSkills.ForEach(s => vs.Add(s));
            m_showSkillSubject.OnNext(vs);
            m_skillSelectFlag = true;
            while (m_skillSelectFlag) //�X�L���I�������܂ő҂�
                await UniTask.Yield();
            Debug.Log("�ΏۑI��҂�");
            await m_onClickCharactorIndex; //�ΏۑI����҂�
            m_playerManager.CurrentPlayers[m_playerIndex].CurrentTurnSkillIndex = m_onClickCharactorIndex.Value;
            Debug.Log($"{m_playerIndex}�l�ڂ̃X�L���I������");
        }
        Debug.Log("�S���̃X�L���I������");
        //m_playerManager.SetSkills();
    }

    /// <summary>�X�L�����I�����ꂽ</summary>
    /// <param name="skillID"></param>
    public void SkillSelected(SkillDataBase dataBase)
    {
        m_playerManager.CurrentPlayers[m_playerIndex].CurrentTurnSkill = dataBase;
        Debug.Log(dataBase.UseType);
        if (dataBase.UseType == SkillUseType.Player)
            m_onPlayerClickStay = true;
        if (dataBase.UseType == SkillUseType.Enemy)
            m_onEnemyClickStay = true;
        //m_playerManager.SelectSkills.Add(dataBase);
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
