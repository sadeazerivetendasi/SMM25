using DG.Tweening;
using Flexalon;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class ChatNotification : MonoBehaviour, INotification
{
    [SerializeField] private TMP_Text chatPersonNameText, chatMessageText;
    private LocalizedString _messageLocalize;
    private FlexalonObject flexalonObject, parentFlexalonObject;
    private Button button;
    public void Click()
    {
        
    }
    void Awake()
    {
        flexalonObject = GetComponent<FlexalonObject>();
        parentFlexalonObject = transform.parent.GetComponent<FlexalonObject>();
        button = GetComponent<Button>();
        //NotificationManager.Instance.AddNotificationList(this);
    }
    public void Deactivate()
    {
        //NotificationManager.Instance.RemoveNotificationList(this);
        button.interactable = false;
        parentFlexalonObject.HeightType = SizeType.Component;
        flexalonObject.SkipLayout = true;
        //flexalonObject.transform.SetParent(NotificationManager.Instance.bodyNotificationContainer);
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
        _messageLocalize = localizedString;
        _messageLocalize.StringChanged += SetText;
    }
    private void SetText(string localizedText)
    {
        chatMessageText.text = localizedText;
    }
    void OnDisable()
    {
        _messageLocalize.StringChanged -= SetText;
    }

    public void Remove()
    {
        throw new System.NotImplementedException();
    }
}
