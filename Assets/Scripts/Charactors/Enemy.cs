using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charactor
{
    protected override void Setup()
    {
        base.Setup();
    }

    protected override void SetUI()
    {
        m_lifeSlider.maxValue = m_maxLife;
        m_lifeSlider.value = m_currentLife;
    }

    public void SetBaseData(EnemyDataBase enemyDataBase)
    {
        SetParametor(enemyDataBase.DataBase);
        SetUI();
    }

    public override void OnAction()
    {
        Debug.LogError("ìGçsìÆñ¢é¿ëï");
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
