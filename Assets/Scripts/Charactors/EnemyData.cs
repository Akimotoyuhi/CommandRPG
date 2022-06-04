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
    /// �GAI��]�����čs��������
    /// </summary>
    /// <param name="charactor">�]���Ώۂ̃N���X</param>
    /// <param name="index">�Ώۂ�index</param>
    /// <exception cref="System.Exception">�s���������ꍇ�ɃX���[������O</exception>
    public void Action(Charactor charactor, int index, int currentTurn)
    {
        foreach (var ai in m_aiList)
        {
            if (ai.ActionExecute(charactor, index, currentTurn))
                return;
        }
        throw new System.Exception("�G�s���f�[�^��������܂���ł���");
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
            //AI�]���@��ł�false�Ȃ玸�s
            if (!ai.Evaluation(charactor))
                return false;
        }
        //���������炻�̉��ɂ���X�L����S�Ď��s
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
/// <summary>�����]���̑ΏۂƂȂ�p�����[�^</summary>
public enum EvaluationParametorType
{
    Turn,
    PlayerLife,
}
/// <summary>EvaluationParametorType�ɑ΂�������^�C�v</summary>
public enum EvaluationType
{
    Any,
    High,
    Low,
    Maltiple,
}