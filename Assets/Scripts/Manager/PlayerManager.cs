using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Player m_playerPrefab;
    public Player CurrentPlayer { get; private set; }

    public void Setup()
    {

    }

    private void PlayerDead()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        CurrentPlayer = Instantiate(m_playerPrefab);
        CurrentPlayer.DeadSubject
            .Subscribe(x => PlayerDead())
            .AddTo(this);
    }
}
