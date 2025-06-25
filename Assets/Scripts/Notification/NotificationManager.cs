using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Playables;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;
    [SerializeField] private Transform bodyNotificationContainer;
    [SerializeField] private GameObject chatNotificationPrefab, webNotificationPrefab;
    [SerializeField] private List<NotificationType> notificationTypes;
    List<NotificationType> removeNotificationTypes;
    void Awake()
    {
        Instance = this;
    }
    public List<NotificationType> GetNotificionList()
    {
        return notificationTypes;
    }
    public void AddNotificationList(NotificationType notificationType)
    {
        notificationTypes.Add(notificationType);
    }
    public void RemoveNotificationList(NotificationType notificationType)
    {
        notificationTypes.Remove(notificationType);
    }
    public void DelNotifications(string types)
    {
        if (notificationTypes.Count == 0) return;
        removeNotificationTypes = new List<NotificationType>();
        if (types == "Chat")
        {
            float sec = 0f;
            foreach (NotificationType item in notificationTypes)
            {
                if (item.GetState() == NotificationType.State.Chat) removeNotificationTypes.Add(item);
            }

            foreach (var item in removeNotificationTypes)
            {
                item.Invoke("Deactivate", sec);
                sec += 0.1f;
            }
        }
    }
    public void SetChatNotification(LocalizedString localizedString)
    {
        NotificationType notificationType = Instantiate(chatNotificationPrefab, bodyNotificationContainer).GetComponentInChildren<NotificationType>();
        notificationType.SetDialogueText(localizedString);
        notificationTypes.Add(notificationType);
    }
}
