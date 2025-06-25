using NaughtyAttributes;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Localization;
[CreateAssetMenu(fileName = "New Dialog Data", menuName = "ChatSystem/DialogData", order = 0)]
public class DialogData : ScriptableObject
{
    public enum ChooseCharacter
    {
        Me, You
    }
    public enum MessageType
    {
        Text, Image, Document, Link
    }
    [SerializeField] private ChooseCharacter chooseCharacter;
    [SerializeField] private MessageType messageType;

    [ShowIf(nameof(IsText))]
    [SerializeField] private LocalizedString dialogText;

    [ShowIf(nameof(IsImage))]
    [SerializeField] private Sprite photoSprite;

    [ShowIf(nameof(IsDocument))]
    [SerializeField] private LocalizedString documentFileName;

    [ShowIf(nameof(IsLink))]
    [SerializeField] private LocalizedString dialogLinkText;

    [ShowIf(EConditionOperator.Or,nameof(IsLink),nameof(IsDocument))]
    [SerializeField] private float sendTime;
    
    [SerializeField] private float waitTime;
    [SerializeField] private bool pauseDialog;

    [HideInInspector]
    public bool IsText() => messageType == MessageType.Text;
    public bool IsImage() => messageType == MessageType.Image;
    public bool IsDocument() => messageType == MessageType.Document;
    public bool IsLink() => messageType == MessageType.Link;
    public float WaitTime { get => waitTime; set => waitTime = value; }
    public float SendTime { get => sendTime; set => sendTime = value; }
    public bool PauseDialog { get => pauseDialog; set => pauseDialog = value; }
    public ChooseCharacter GetChooseCharacter()
    {
        return chooseCharacter;
    }
    public MessageType GetMessageType()
    {
        return messageType;
    }
    public LocalizedString GetDocumentFileName()
    {
        return documentFileName;
    }
    public LocalizedString GetDialog()
    {
        return dialogText;
    }
    public LocalizedString GetDialogLink()
    {
        return dialogLinkText;
    }
    public Sprite GetPhotoImage()
    {
        return photoSprite;
    }
}
