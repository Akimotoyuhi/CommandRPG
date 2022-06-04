using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button m_button;
    [SerializeField] Text m_text;
    [SerializeField] Image m_image;
    private SkillID m_skillId;
    private string m_name;
    private string m_tooltip;
    private SkillUseType m_useType;
    private Subject<List<int>> m_onclickSubject = new Subject<List<int>>();
    private Subject<string> m_pointerSubject = new Subject<string>();
    public bool IsActive { get; set; }
    public System.IObservable<string> PointerSubject => m_pointerSubject;
    public System.IObservable<List<int>> OnClickSubject => m_onclickSubject;

    public void Setup(SkillDataBase skillDataBase = null)
    {
        if (skillDataBase == null)
        {
            m_button.enabled = false;
            m_image.color = Color.clear;
            m_text.text = "";
        }
        else
        {
            m_skillId = skillDataBase.Id;
            m_name = skillDataBase.Name;
            m_tooltip = skillDataBase.Tooltip;
            m_image.color = Color.white;
            m_useType = skillDataBase.UseType;
            m_text.text = m_name;
            m_button.enabled = true;
        }
    }

    public void OnClick()
    {
        m_onclickSubject.OnNext(new List<int> { (int)m_skillId, (int)m_useType });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_pointerSubject.OnNext(m_tooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_pointerSubject.OnNext("");
    }
}

public enum SkillButtonIndex
{
    SkillID,
    UseType,
}