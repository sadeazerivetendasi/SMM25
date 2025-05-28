using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class DialogData
{
    public enum ChooseCharacter
    {
        Me, You
    }
    public ChooseCharacter chooseCharacter;
    public LocalizedString dialogText;
}

[CreateAssetMenu(fileName = "New Chat Data", menuName = "ChatData")]
public class ChatData : ScriptableObject
{
    public string ID;
    public Sprite Logo;
    public LocalizedString Name, Position;
    public List<DialogData> dialogData;
}
