using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// �v���C���[��G�l�~�[�̊Ǘ�������N���X�̊��
/// </summary>
public abstract class CharactorManager : MonoBehaviour
{
    [SerializeField] protected Transform m_prefabPos;
    public virtual void Setup() { }
    public abstract void GetDamage(Command command);
    protected abstract void OnDead();
    protected abstract void Create(int dataIndex);
}
