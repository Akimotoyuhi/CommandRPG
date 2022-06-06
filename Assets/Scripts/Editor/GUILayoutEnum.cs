using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Popup
{ }

public class GUILayoutEnum : EditorWindow
{
    public Popup op;
    [MenuItem("Examples/Editor GUILayout Enum Popup usage")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(GUILayoutEnum));
        window.Show();
    }

    void OnGUI()
    {
        op = (Popup)EditorGUILayout.EnumPopup("Primitive to create:", op);
        if (GUILayout.Button("Create"))
            InstantiatePrimitive(op);
    }

    void InstantiatePrimitive(Popup op)
    {
    }
}
