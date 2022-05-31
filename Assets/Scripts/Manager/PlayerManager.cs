using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Player m_playerPrefab;
    [SerializeField] Transform m_playerPos;
    private const int m_playerNum = 4;
    public List<Player> CurrentPlayers { get; private set; } = new List<Player>();

    public void Setup()
    {
        for (int i = 0; i < m_playerNum; i++)
            CreatePlayer();
    }

    private void PlayerDead()
    {

    }

    private void CreatePlayer()
    {
        Player p = Instantiate(m_playerPrefab);
        p.transform.SetParent(m_playerPos);
        p.Setup();
        p.DeadSubject
            .Subscribe(x => PlayerDead())
            .AddTo(this);
        CurrentPlayers.Add(p);
    }
}
