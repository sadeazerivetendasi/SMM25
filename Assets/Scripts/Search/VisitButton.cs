using Coffee.UIEffects;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    PageManager pageManager;
    RectTransform thisRect;
    public Image iconImage;
    public TMP_Text iconText;
    public RectTransform iconBorder;
    public Color normalColor, hoverColor;
    private Tweener moveTween, colorImageTween, colorTextTween, borderTween;

    void Awake()
    {
        thisRect = GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        AnimButton(0.5f, hoverColor, 0.2f, 1);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        AnimButton(0, normalColor, 0.2f, 0);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        pageManager.ConnectWeb();
    }
    private void AnimButton(float transformY, Color color, float time, float borderWidth)
    {
        moveTween = thisRect.DOAnchorPosY(transformY, time);
        colorImageTween = iconImage.DOColor(color, time);
        colorTextTween = iconText.DOColor(color, time);
        borderTween = iconBorder.DOScaleX(borderWidth, time);
    }
    void OnDisable()
    {
        moveTween?.Kill();
        colorImageTween?.Kill();
        colorTextTween?.Kill();
        borderTween?.Kill();

        // Əl ilə sıfırlama
        thisRect.anchoredPosition = new Vector2(thisRect.anchoredPosition.x, 0);
        iconImage.color = normalColor;
        iconText.color = normalColor;
        iconBorder.localScale = new Vector2(0, 1);
    }

}
