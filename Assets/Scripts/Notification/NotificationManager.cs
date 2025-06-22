using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;
    public Transform bodyNotificationContainer;
    public GameObject chatNotificationPrefab, webNotificationPrefab;
    public List<NotificationType> notificationTypes;
    List<NotificationType> removeNotificationTypes;
    void Awake()
    {
        Instance = this;
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
                if (item.state == NotificationType.State.Chat) removeNotificationTypes.Add(item);
            }
            
            foreach (var item in removeNotificationTypes)
            {
                item.Invoke("Deactivate", sec);
                sec += 0.1f;
            }
        }
    }
    public void DelNotification(NotificationType notificationType)
    {
    }
    public void SetChatNotification(LocalizedString localizedString)
    {
        NotificationType notificationType = Instantiate(chatNotificationPrefab, bodyNotificationContainer).GetComponentInChildren<NotificationType>();
        notificationType.SetDialogueText(localizedString);
        notificationTypes.Add(notificationType);
    }
}
