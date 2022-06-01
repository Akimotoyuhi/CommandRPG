using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerManager : CharactorManager
{
    [SerializeField] Player m_playerPrefab;
    private const int m_playerNum = 4;
    public List<Player> CurrentPlayers { get; private set; } = new List<Player>();

    public override void Setup()
    {
        for (int i = 0; i < m_playerNum; i++)
            Create();
    }

    protected override void OnDead()
    {

    }


    protected override void Create()
    {
        Player p = Instantiate(m_playerPrefab);
        p.transform.SetParent(m_prefabPos);
        p.SetBaseData(GameManager.Instance.PlayerData.DataBases[0]);
        p.DeadSubject
            .Subscribe(x => OnDead())
            .AddTo(this);
        CurrentPlayers.Add(p);
    }
}
