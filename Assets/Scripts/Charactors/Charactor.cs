using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class Charactor : MonoBehaviour, IPointerDownHandler
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
    /// <summary>�U���Ώۂ�index</summary>
    protected int m_skillUseIndex;
    private bool m_isDead = false;
    private AsyncSubject<Unit> m_deadSubject = new AsyncSubject<Unit>();
    private Subject<int> m_onCharactorClickSubject = new Subject<int>();
    protected ReactiveProperty<int> m_reactiveLife = new ReactiveProperty<int>();
    protected ReactiveProperty<int> m_reactiveMp = new ReactiveProperty<int>();
    #endregion
    #region property
    public string Name => m_name;
    /// <summary>�U����</summary>
    public int Power => m_power;
    /// <summary>���@�U����</summary>
    public int MagicPower => m_magicPower;
    /// <summary>�f����</summary>
    public float CurrentSpeed => m_speed;
    /// <summary>���g�̏���index</summary>
    public int Index { get; set; }
    /// <summary>���̃^�[���̍U���Ώۂ�index</summary>
    public int CurrentTurnSkillIndex { set => m_skillUseIndex = value; }
    public bool IsPlayer { get; protected set; }
    /// <summary>�s���I���t���O</summary>
    public bool IsActionFinished { get; set; }
    /// <summary>�u�]����</summary>
    public bool IsDead => m_isDead;
    public IObservable<Unit> DeadSubject => m_deadSubject;
    public IObservable<int> OnCharactorClickSubject => m_onCharactorClickSubject;
    protected IObservable<int> OnLifeChanged => m_reactiveLife;
    protected IObservable<int> OnMpChanged => m_reactiveMp;
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
        if (cmd.UseCharctorIndex != Index)
            return;
        int dmg;
        if (cmd.PhysicsDamage != 0)
        {
            dmg = m_defence - cmd.PhysicsDamage;
            if (dmg >= 0) //�_���[�W�͍Œ�ł��P�ʂ�悤�ɂ���
                dmg = -1;
            m_currentLife += dmg;
            Debug.Log($"{Name}��{dmg}�̕����_���[�W���󂯂�");
        }
        if (cmd.MagicDamage != 0)
        {
            dmg = m_magicDefence - cmd.MagicDamage;
            if (dmg >= 0)
                dmg = -1;
            m_currentLife += dmg;
            Debug.Log($"{Name}��{dmg}�̖��@�_���[�W���󂯂�");
        }
        if (m_currentLife <= 0)
            Dead();
    }

    /// <summary>���S���̏���</summary>
    protected virtual void Dead()
    {
        Debug.Log($"{Name}���|���ꂽ");
        m_isDead = true;
        m_deadSubject.OnNext(Unit.Default);
        m_deadSubject.OnCompleted();
        m_deadSubject.Dispose();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"{Index}���N���b�N���ꂽ");
        m_onCharactorClickSubject.OnNext(Index);
    }
}
