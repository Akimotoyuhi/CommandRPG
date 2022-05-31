using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charactor
{
    public override void Setup(CharactorDataBase dataBase)
    {
        base.Setup(dataBase);
    }

    protected override void SetUI()
    {
        m_lifeSlider.maxValue = m_maxLife;
        m_lifeSlider.value = m_currentLife;
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
