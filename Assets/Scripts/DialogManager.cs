using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private List<ChatController> chatDialogManagers;
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
        for (int i = chatController.ActiveIndex; i < chatData.DialogData.Count; i++)
        {
            chatController.ActiveIndex = i;
            var item = chatData.DialogData[i];

            MessageController messagePrefab = item.GetChooseCharacter() == DialogData.ChooseCharacter.Me ? chatController.SetMeMessage() :
                chatController.SetYouMessage();
            #region TypeWriting
            bool typeText = messagePrefab.SetType(item);
            messagePrefab.TypingActive();
            if (typeText)
            {
                string s = "";
                foreach (var herf in messagePrefab.DialogMessage.GetLocalizedString())
                {
                    yield return new WaitForSeconds(chatData.TypingSpeed);
                    s += herf;
                }
            }
            else
            {
                yield return new WaitForSeconds(item.SendTime);
            }
            messagePrefab.TextObjectActive();
            #endregion
            if (!chatPanel.activeSelf) NotificationManager.Instance.SetChatNotification(messagePrefab.DialogMessage);
            if (item.PauseDialog)
            {
                chatController.ActiveIndex = i + 1; // növbəti yerdən davam edə bilək
                break;
            }
            yield return new WaitForSeconds(item.WaitTime);
        }
    }
    public List<ChatController> GetChatDialogManagers()
    {
        return chatDialogManagers;
    }
    public void AddChatDialogManager(ChatController chatController)
    {
        if (!chatDialogManagers.Contains(chatController))
        {
            chatDialogManagers.Add(chatController);
        }
    }
}
