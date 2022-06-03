using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
/// <summary>
/// �v���C���[��G�l�~�[�̊Ǘ�������N���X�̊��
/// </summary>
public abstract class CharactorManager : MonoBehaviour
{
    [SerializeField] protected Transform m_prefabPos;
    public int CurrentTurn { get; set; }
    public virtual void Setup() { }
    public abstract void GetDamage(Command command);
    protected abstract void OnDead();
    protected abstract void Create();
}
