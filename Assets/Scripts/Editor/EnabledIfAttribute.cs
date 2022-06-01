using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// enum�ɉ����ăt�B�[���h�̕\����Ԃ�؂�ւ���Attribute<br/>
/// https://light11.hatenadiary.com/entry/2019/03/24/013917
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class EnabledIfAttribute : PropertyAttribute
{
    /// <summary>
    /// ��\����Ԃ̕\�����@
    /// </summary>
    public enum HideMode
    {
        Invisible, // �����Ȃ�����
        Disabled, // ��A�N�e�B�u�ɂ���
    }

    /// <summary>
    /// �����Ƃ��Ďw�肷��t�B�[���h�̌^
    /// </summary>
    public enum SwitcherType
    {
        Bool,
        Enum,
    }

    public SwitcherType switcherType;
    public HideMode hideMode;
    public string switcherFieldName;
    public int enableIfValueIs;

    public EnabledIfAttribute(string switcherFieldName, bool enableIfValueIs, HideMode hideMode = HideMode.Disabled)
        : this(SwitcherType.Bool, switcherFieldName, enableIfValueIs ? 1 : 0, hideMode)
    {
    }

    public EnabledIfAttribute(string switcherFieldName, int enableIfValueIs, HideMode hideMode = HideMode.Disabled)
        : this(SwitcherType.Enum, switcherFieldName, enableIfValueIs, hideMode)
    {
    }

    private EnabledIfAttribute(SwitcherType switcherType, string switcherFieldName, int enableIfValueIs, HideMode hideMode)
    {
        this.switcherType = switcherType;
        this.hideMode = hideMode;
        this.switcherFieldName = switcherFieldName;
        this.enableIfValueIs = enableIfValueIs;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EnabledIfAttribute))]
public class EnabledIfAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = attribute as EnabledIfAttribute;
        var isEnabled = GetIsEnabled(attr, property);

        // ��\������
        if (attr.hideMode == EnabledIfAttribute.HideMode.Disabled)
        {
            GUI.enabled &= isEnabled;
        }

        // �v���p�e�B��`��
        if (GetIsVisible(attr, property))
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        // GUI.enabled��߂�
        if (attr.hideMode == EnabledIfAttribute.HideMode.Disabled)
        {
            GUI.enabled = true;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var attr = attribute as EnabledIfAttribute;
        return GetIsVisible(attr, property) ? EditorGUI.GetPropertyHeight(property) : -2; // �\������Ă��Ȃ��ꍇ�X�y�[�X���l�߂邽��-2��Ԃ�
    }

    /// <summary>
    /// �\������邩
    /// ��A�N�e�B�u�ł��\������Ă����true��Ԃ�
    /// </summary>
    private bool GetIsVisible(EnabledIfAttribute attribute, SerializedProperty property)
    {
        if (GetIsEnabled(attribute, property))
        {
            return true;
        }
        if (attribute.hideMode != EnabledIfAttribute.HideMode.Invisible)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// �t�B�[���h���L����
    /// </summary>
    private bool GetIsEnabled(EnabledIfAttribute attribute, SerializedProperty property)
    {
        return attribute.enableIfValueIs == GetSwitcherPropertyValue(attribute, property);
    }

    private int GetSwitcherPropertyValue(EnabledIfAttribute attribute, SerializedProperty property)
    {
        var propertyNameIndex = property.propertyPath.LastIndexOf(property.name, StringComparison.Ordinal);
        var switcherPropertyName = property.propertyPath.Substring(0, propertyNameIndex) + attribute.switcherFieldName;
        var switcherProperty = property.serializedObject.FindProperty(switcherPropertyName);
        switch (switcherProperty.propertyType)
        {
            case SerializedPropertyType.Boolean:
                return switcherProperty.boolValue ? 1 : 0;
            case SerializedPropertyType.Enum:
                return switcherProperty.intValue;
            default:
                throw new Exception("unsupported type.");
        }
    }
#endif
}