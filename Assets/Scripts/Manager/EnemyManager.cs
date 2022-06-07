using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class EnemyManager : CharactorManager
{
    [SerializeField] Enemy m_enemyPrefab;
    /// <summary>Œ»İí“¬’†‚Ì“G‚½‚¿</summary>
    public List<Enemy> CurrentEnemys { get; private set; } = new List<Enemy>();

    public override void Setup()
    {
        Create(0);
        //GameManager.Instance.CommandExecutor.EnemyDamageSubject.Subscribe(_ => GetDamage(_));
    }

    public override void GetDamage(Command command)
    {
        CurrentEnemys.ForEach(e => e.Damage(command));
    }

    protected override void Create(int dataIndex)
    {
        Enemy e = Instantiate(m_enemyPrefab);
        e.transform.SetParent(m_prefabPos, false);
        e.SetBaseData(GameManager.Instance.EnemyData.DataBases[dataIndex]);
        e.Index = 0;
        e.DeadSubject
            .Subscribe(x => OnDead())
            .AddTo(this);
        CurrentEnemys.Add(e);
    }

    protected override void OnDead()
    {
    }
}
