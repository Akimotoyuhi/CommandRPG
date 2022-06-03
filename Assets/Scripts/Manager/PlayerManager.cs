using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerManager : CharactorManager
{
    [SerializeField] Player m_playerPrefab;
    /// <summary>�X�L���I����ʂőI�����ꂽ�X�L���B</summary>
    private List<SkillID> m_selectSkills = new List<SkillID>();
    /// <summary>�퓬�ɏo����v���C���[�̍ő吔</summary>
    private const int m_maxPlayerNum = 4;
    /// <summary>���ݐ퓬���̃v���C���[����</summary>
    public List<Player> CurrentPlayers { get; private set; } = new List<Player>();
    public List<SkillID> SelectSkills { get => m_selectSkills; }
    /// <summary>���݂̃v���C���[�B�������Ă�X�L��</summary>
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
        //�Ƃ肠��������
        //for (int i = 0; i < m_maxPlayerNum; i++)
        //    Create();
        Create();
        GameManager.Instance.CommandExecutor.PlayerDamageSubject.Subscribe(_ => GetDamage(_))
            .AddTo(this);
    }

    /// <summary>�v���C���[�����ɑI�����ꂽ�X�L���𑗂�</summary>
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
