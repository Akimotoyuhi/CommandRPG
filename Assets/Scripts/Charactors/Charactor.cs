using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public abstract class Charactor : MonoBehaviour
{
    #region field
    protected int m_maxLife;
    protected int m_currentLife;
    protected int m_maxMagicPoint;
    protected int m_currentMagicPoint;
    protected int m_power;
    protected int m_magic;
    protected int m_defence;
    protected int m_magicDefence;
    private readonly AsyncSubject<Unit> m_deadSubject = new AsyncSubject<Unit>();
    #endregion
    #region property
    public IObservable<Unit> DeadSubject => m_deadSubject;
    #endregion

    public virtual void Setup()
    {

    }

    protected virtual void Dead()
    {
        m_deadSubject.OnNext(Unit.Default);
        m_deadSubject.OnCompleted();
    }
}
