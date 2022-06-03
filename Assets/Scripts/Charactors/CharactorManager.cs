using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
/// <summary>
/// プレイヤーやエネミーの管理をするクラスの基底
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
