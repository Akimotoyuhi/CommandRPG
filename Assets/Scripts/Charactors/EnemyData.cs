using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] List<EnemyDataBase> m_dataBases;
    public List<EnemyDataBase> DataBases => m_dataBases;
}

[System.Serializable]
public class EnemyDataBase
{
    [SerializeField] CharactorDataBase m_dataBase;
    [SerializeField] List<EnemyAction> m_aiList;
    public CharactorDataBase DataBase => m_dataBase;
    public List<EnemyAction> AIList => m_aiList;
}
[System.Serializable]
public class EnemyAction
{
    [SerializeField] List<EnemyAI> m_aiList;
    [SerializeField] List<SkillID> m_skillList;
    public List<EnemyAI> EnemyAiList => m_aiList;
    public List<SkillID> SkillList => m_skillList;
}
[System.Serializable]
public class EnemyAI
{
    [SerializeField] EvaluationParametorType m_evaluationParametorType;
    [SerializeField] EvaluationType m_evaluationType;
    [SerializeField] int value;

    public bool Evaluation()
    {
        if (m_evaluationType == EvaluationType.Any)
            return true;
        else
            return false;
    }
}

public enum EnemyID
{
    Enemy,
}
/// <summary>条件評価の対象となるパラメータ</summary>
public enum EvaluationParametorType
{
    Turn,
    PlayerLife,
}
/// <summary>EvaluationParametorTypeに対する条件タイプ</summary>
public enum EvaluationType
{
    Any,
    High,
    Low,
    Maltiple,
}