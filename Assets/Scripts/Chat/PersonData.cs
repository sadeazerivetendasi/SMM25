using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PersonData : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Image avatarImage;
    public TMP_Text nameText, roleText;
    public GameObject messagePanel;
    public Image borderImage;
    private ChatData data;
    bool isClicked;

    public void Initialize(ChatData _data)
    {
        data = _data;
        avatarImage.sprite = data.Logo;
        data.Name.StringChanged += value => nameText.text = value;
        data.Position.StringChanged += value => roleText.text = value;
        data.Name.RefreshString();
        data.Position.RefreshString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChatManager.Instance.SelectUser(this);
    }

    public void Activate()
    {
        if (messagePanel != null)
            messagePanel.SetActive(true);
        isClicked = true;
        borderImage.DOFade(1, 0.2f);
    }

    public void Deactivate()
    {
        if (messagePanel != null)
            messagePanel.SetActive(false);
        isClicked = false;
        borderImage.DOFade(0, 0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isClicked) borderImage.DOFade(1, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isClicked) borderImage.DOFade(0, 0.2f);
    }
}
