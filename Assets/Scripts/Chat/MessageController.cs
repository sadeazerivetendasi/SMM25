using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using DG.Tweening;
using UnityEngine.UI;
using UnityEditor;

public class MessageController : MonoBehaviour
{
    [SerializeField] private GameObject typingObject, textObject;

    [Header("Text")]
    [SerializeField] private TMP_Text messageText, timeText;

    [Header("Link")]
    [SerializeField] private TMP_Text linkText;

    [Header("Doc")]
    [SerializeField] private ChatDocDownloader docObject;

    [Header("Photo")]
    [SerializeField] private Image photoImage;

    private LocalizedString _dialogMessage;
    public LocalizedString DialogMessage{ get => _dialogMessage; set => _dialogMessage = value; }
    private void Start()
    {
        messageText.alpha = 0;
        timeText.alpha = 0;
    }
    public bool SetType(DialogData dialogData)
    {
        var messageType = dialogData.GetMessageType();
        switch (messageType)
        {
            case DialogData.MessageType.Text:
                messageText.gameObject.SetActive(true);
                _dialogMessage = dialogData.GetDialog();
                _dialogMessage.StringChanged += SetMessageText;
                return true;
            case DialogData.MessageType.Image:
                photoImage.gameObject.SetActive(true);
                photoImage.sprite = dialogData.GetPhotoImage();
                return false;
            case DialogData.MessageType.Document:
                docObject.gameObject.SetActive(true);
                _dialogMessage = dialogData.GetDocumentFileName();
                docObject.GetComponent<ChatDocDownloader>().SetFileName(_dialogMessage);
                return false;
            case DialogData.MessageType.Link:
                linkText.gameObject.SetActive(true);
                _dialogMessage = dialogData.GetDialogLink();
                _dialogMessage.StringChanged += SetLinkText;
                return true;
            default:
                return false;
        }
    }
    private void SetMessageText(string localizedtext)
    {
        messageText.text = localizedtext;
    }
    private void SetLinkText(string localizedtext)
    {
        linkText.text = localizedtext;
    }
    public void TypingActive()
    {
        typingObject.SetActive(true);
        textObject.SetActive(false);
    }
    public void TextObjectActive()
    {
        messageText.DOFade(1f,1f);
        timeText.DOFade(1f, 1f);
        typingObject.SetActive(false);
        textObject.SetActive(true);
    }
    void OnDestroy()
    {
        _dialogMessage.StringChanged -= SetMessageText;
    }
}
