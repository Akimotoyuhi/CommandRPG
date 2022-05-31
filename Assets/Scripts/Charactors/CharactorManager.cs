using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーやエネミーの管理をするクラスの基底
/// </summary>
public abstract class CharactorManager : MonoBehaviour
{
    [SerializeField] protected Transform m_prefabPos;
    public virtual void Setup()
    {

    }

    protected abstract void OnDead();

    protected abstract void Create();
}
