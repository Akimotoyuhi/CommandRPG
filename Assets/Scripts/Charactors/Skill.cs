using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private string m_name;
    private SkillID m_id;
    private List<int[]> m_skillCommands = new List<int[]>();
    public string Name => m_name;
    public SkillID ID => m_id;
    public List<int[]> SkillCommands { set => m_skillCommands = value; }

    public void Setup()
    {

    }

    public void CommnadExecutor(List<Command> commands)
    {
    }
}
