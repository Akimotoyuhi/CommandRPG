using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// プレイヤーやエネミーの管理をするクラスの基底
/// </summary>
public abstract class CharactorManager : MonoBehaviour
{
    [SerializeField] protected Transform m_prefabPos;
    public virtual void Setup() { }
    /// <summary>
    /// 管理中の敵またはプレイヤーのDamage関数を
    /// </summary>
    /// <param name="command"></param>
    public abstract void GetDamage(Command command);
    /// <summary>
    /// 管理中の敵またはプレイヤーが死んだ時に呼ばれる関数
    /// </summary>
    protected abstract void OnDead();
    /// <summary>
    /// 敵またはプレイヤーの生成
    /// </summary>
    /// <param name="dataIndex"></param>
    protected abstract void Create(int dataIndex);
}
