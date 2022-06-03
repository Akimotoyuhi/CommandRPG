using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerManager : CharactorManager
{
    [SerializeField] Player m_playerPrefab;
    /// <summary>スキル選択画面で選択されたスキル達</summary>
    private List<SkillID> m_selectSkills = new List<SkillID>();
    /// <summary>戦闘に出せるプレイヤーの最大数</summary>
    private const int m_maxPlayerNum = 4;
    /// <summary>現在戦闘中のプレイヤーたち</summary>
    public List<Player> CurrentPlayers { get; private set; } = new List<Player>();
    public List<SkillID> SelectSkills { get => m_selectSkills; }
    /// <summary>現在のプレイヤー達が持ってるスキル</summary>
    //public List<List<SkillID>> PlayersSkill
    //{
    //    get
    //    {
    //        var ret = new List<List<SkillID>>();
    //        foreach (var p in CurrentPlayers)
    //            ret.Add(p.HaveSkills);
    //        return ret;
    //    }
    //}

    public override void Setup()
    {
        //とりあえず生成
        //for (int i = 0; i < m_maxPlayerNum; i++)
        //    Create();
        Create();
        GameManager.Instance.CommandExecutor.PlayerDamageSubject.Subscribe(_ => GetDamage(_))
            .AddTo(this);
    }

    /// <summary>プレイヤーたちに選択されたスキルを送る</summary>
    public void SetSkills()
    {
        for (int i = 0; i < CurrentPlayers.Count; i++)
        {
            CurrentPlayers[i].CurrentTurnSkill = m_selectSkills[i];
        }
    }

    public override void GetDamage(Command command)
    {
        CurrentPlayers.ForEach(p => p.Damage(command));
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
