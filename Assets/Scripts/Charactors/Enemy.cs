using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charactor
{
    private EnemyDataBase m_enemyDataBase;

    protected override void Setup()
    {
        IsPlayer = false;
        base.Setup();
    }

    protected override void SetUI()
    {
        m_lifeSlider.maxValue = m_maxLife;
        m_lifeSlider.value = m_currentLife;
    }

    public void SetBaseData(EnemyDataBase enemyDataBase)
    {
        m_enemyDataBase = enemyDataBase;
        SetParametor(enemyDataBase.DataBase);
        SetUI();
    }

    public override void Action(int currentTrun)
    {
        m_enemyDataBase.Action(this, 0, currentTrun);
    }

    public override void Damage(Command command)
    {
        base.Damage(command);
        SetUI();
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
