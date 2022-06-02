using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SkillButton : MonoBehaviour
{
    [SerializeField] Button m_button;
    [SerializeField] Text m_text;
    [SerializeField] Image m_image;
    private SkillID m_skillId;
    private string m_name;
    private Subject<SkillID> m_onclickSubject = new Subject<SkillID>();
    public bool IsActive { get; set; }
    public System.IObservable<SkillID> OnClickSubject => m_onclickSubject;

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
            m_image.color = Color.white;
            m_text.text = m_name;
            m_button.enabled = true;
        }
    }

    public void OnClick()
    {
        m_onclickSubject.OnNext(m_skillId);
    }
}
