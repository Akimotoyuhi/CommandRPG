using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;
using Cysharp.Threading.Tasks;

/// <summary>バトル全体を管理する</summary>
public class BattleManager : MonoBehaviour
{
    [SerializeField] PlayerManager m_playerManager;
    [SerializeField] EnemyManager m_enemyManager;
    private int m_currentTurn;
    private int m_playerIndex = 0;
    private bool m_skillSelectFlag = false;
    /// <summary>Playerのクリック待機クラグ</summary>
    private bool m_onPlayerClickStay = false;
    /// <summary>Enemyのクリック待機フラグ</summary>
    private bool m_onEnemyClickStay = false;
    private Subject<List<SkillDataBase>> m_showSkillSubject = new Subject<List<SkillDataBase>>();
    private Subject<Command> m_playerDamageSubject = new Subject<Command>();
    private Subject<Command> m_enemyDamageSubject = new Subject<Command>();
    private ReactiveProperty<int> m_onClickCharactorIndex = new ReactiveProperty<int>();
    public IObservable<Command> PlayerDamageSubject => m_playerDamageSubject;
    public IObservable<Command> EnemyDamageSubject => m_enemyDamageSubject;
    /// <summary>スキルを表示する</summary>
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

    /// <summary>戦うボタンが押された処理<br/>unityのボタンから呼ばれる事を想定している</summary>
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
            while (m_skillSelectFlag) //スキル選択完了まで待つ
                await UniTask.Yield();
            Debug.Log("対象選択待ち");
            await m_onClickCharactorIndex; //対象選択を待つ
            m_playerManager.CurrentPlayers[m_playerIndex].CurrentTurnSkillIndex = m_onClickCharactorIndex.Value;
            Debug.Log($"{m_playerIndex}人目のスキル選択完了");
        }
        Debug.Log("全員のスキル選択完了");
        //m_playerManager.SetSkills();
    }

    /// <summary>スキルが選択された</summary>
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
        m_skillSelectFlag = false; //スキル選択を完了
    }

    /// <summary>戦闘画面のキャラクター達を測度順で並び替え</summary>
    /// <returns>並び替え後のlist</returns>
    private List<Charactor> SortCharactor()
    {
        List<Charactor> chars = new List<Charactor>();
        m_playerManager.CurrentPlayers.ForEach(p => chars.Add(p));
        m_enemyManager.CurrentEnemys.ForEach(e => chars.Add(e));
        var ret = chars.OrderByDescending(c => c.CurrentSpeed).ToList();
        return ret;
    }

    /// <summary>キャラクター達を行動させる</summary>
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
