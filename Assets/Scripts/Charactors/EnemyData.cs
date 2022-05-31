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
    [SerializeField] List<EnemyAI> m_aiList;
    public CharactorDataBase DataBase => m_dataBase;
    public List<EnemyAI> AIList => m_aiList;
}

[System.Serializable]
public class EnemyAI
{
    [SerializeField] EvaluationParametorType m_evaluationParametorType;
    [SerializeField] EvaluationType m_evaluationType;
    [SerializeField] int value;

    public bool Evaluation()
    {
        return false;
    }
}

public enum EnemyID
{

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
    High,
    Low,
    Maltiple,
}