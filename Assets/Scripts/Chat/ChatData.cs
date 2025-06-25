using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "New Chat Data", menuName = "ChatSystem/ChatData")]
public class ChatData : ScriptableObject
{
    [SerializeField] private string _ID;
    [SerializeField] private Sprite _Logo;
    [SerializeField] private LocalizedString _Name, _Position;
    [SerializeField] private float _typingSpeed;
    [Expandable]
    [ReorderableList]
    [SerializeField] private List<DialogData> _dialogData;
    public string ID { get => _ID; set => _ID = value; }
    public Sprite Logo { get => _Logo; set => _Logo = value; }
    public LocalizedString Name { get => _Name; set => _Name = value; }
    public LocalizedString Position { get => _Position; set => _Position = value; }
    public float TypingSpeed { get => _typingSpeed; set => _typingSpeed = value; }
    public List<DialogData> DialogData{ get => _dialogData; set => _dialogData = value; }
}
