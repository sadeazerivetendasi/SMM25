using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    [SerializeField] private Transform messageContainer;
    [SerializeField] private GameObject messagePrefabMe, messagePrefabYou;
    private ChatData chatData;
    private int _activeIndex;
    public int ActiveIndex
    {
        get
        {
            return _activeIndex;
        }
        set
        {
            _activeIndex = value;
        }
    }
    public ChatData GetChatData()
    {
        return chatData;
    }
    public void SetChatData(ChatData _chatData)
    {
        this.name = _chatData.ID;
        chatData = _chatData;
    }
    public MessageController SetMeMessage()
    {
        return Instantiate(messagePrefabMe, messageContainer).GetComponent<MessageController>();
    }
    public MessageController SetYouMessage()
    {
        return Instantiate(messagePrefabYou, messageContainer).GetComponent<MessageController>();
    }
}
