using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] List<SkillDataBase> m_dataBases;
    public List<SkillDataBase> DataBases => m_dataBases;
}

[System.Serializable]
public class SkillDataBase
{

}

public enum SkillID
{

}