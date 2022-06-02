using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject m_skillPanel;
    [SerializeField] int m_defaltSkillButtonNum = 55;
    [SerializeField] BattleManager m_battleManager;
    [SerializeField] SkillButton m_skillButtonPrefab;
    [SerializeField] Transform m_buttonParent;
    private List<SkillButton> m_skillButtons = new List<SkillButton>();

    public void Setup()
    {
        //Ç∆ÇËÇ†Ç¶Ç∏É{É^ÉìëÚéRê∂ê¨
        for (int i = 0; i < m_defaltSkillButtonNum; i++)
        {
            SkillButton s = Instantiate(m_skillButtonPrefab);
            s.Setup();
            s.OnClickSubject
                .Subscribe(_ => Debug.Log(_))
                .AddTo(this);
            s.transform.SetParent(m_buttonParent);
            m_skillButtons.Add(s);
        }
        m_battleManager.ShowSkillSubject
            .Subscribe(_ => ShowSkills(_))
            .AddTo(this);
        m_skillPanel.SetActive(false);
    }

    public void ShowSkills(List<SkillID> skills)
    {
        m_skillPanel.SetActive(true);
        for (int i = 0; i < m_skillButtons.Count; i++)
        {
            if (i < skills.Count)
                m_skillButtons[i].Setup(GameManager.Instance.SkillData.DataBases[(int)skills[i]]);
            else
                m_skillButtons[i].Setup();
        }
    }
}
