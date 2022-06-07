using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Player : Charactor
{
    [SerializeField] Text m_lifeSliderText;
    [SerializeField] Slider m_magicSlider;
    [SerializeField] Text m_magicSliderText;
    private PlayerDataBase m_playerDataBase;
    private List<SkillDataBase> m_haveSkills = new List<SkillDataBase>();
    /// <summary>このターン使用予定のスキル</summary>
    private SkillDataBase m_currentTurnSkill;
    public List<SkillDataBase> HaveSkills => m_haveSkills;
    public SkillDataBase CurrentTurnSkill { set => m_currentTurnSkill = value; }

    protected override void Setup()
    {
        IsPlayer = true;
        base.Setup();
        //OnLifeChanged.Subscribe(_ =>
        //{
        //    m_lifeSlider.value = m_reactiveLife.Value;
        //    m_lifeSliderText.text = m_reactiveLife.Value.ToString();
        //});
        //OnMpChanged.Subscribe(_ =>
        //{
        //    m_magicSlider.value = m_reactiveMp.Value;
        //    m_magicSliderText.text = m_reactiveMp.Value.ToString();
        //});
        SetUI();
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
        playerDataBase.SkillData.HaveSkills.ForEach(s =>
        {
            SkillDataBase db = GameManager.Instance.SkillData.GetSkillData(s);
            db.Commands.ForEach(c =>
            {
                if (c.UseType == SkillUseType.Dependence)
                c.DependenceUseType = SkillUseType.Enemy;
            });
            m_haveSkills.Add(db);
        });
        Setup();
    }

    public override void Action(int currentTrun)
    {
        Debug.Log($"{Name}が{m_currentTurnSkill.Name}を敵index{m_skillUseIndex}に実行");
        m_currentTurnSkill.Execute(this, m_skillUseIndex);
        //GameManager.Instance.SkillData.GetSkillData(m_currentTurnSkill).Execute(this, 0);
    }

    public override void Damage(Command cmd)
    {
        if (cmd.UseType == SkillUseType.Player)
        {
            if (cmd.UseCharctorIndex != Index)
                return;
        }
        base.Damage(cmd);
        SetUI();
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
