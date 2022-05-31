using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Charactor
{
    [SerializeField] Text m_lifeSliderText;
    [SerializeField] Slider m_magicSlider;
    [SerializeField] Text m_magicSliderText;
    public override void Setup(CharactorDataBase dataBase)
    {
        base.Setup(dataBase);
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

    protected override void Dead()
    {
        base.Dead();
    }
}
