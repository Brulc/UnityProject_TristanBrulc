#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Database/Card Database")]
public class CardDatabase : ScriptableObject
{
    public List<CardInfo> allCards;
#if UNITY_EDITOR
    [ContextMenu("Fill Database")]
    public void AutoFill()
    {
        allCards = new List<CardInfo>();
        string[] guids = AssetDatabase.FindAssets("t:CardInfo");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            CardInfo card = AssetDatabase.LoadAssetAtPath<CardInfo>(path);
            allCards.Add(card);
        }

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}