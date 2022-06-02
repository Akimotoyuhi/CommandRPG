using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerManager : CharactorManager
{
    [SerializeField] Player m_playerPrefab;
    private const int m_playerNum = 4;
    /// <summary>���ݐ퓬���̃v���C���[����</summary>
    public List<Player> CurrentPlayers { get; private set; } = new List<Player>();
    /// <summary>���݂̃v���C���[�B�������Ă�X�L��</summary>
    public List<List<SkillID>> PlayersSkill
    {
        get
        {
            var ret = new List<List<SkillID>>();
            foreach (var p in CurrentPlayers)
                ret.Add(p.HaveSkills);
            return ret;
        }
    }

    public override void Setup()
    {
        //�Ƃ肠��������
        for (int i = 0; i < m_playerNum; i++)
            Create();
    }

    public void PlayerTurn()
    {

    }

    private IEnumerator PlayerTurnAsync()
    {
        for (int i = 0; i < CurrentPlayers.Count; i++)
        {

        }
        yield return null;
    }

    protected override void Create()
    {
        Player p = Instantiate(m_playerPrefab);
        p.transform.SetParent(m_prefabPos);
        p.SetBaseData(GameManager.Instance.PlayerData.DataBases[0]);
        p.DeadSubject
            .Subscribe(_ => OnDead())
            .AddTo(this);
        CurrentPlayers.Add(p);
    }

    protected override void OnDead()
    {

    }
}
