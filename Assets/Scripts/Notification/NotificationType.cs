using Flexalon;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using UnityEngine.Localization;

public class NotificationType : MonoBehaviour
{
    public enum State
    {
        Chat, Search
    }
    public State state;
    [ShowIf(nameof(IsChat))]
    public TMP_Text chatPersonNameText, chatMessageText;
    LocalizedString dialogText;
    FlexalonObject flexalonObject;
    FlexalonObject parentFlexalonObject;
    Button button;
    private bool IsChat() => state == State.Chat;
    void Awake()
    {
        flexalonObject = GetComponent<FlexalonObject>();
        parentFlexalonObject = transform.parent.GetComponent<FlexalonObject>();
        button = GetComponent<Button>();
        NotificationManager.Instance.notificationTypes.Add(this);
    }
    public void Deactivate()
    {
        NotificationManager.Instance.notificationTypes.Remove(this);
        button.interactable = false;
        parentFlexalonObject.HeightType = SizeType.Component;
        flexalonObject.SkipLayout = true;
        flexalonObject.transform.SetParent(NotificationManager.Instance.bodyNotificationContainer);
        flexalonObject.GetComponent<RectTransform>().DOAnchorPosX(390, 0.3f).OnComplete(() =>
        {
            RectTransform parentRectTransform = parentFlexalonObject.GetComponent<RectTransform>();
            parentRectTransform.DOSizeDelta(new Vector2(parentRectTransform.sizeDelta.x, 0), 0.2f).OnComplete
            (() =>
            {
                Destroy(parentFlexalonObject.gameObject);
                Destroy(gameObject);
            });
        });
    }
    public void SetDialogueText(LocalizedString localizedString)
    {
        dialogText = localizedString;
        dialogText.StringChanged += value => chatMessageText.text = value;
    }
    public void SetListener()
    {
        //button.onClick.AddListener();
    }
}
