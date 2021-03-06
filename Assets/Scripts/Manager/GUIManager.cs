using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

/// <summary>
/// ゲーム全体のUIの制御
/// </summary>
public class GUIManager : MonoBehaviour
{
    [Header("このクラス全体で使うもの")]
    [SerializeField] BattleManager m_battleManager;
    [SerializeField] Text m_logText;
    [Header("戦闘画面系")]
    [SerializeField] GameObject m_battlePanel;
    [SerializeField] Button m_battleButton;
    [Header("スキル関連")]
    [SerializeField] GameObject m_skillPanel;
    [SerializeField] int m_defaltSkillButtonNum = 50;
    [SerializeField] SkillButton m_skillButtonPrefab;
    [SerializeField] Transform m_buttonParent;
    /// <summary>スキルを表示するボタン</summary>
    private List<SkillButton> m_skillButtons = new List<SkillButton>();
    public static ReactiveProperty<string> ReactiveText { get; } = new ReactiveProperty<string>();
    private IObservable<string> ReactiveTextObservable => ReactiveText;

    public void Setup()
    {
        GameManager.Instance.GameStateObsarvable.Subscribe(s =>
        {
            if (s == GameState.Battle)
                m_battlePanel.SetActive(true);
            else
                m_battlePanel.SetActive(false);
        }).AddTo(this);

        //とりあえずボタン沢山生成
        for (int i = 0; i < m_defaltSkillButtonNum; i++)
        {
            SkillButton s = Instantiate(m_skillButtonPrefab);
            s.Setup();
            s.OnClickSubject
                .Subscribe(sdb =>
                {
                    //スキルボタンがクリックされた時
                    m_battleManager.SkillSelected(sdb);
                })
                .AddTo(this);
            s.transform.SetParent(m_buttonParent, false);
            m_skillButtons.Add(s);
        }

        //スキル一覧の表示
        m_battleManager.ShowSkillSubject
            .Subscribe(_ => ShowSkills(_))
            .AddTo(this);

        //バトルの状態を見てUIの制御
        m_battleManager.BattleFazeObservable.Subscribe(f =>
        {
            if (f == BattleFaze.Idle)
                m_battleButton.interactable = true;
            else
                m_battleButton.interactable = false;

            if (f == BattleFaze.SkillSelect)
                m_skillPanel.SetActive(true);
            else
                m_skillPanel.SetActive(false);
            #region 
            //switch (f)
            //{
            //    case BattleFaze.Idle:
            //        m_skillPanel.SetActive(false);
            //        m_battleButton.interactable = true;
            //        break;
            //    case BattleFaze.SkillSelect:
            //        m_skillPanel.SetActive(true);
            //        m_battleButton.interactable = false;
            //        break;
            //    case BattleFaze.TargetSelectToPlayer:
            //        m_skillPanel.SetActive(false);
            //        m_battleButton.interactable = false;
            //        break;
            //    case BattleFaze.TargetSelectToEnemy:
            //        m_skillPanel.SetActive(false);
            //        m_battleButton.interactable = false;
            //        break;
            //    default:
            //        break;
            //}
            #endregion
        }).AddTo(this);
        m_battleButton.onClick.AddListener(() => m_battleManager.SkillSelect());

        //ReactiveTextの変更を検知してlogテキストを書き換え
        ReactiveTextObservable.Subscribe(_ => m_logText.text = ReactiveText.Value).AddTo(this);

        m_skillPanel.SetActive(false);
        m_logText.text = "";
    }

    private void ShowSkills(List<SkillDataBase> skills)
    {
        for (int i = 0; i < m_skillButtons.Count; i++)
        {
            if (i < skills.Count)
                m_skillButtons[i].Setup(skills[i]);
            else
                m_skillButtons[i].Setup();
        }
    }
}
