using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private string m_name;
    private SkillID m_id;
    private SkillDataBase m_dataBase;
    public string Name => m_name;
    public SkillID ID => m_id;
    //public SkillDataBase SkillCommands { set => m_skillCommands = value; }

    public void Setup()
    {

    }

    public void CommnadExecutor(List<Command> commands)
    {
    }
}
