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
    private bool m_skillSelectFlag = false;
    private Subject<List<SkillID>> m_showSkillSubject = new Subject<List<SkillID>>();
    private Subject<Command> m_playerDamageSubject = new Subject<Command>();
    private Subject<Command> m_enemyDamageSubject = new Subject<Command>();
    public IObservable<Command> PlayerDamageSubject => m_playerDamageSubject;
    public IObservable<Command> EnemyDamageSubject => m_enemyDamageSubject;
    /// <summary>スキルを表示する</summary>
    public IObservable<List<SkillID>> ShowSkillSubject => m_showSkillSubject;
    public void Setup()
    {
        m_currentTurn = 0;
        m_playerManager.Setup();
        m_playerDamageSubject.Subscribe(_ => m_playerManager.GetDamage(_)).AddTo(m_playerManager);
        m_enemyManager.Setup();
        m_enemyDamageSubject.Subscribe(_ => m_enemyManager.GetDamage(_)).AddTo(m_enemyManager);
    }

    /// <summary>戦うボタンが押された処理<br/>unityのボタンから呼ばれる事を想定している</summary>
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
        //        while (m_skillSelectFlag) //スキル選択完了まで待つ
        //            await UniTask.Yield();
        //        Debug.Log($"{i}人目のスキル選択完了");
        //    }
        //    Debug.Log("全員のスキル選択完了");
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
            while (m_skillSelectFlag) //スキル選択完了まで待つ
                await UniTask.Yield();
            Debug.Log($"{i}人目のスキル選択完了");
        }
        Debug.Log("全員のスキル選択完了");
        m_playerManager.SetSkills();
    }

    /// <summary>スキルが選択された</summary>
    /// <param name="skillID"></param>
    public void SkillSelected(SkillID skillID)
    {
        m_playerManager.SelectSkills.Add(skillID);
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
