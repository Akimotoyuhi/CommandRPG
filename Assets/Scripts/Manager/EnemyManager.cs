using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyManager : CharactorManager
{
    [SerializeField] Enemy m_enemyPrefab;
    /// <summary>Œ»İí“¬’†‚Ì“G‚½‚¿</summary>
    public List<Enemy> CurrentEnemys { get; private set; } = new List<Enemy>();

    public override void Setup()
    {
        Create();
        GameManager.Instance.CommandExecutor.EnemyDamageSubject.Subscribe(_ => GetDamage(_));
    }

    public override void GetDamage(Command command)
    {
        CurrentEnemys.ForEach(e => e.Damage(command));
    }

    protected override void Create()
    {
        Enemy e = Instantiate(m_enemyPrefab);
        e.transform.SetParent(m_prefabPos);
        e.SetBaseData(GameManager.Instance.EnemyData.DataBases[0]);
        e.DeadSubject
            .Subscribe(x => OnDead())
            .AddTo(this);
        CurrentEnemys.Add(e);
    }

    protected override void OnDead()
    {
    }
}
