using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MusicPlayerRedirect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Image backgroundImage;
    public Transform Bar;
    public TMP_Text iconText;
    public Image iconImage;
    public Color inActiveColor, hoverColor, ActiveColor, borderActiveColor, borderInactiveColor;
    public Image Border;
    public bool firstPage;
    bool isClicked;
    void Start()
    {
        backgroundImage = GetComponent<Image>();
        if (firstPage)
        {
            MusicPlayerManager.Instance.musicPlayerRedirectActive = this;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        MusicPlayerManager.Instance.ChangePage(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked) backgroundImage.DOColor(hoverColor, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked) backgroundImage.DOColor(inActiveColor, 0.2f);
    }
    public void Activate()
    {
        iconImage.DOColor(borderActiveColor, 0.2f);
        iconText.DOColor(borderActiveColor, 0.2f);
        backgroundImage.DOColor(ActiveColor, 0.2f);
        Border.DOColor(borderActiveColor, 0.2f);
        isClicked = true;
        Bar.SetAsLastSibling();
    }
    public void Deactivate()
    {
        iconImage.DOColor(Color.white, 0.2f);
        iconText.DOColor(Color.white, 0.2f);
        backgroundImage.DOColor(inActiveColor, 0.2f);
        Border.DOColor(borderInactiveColor, 0.2f);
        isClicked = false;
    }
}
