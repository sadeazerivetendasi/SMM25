using NaughtyAttributes;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Localization;
[CreateAssetMenu(fileName = "DialogData", menuName = "DialogData", order = 0)]
public class DialogData : ScriptableObject
{
    public enum ChooseCharacter
    {
        Me, You
    }
    public enum MessageType
    {
        Text, Image, Document
    }
    public ChooseCharacter chooseCharacter;
    public MessageType messageType;
    [ShowIf(nameof(IsText))]
    public LocalizedString dialogText;
    [ShowIf(nameof(IsImage))]
    public Sprite imageSprite;
    public float waitTime;
    public bool pauseDialog;
    [HideInInspector]
    public bool IsText() => messageType == MessageType.Text;
    public bool IsImage() => messageType == MessageType.Image;
    public bool IsDocument() => messageType == MessageType.Document;
}
