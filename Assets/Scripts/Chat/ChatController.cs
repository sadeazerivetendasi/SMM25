using System;
using System.Collections;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    public Transform messageContainer;
    public GameObject messagePrefabMe, messagePrefabYou;
    [SerializeField] private ChatData chatData;
    [SerializeField] private int _activeIndex;
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
        chatData = _chatData;
    }
}
