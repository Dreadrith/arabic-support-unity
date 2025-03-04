﻿using System;
using System.ComponentModel;
using ArabicSupport;
using UnityEditor;
using UnityEngine;

public class ArabicSupportTool : EditorWindow
{
    private Vector2 scroll;
    private string intToChar;
    private char intToTextChar;
    private string textToInt;
    private int textToIntInt;
    private static readonly Int32Converter IntConverter = new Int32Converter();

    public string rawText;
    public string fixedText;
    public bool showTashkeel = true;
    public bool combineTashkeel = false;
    public bool useHinduNumbers = true;

    // Add menu item named "Arabic Support Tool" to the Tools menu
    [MenuItem("Tools/Arabic Support Tool")]
    public static void ShowWindow() => GetWindow(typeof(ArabicSupportTool));
    
    public void OnGUI()
    {
        scroll = EditorGUILayout.BeginScrollView(scroll);
        EditorGUILayout.Space();
        
        GUILayout.Label("Options:", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();
        showTashkeel = EditorGUILayout.Toggle("Use Tashkeel", showTashkeel);
        // combineTashkeel = EditorGUILayout.Toggle("Combine Tashkeel", combineTashkeel);
        useHinduNumbers = EditorGUILayout.Toggle("Use Hindu Numbers", useHinduNumbers);
        if (EditorGUI.EndChangeCheck()) FixText();
        EditorGUILayout.Space();
        using (new GUILayout.HorizontalScope())
        {
            var textStyle = new GUIStyle(GUI.skin.textArea) {wordWrap = true};
            using (new GUILayout.VerticalScope())
            {
                using (new GUILayout.HorizontalScope(GUILayout.Height(20)))
                {
                    GUILayout.Label("Input (Not Fixed)", EditorStyles.boldLabel);
                }
                EditorGUI.BeginChangeCheck();
                rawText = EditorGUILayout.TextArea(rawText, textStyle, GUILayout.ExpandHeight(true));
                if (EditorGUI.EndChangeCheck())
                    FixText();
            }

            using (new GUILayout.VerticalScope())
            {
                using (new GUILayout.HorizontalScope(GUILayout.Height(20)))
                {
                    GUILayout.Label("Output (Fixed)", EditorStyles.boldLabel);
                    if (GUILayout.Button("Copy", GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false)))
                    {
                        EditorGUIUtility.systemCopyBuffer = fixedText;
                        Debug.Log(fixedText);
                    }
                }
                EditorGUILayout.TextArea(fixedText, textStyle, GUILayout.ExpandHeight(true));
            }
        }

        EditorGUILayout.Space();
        EditorGUIUtility.labelWidth = 80;
        using (new GUILayout.HorizontalScope()) 
        {
            EditorGUI.BeginChangeCheck();
            intToChar = EditorGUILayout.TextField("Int To Char", intToChar);
            if (EditorGUI.EndChangeCheck())
            {
                try { intToTextChar = (char)(int)IntConverter.ConvertFromString(intToChar); }
                catch { intToTextChar = '0'; }
            }
            
            EditorGUILayout.TextField(intToTextChar.ToString());
        }

        using (new GUILayout.HorizontalScope())
        {
            EditorGUI.BeginChangeCheck();
            textToInt = EditorGUILayout.TextField("Char To Int", textToInt);
            if (EditorGUI.EndChangeCheck())
                textToIntInt = textToInt[0];
            EditorGUILayout.IntField(textToIntInt);
        }
        EditorGUIUtility.labelWidth = 0;
        EditorGUILayout.EndScrollView();
    }
    
    private void FixText() => fixedText = string.IsNullOrWhiteSpace(rawText) ? string.Empty : ArabicFixer.Fix(rawText, showTashkeel, combineTashkeel, useHinduNumbers);

}