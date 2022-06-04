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
    /// <summary>�U����<br/>�o�t�f�o�t�̒ǉ���͂����ŕ]�����鎖</summary>
    public int Power => m_power;
    /// <summary>���@�U����<br/>�o�t�f�o�t�̒ǉ���͂����ŕ]�����鎖</summary>
    public int MagicPower => m_magicPower;
    public float CurrentSpeed => m_speed;
    public int Index { get; set; }
    public bool IsPlayer { get; protected set; }
    /// <summary>�s���I���t���O</summary>
    public bool IsActionFinished { get; set; }
    /// <summary>�u�]����</summary>
    public bool IsDead => m_isDead;
    public IObservable<Unit> DeadSubject => m_deadSubject;
    #endregion

    protected virtual void Setup()
    {
        SetUI();
    }

    /// <summary>
    /// �e�p�����[�^�̐ݒ�
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

    /// <summary>UI�̐���</summary>
    protected abstract void SetUI();

    /// <summary>�G�Ȃ�currentTurn�Ɋ�Â����s���A�v���C���[�Ȃ�I�����ꂽ�s��������</summary>
    /// <param name="currentTrun"></param>
    public abstract void Action(int currentTrun);

    /// <summary>��_���[�W����</summary>
    public virtual void Damage(Command cmd)
    {
        int dmg;
        if (cmd.PhysicsDamage != 0)
        {
            dmg = m_defence - cmd.PhysicsDamage;
            if (dmg >= 0) //�_���[�W�͍Œ�ł��P�ʂ�悤�ɂ���
                dmg = -1;
            m_currentLife += dmg;
            Debug.Log($"{Name}��{dmg}�_���[�W���󂯂�");
        }
        if (cmd.MagicDamage != 0)
        {
            dmg = m_magicDefence - cmd.MagicDamage;
            if (dmg >= 0)
                dmg = -1;
            m_currentLife += dmg;
        }
        if (m_currentLife <= 0)
            Dead();
    }

    /// <summary>���S���̏���</summary>
    protected virtual void Dead()
    {
        m_isDead = true;
        m_deadSubject.OnNext(Unit.Default);
        m_deadSubject.OnCompleted();
        m_deadSubject.Dispose();
    }
}
