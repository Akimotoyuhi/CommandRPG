using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Enemy : Charactor
{
    private EnemyDataBase m_enemyDataBase;

    protected override void Setup()
    {
        IsPlayer = false;
        base.Setup();
        OnLifeChanged.Subscribe(_ => m_lifeSlider.value = m_reactiveLife.Value);
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
        Debug.Log($"çUåÇëŒè€ index:{m_skillUseIndex}");
        m_enemyDataBase.Action(this, m_skillUseIndex, currentTrun);
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
