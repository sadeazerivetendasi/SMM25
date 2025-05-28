using Coffee.UIEffects;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisitBookmarksTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum ButtonType
    {
        Visit, Bookmarks
    }
    public TMP_Text Title;
    public ButtonType buttonType;
    RectTransform thisRect;
    public Image iconImage;
    public TMP_Text iconText;
    public RectTransform iconBorder;
    public Color normalColor, hoverColor;
    void Awake()
    {
        thisRect = GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        AnimButton(1, hoverColor, 0.2f, 80);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        AnimButton(0, normalColor, 0.2f,0);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (buttonType == ButtonType.Bookmarks)
        {
            SearchManager.Instance.CreateBookmarksFunction(Title.text);    
        }
    }
    public void AnimButton(float transformY, Color color, float time, float borderWidth)
    {
        thisRect.DOAnchorPosY(transformY, time);
        iconImage.DOColor(color, time);
        iconText.DOColor(color, time);
        iconBorder.DOSizeDelta(new Vector2(borderWidth, iconBorder.sizeDelta.y),time);
    }
}
