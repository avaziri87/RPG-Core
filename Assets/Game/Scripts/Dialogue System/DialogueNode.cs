using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue
{
    public class DialogueNode: ScriptableObject
    {
        [SerializeField] string text;
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new Rect(0,0,200,100);
        [SerializeField] bool isPlayerSpeaking = false;

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }
        public Rect GetRect()
        {
            return rect;
        }
        public string GetText()
        {
            return text;
        }
        public List<string> GetChildren()
        {
            return children;
        }
#if UNITY_EDITOR
        public void SetPosition(Vector2 newPos)
        {
            Undo.RecordObject(this, "Dialogue Node Drag");
            rect.position = newPos;
            EditorUtility.SetDirty(this);
        }
        public void SetText(string newText)
        {
            if(newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }
        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }
        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }
        public void SetIsPlayerSpeaking( bool value)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = value;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
