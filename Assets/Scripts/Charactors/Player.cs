using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Charactor
{
    [SerializeField] Text m_lifeSliderText;
    [SerializeField] Slider m_magicSlider;
    [SerializeField] Text m_magicSliderText;
    private PlayerDataBase m_playerDataBase;
    private List<SkillDataBase> m_haveSkills = new List<SkillDataBase>();
    private SkillID m_currentTurnSkill;
    public List<SkillDataBase> HaveSkills => m_haveSkills;
    public SkillID CurrentTurnSkill { set => m_currentTurnSkill = value; }

    protected override void Setup()
    {
        Debug.Log("PlayerSetup");
        IsPlayer = true;
        base.Setup();
    }

    protected override void SetUI()
    {
        m_lifeSlider.maxValue = m_maxLife;
        m_lifeSlider.value = m_currentLife;
        m_lifeSliderText.text = m_currentLife.ToString();
        m_magicSlider.maxValue = m_maxMagicPoint;
        m_magicSlider.value = m_currentMagicPoint;
        m_magicSliderText.text = m_currentMagicPoint.ToString();
    }

    public void SetBaseData(PlayerDataBase playerDataBase)
    {
        SetParametor(playerDataBase.DataBase);
        playerDataBase.SkillData.HaveSkills.ForEach(s => m_haveSkills.Add(GameManager.Instance.SkillData.GetSkillData(s)));
        Setup();
    }

    public override void Action(int currentTrun)
    {
        var db = GameManager.Instance.SkillData.GetSkillData(m_currentTurnSkill);
        db.Commands.ForEach(c => c.Execute(this, 0));
    }

    public override void Damage(Command command)
    {
        base.Damage(command);
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
