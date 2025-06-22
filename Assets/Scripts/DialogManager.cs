using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    public List<ChatController> chatDialogManagers;
    public GameObject ChatPanel;
    void Awake()
    {
        Instance = this;
    }
    public void SetDialogue(ChatData chatData)
    {
        foreach (var item in chatDialogManagers)
        {
            if (item.GetChatData() == chatData)
            {
                StartCoroutine(StartDialogue(item));
                break;
            }
        }
    }

    private IEnumerator StartDialogue(ChatController chatController)
    {
        var chatData = chatController.GetChatData();

        for (int i = chatController.ActiveIndex; i < chatData.dialogData.Count; i++)
        {
            chatController.ActiveIndex = i;
            var item = chatData.dialogData[i];

            MessageController chatPrefab = item.chooseCharacter == DialogData.ChooseCharacter.Me ?
              Instantiate(chatController.messagePrefabMe, chatController.messageContainer).GetComponent<MessageController>() :
                Instantiate(chatController.messagePrefabYou, chatController.messageContainer).GetComponent<MessageController>();
            chatPrefab.SetMessage(item.dialogText);
            chatPrefab.TypingActive();
            string s = "";
            foreach (var herf in chatPrefab.GetDialogMessage().GetLocalizedString())
            {
                yield return new WaitForSeconds(chatData.typingSpeed);
                s += herf;
            }
            chatPrefab.TextObjectActive();
            if(!ChatPanel.activeSelf) NotificationManager.Instance.SetChatNotification(item.dialogText);
            if (item.pauseDialog)
            {
                chatController.ActiveIndex = i + 1; // növbəti yerdən davam edə bilək
                break;
            }
            yield return new WaitForSeconds(item.waitTime);
        }
    }
    private IEnumerator DialogueTypingWaitingEffect(MessageController messageController, float typingSpeed)
    {
        string s = "";
        foreach (var item in messageController.GetDialogMessage().GetLocalizedString())
        {
            yield return new WaitForSeconds(typingSpeed);
            s += item;
        }
        messageController.TextObjectActive();
    }

}
