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
    /// <summary>
    /// 敵AIを評価して行動させる
    /// </summary>
    /// <param name="charactor">評価対象のクラス</param>
    /// <param name="index">対象のindex</param>
    /// <exception cref="System.Exception">行動が無い場合にスローされる例外</exception>
    public void Action(Charactor charactor, int index, int currentTurn)
    {
        foreach (var ai in m_aiList)
        {
            if (ai.ActionExecute(charactor, index, currentTurn))
                return;
        }
        throw new System.Exception("敵行動データが見つかりませんでした");
    }
}
[System.Serializable]
public class EnemyAction
{
    [SerializeField] List<EnemyAI> m_aiList;
    [SerializeField] List<SkillID> m_skillList;
    public bool ActionExecute(Charactor charactor, int index, int currentTurn)
    {
        foreach (var ai in m_aiList)
        {
            //AI評価　一つでもfalseなら失敗
            if (!ai.Evaluation(charactor))
                return false;
        }
        //成功したらその下にあるスキルを全て実行
        m_skillList.ForEach(skill => GameManager.Instance.SkillData.GetSkillData(skill).Execute(charactor, index));
        return true;
    }
}
[System.Serializable]
public class EnemyAI
{
    [SerializeField] EvaluationParametorType m_evaluationParametorType;
    [SerializeField] EvaluationType m_evaluationType;
    [SerializeField] int value;

    public bool Evaluation(Charactor charactor)
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