using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class PlayerManager : CharactorManager
{
    [SerializeField] Player m_playerPrefab;
    /// <summary>�X�L���I����ʂőI�����ꂽ�X�L���B</summary>
    private List<SkillDataBase> m_selectSkills = new List<SkillDataBase>();
    /// <summary>�퓬�ɏo����v���C���[�̍ő吔</summary>
    private const int m_maxPlayerNum = 4;
    /// <summary>���ݐ퓬���̃v���C���[����</summary>
    public List<Player> CurrentPlayers { get; private set; } = new List<Player>();
    public List<SkillDataBase> SelectSkills { get => m_selectSkills; }

    public override void Setup()
    {
        //�Ƃ肠��������
        Create(0);
        Create(1);
    }

    /// <summary>�v���C���[�����ɑI�����ꂽ�X�L����ݒ�</summary>
    //public void SetSkills()
    //{
    //    for (int i = 0; i < CurrentPlayers.Count; i++)
    //    {
    //        CurrentPlayers[i].CurrentTurnSkill = m_selectSkills[i];
    //    }
    //    m_selectSkills.Clear();
    //}

    public override void GetDamage(Command command)
    {
        CurrentPlayers.ForEach(p => p.Damage(command));
    }

    protected override void Create(int dataIndex)
    {
        Player p = Instantiate(m_playerPrefab);
        p.transform.SetParent(m_prefabPos, false);
        p.SetBaseData(GameManager.Instance.PlayerData.DataBases[dataIndex]);
        p.Index = CurrentPlayers.Count;
        p.DeadSubject
            .Subscribe(_ => OnDead())
            .AddTo(this);
        CurrentPlayers.Add(p);
    }

    protected override void OnDead()
    {

    }
}
