using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "New Chat Data", menuName = "ChatData")]
public class ChatData : ScriptableObject
{
    public string ID;
    public Sprite Logo;
    public LocalizedString Name, Position;
    public float typingSpeed;
    [Expandable]
    public List<DialogData> dialogData;
}
