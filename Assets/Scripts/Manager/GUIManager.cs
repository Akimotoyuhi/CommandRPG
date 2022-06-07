using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class GUIManager : MonoBehaviour
{
    [Header("このクラス全体で使うもの")]
    [SerializeField] BattleManager m_battleManager;
    [SerializeField] Text m_logText;
    [Header("スキル関連")]
    [SerializeField] GameObject m_skillPanel;
    [SerializeField] int m_defaltSkillButtonNum = 50;
    [SerializeField] SkillButton m_skillButtonPrefab;
    [SerializeField] Transform m_buttonParent;
    private List<SkillButton> m_skillButtons = new List<SkillButton>();
    public static ReactiveProperty<string> ReactiveText { get; } = new ReactiveProperty<string>();
    private IObservable<string> ReactiveTextObservable => ReactiveText;

    public void Setup()
    {
        //とりあえずボタン沢山生成
        for (int i = 0; i < m_defaltSkillButtonNum; i++)
        {
            SkillButton s = Instantiate(m_skillButtonPrefab);
            s.Setup();
            s.OnClickSubject
                .Subscribe(sdb =>
                {
                    m_skillPanel.SetActive(false);
                    m_battleManager.SkillSelected(sdb);
                })
                .AddTo(this);
            s.transform.SetParent(m_buttonParent, false);
            m_skillButtons.Add(s);
        }

        m_battleManager.ShowSkillSubject
            .Subscribe(_ => ShowSkills(_))
            .AddTo(this);

        ReactiveTextObservable.Subscribe(_ => m_logText.text = ReactiveText.Value);

        m_skillPanel.SetActive(false);
        m_logText.text = "";
    }

    private void ShowSkills(List<SkillDataBase> skills)
    {
        m_skillPanel.SetActive(true);
        for (int i = 0; i < m_skillButtons.Count; i++)
        {
            if (i < skills.Count)
                m_skillButtons[i].Setup(skills[i]);
            else
                m_skillButtons[i].Setup();
        }
    }
}
