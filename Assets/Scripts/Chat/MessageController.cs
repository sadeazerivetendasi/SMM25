using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class MessageController : MonoBehaviour
{
    public GameObject typingObject, textObject;
    public TMP_Text messageText, timeText;
    LocalizedString dialogMessage;
    public void SetMessage(LocalizedString localizedString)
    {
        dialogMessage = localizedString;
        dialogMessage.StringChanged += (localizedtext) => messageText.text = localizedtext;
    }
    public LocalizedString GetDialogMessage()
    {
        return dialogMessage;
    }
    public void TypingActive()
    {
        typingObject.SetActive(true);
        textObject.SetActive(false);
    }
    public void TextObjectActive()
    {
        typingObject.SetActive(false);
        textObject.SetActive(true);
    }
}
