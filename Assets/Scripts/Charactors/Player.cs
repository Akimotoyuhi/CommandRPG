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
    protected override void Setup()
    {
        
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
        SetParametor(m_playerDataBase.DataBase);
        base.Setup();
    }

    public override void OnAction()
    {
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
