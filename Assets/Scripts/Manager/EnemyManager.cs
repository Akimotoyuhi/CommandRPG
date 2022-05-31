using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyManager : CharactorManager
{
    [SerializeField] Enemy m_enemyPrefab;
    public List<Enemy> CurrentEnemys { get; private set; } = new List<Enemy>();

    public override void Setup()
    {
        Create();
    }

    protected override void OnDead()
    {
    }

    protected override void Create()
    {
        Enemy e = Instantiate(m_enemyPrefab);
        e.transform.SetParent(m_prefabPos);
        e.Setup(GameManager.Instance.EnemyData.DataBases[0].DataBase);
        e.DeadSubject
            .Subscribe(x => OnDead())
            .AddTo(this);
        CurrentEnemys.Add(e);
    }
}
