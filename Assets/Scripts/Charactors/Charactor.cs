using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

public abstract class Charactor : MonoBehaviour
{
    #region field
    [SerializeField] protected Image m_image;
    [SerializeField] protected Slider m_lifeSlider;
    protected string m_name;
    protected int m_maxLife;
    protected int m_currentLife;
    protected int m_maxMagicPoint;
    protected int m_currentMagicPoint;
    protected int m_power;
    protected int m_magicPower;
    protected int m_defence;
    protected int m_magicDefence;
    protected float m_speed;
    protected int m_skillUseIndex;
    private bool m_isDead = false;
    private AsyncSubject<Unit> m_deadSubject = new AsyncSubject<Unit>();
    #endregion
    #region property
    public string Name => m_name;
    /// <summary>攻撃力<br/>バフデバフの追加後はここで評価する事</summary>
    public int Power => m_power;
    /// <summary>魔法攻撃力<br/>バフデバフの追加後はここで評価する事</summary>
    public int MagicPower => m_magicPower;
    public float CurrentSpeed => m_speed;
    public bool IsPlayer { get; protected set; }
    /// <summary>行動終了フラグ</summary>
    public bool IsActionFinished { get; set; }
    /// <summary>志望判定</summary>
    public bool IsDead => m_isDead;
    public IObservable<Unit> DeadSubject => m_deadSubject;
    #endregion

    protected virtual void Setup()
    {
        SetUI();
    }

    /// <summary>
    /// 各パラメータの設定
    /// </summary>
    /// <param name="dataBase"></param>
    protected void SetParametor(CharactorDataBase dataBase)
    {
        m_name = dataBase.Name;
        m_maxLife = dataBase.Life;
        m_currentLife = dataBase.Life;
        m_maxMagicPoint = dataBase.MagicPoint;
        m_currentMagicPoint = dataBase.MagicPoint;
        m_power = dataBase.Power;
        m_magicPower = dataBase.MagicPower;
        m_defence = dataBase.Defence;
        m_magicDefence = dataBase.MagicDefence;
        m_speed = dataBase.Speed;
        m_image.sprite = dataBase.Sprite;
    }

    /// <summary>UIの制御</summary>
    protected abstract void SetUI();

    /// <summary>敵ならcurrentTurnに基づいた行動、プレイヤーなら選択された行動をする</summary>
    /// <param name="currentTrun"></param>
    public abstract void Action(int currentTrun);

    /// <summary>被ダメージ処理</summary>
    public virtual void Damage(Command command)
    {
        Debug.Log($"{Name}が{command.PhysicsDamage}ダメージを受けた");
        if (m_currentLife <= 0)
            Dead();
    }

    /// <summary>死亡時の処理</summary>
    protected virtual void Dead()
    {
        m_isDead = true;
        m_deadSubject.OnNext(Unit.Default);
        m_deadSubject.OnCompleted();
        m_deadSubject.Dispose();
    }
}
