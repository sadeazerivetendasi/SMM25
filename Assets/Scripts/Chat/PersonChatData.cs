using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI.ProceduralImage;

public class PersonChatData : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ProceduralImage avatarImage,borderImage;
    [SerializeField] private TMP_Text nameText, roleText;
    [SerializeField] private ChatController messagePanel;
    private ChatData data;
    bool isClicked;

    public void Initialize(ChatData _data, ChatController _messagePanel)
    {
        data = _data;

        messagePanel = _messagePanel;
        messagePanel.SetChatData(_data);
        messagePanel.gameObject.SetActive(false);

        this.name = data.ID;
        avatarImage.sprite = data.Logo;
        data.Name.StringChanged += SetNameText;
        data.Position.StringChanged += SetPositionText;
    }
    private void SetNameText(string value)
    {
        nameText.text = value;
    }
    private void SetPositionText(string value)
    {
        roleText.text = value;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ChatManager.Instance.SelectUser(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked) borderImage.DOFade(1, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked) borderImage.DOFade(0, 0.2f);
    }

    public void Activate()
    {
        if (messagePanel != null)
            messagePanel.gameObject.SetActive(true);
        isClicked = true;
        borderImage.DOFade(1, 0.2f);
    }

    public void Deactivate()
    {
        if (messagePanel != null)
            messagePanel.gameObject.SetActive(false);
        isClicked = false;
        borderImage.DOFade(0, 0.2f);
    }
    void OnDestroy()
    {
        data.Name.StringChanged -= SetNameText;
        data.Position.StringChanged -= SetPositionText;
    }
}
