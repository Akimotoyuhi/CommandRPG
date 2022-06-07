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
    /// <summary>
    /// �Ǘ����̓G�܂��̓v���C���[��Damage�֐���
    /// </summary>
    /// <param name="command"></param>
    public abstract void GetDamage(Command command);
    /// <summary>
    /// �Ǘ����̓G�܂��̓v���C���[�����񂾎��ɌĂ΂��֐�
    /// </summary>
    protected abstract void OnDead();
    /// <summary>
    /// �G�܂��̓v���C���[�̐���
    /// </summary>
    /// <param name="dataIndex"></param>
    protected abstract void Create(int dataIndex);
}
