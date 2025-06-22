using UnityEngine;
using System.Collections.Generic;
using System;

public class ChatManager : MonoBehaviour
{
    public static ChatManager Instance;

    [Header("User Panel Settings")]
    [SerializeField] private Transform userButtonParent;
    [SerializeField] private GameObject userButtonPrefab;
    [Header("User Message Panel Settings")]
    [SerializeField] private Transform userMessagePageParent;
    [SerializeField] private GameObject userMessagePagePrefab;

    [Header("Message Panels")]
    ChatController activeChatDialogManager;


    public List<GameObject> pageList;

    private PersonChatData currentSelected;

    private void Awake()
    {
        Instance = this;
        ChangePage("FirstPage");
    }
    private void ChangePage(string pageName)
    {
        foreach (GameObject item in pageList)
        {
            item.SetActive(item.name == pageName);
        }
    }
    public void CreateAndConnectUser(ChatData chatData)
    {
        PersonChatData pd = SpawnAndConnectUser(chatData);
        SelectUser(pd);
    }
    PersonChatData SpawnAndConnectUser(ChatData chatData)
    {
        PersonChatData pd = Instantiate(userButtonPrefab, userButtonParent).GetComponent<PersonChatData>();
        pd.name = chatData.ID;
        pd.Initialize(chatData);
        ChatController userMessagePage = Instantiate(userMessagePagePrefab, userMessagePageParent).GetComponent<ChatController>();
        userMessagePage.SetChatData(chatData);
        userMessagePage.name = chatData.ID;
        DialogManager.Instance.chatDialogManagers.Add(userMessagePage);
        pd.messagePanel = userMessagePage.gameObject;
        pd.messagePanel.SetActive(false);
        return pd;
    }
    public void SelectUser(PersonChatData user)
    {
        if (currentSelected == user)
        {
            currentSelected.Deactivate();
            currentSelected = null;
            ChangePage("FirstPage");
        }
        else
        {
            if (currentSelected != null) currentSelected.Deactivate();
            ChangePage("MessagesPage");
            currentSelected = user;
            currentSelected.Activate();
        }
    }
    void OnEnable()
    {
        if(NotificationManager.Instance != null)
            NotificationManager.Instance.DelNotifications("Chat");
    }
}
