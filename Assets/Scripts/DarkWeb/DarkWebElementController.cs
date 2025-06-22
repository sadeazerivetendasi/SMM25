using DG.Tweening;
using LeTai.TrueShadow;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DarkWebElementController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector] public DarkWebData darkWebData;
    [Foldout("Texts")]
    public TMP_Text titleText, infoText, vendorText, ratingOneText, ratingTwoText, typeText, priceText;
    public TrueShadow shadowObject;
    float shadowValue;
    Tween transformElement, shadowElement;

    RectTransform thisRect;
    Vector2 originalVector;
    void Start()
    {
        thisRect = GetComponent<RectTransform>();
        shadowValue = shadowObject.Size;
        originalVector = thisRect.anchoredPosition;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transformElement = thisRect.DOAnchorPosY(originalVector.y + 2f, 0.2f);
        shadowElement = DOTween.To(() => shadowValue, x =>
        {
            shadowValue = x;
            shadowObject.Size = shadowValue;
        }, 6.05f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transformElement = thisRect.DOAnchorPosY(originalVector.y, 0.2f);
        shadowElement = DOTween.To(() => shadowValue, x =>
        {
            shadowValue = x;
            shadowObject.Size = shadowValue;
        }, 1.5f, 0.2f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        DarkWebManager.Instance.ConnectElement(darkWebData);
    }
    void OnDisable()
    {
        shadowElement?.Kill();
        transformElement?.Kill();
        shadowObject.Size = 0;
        thisRect.anchoredPosition = originalVector;
    }

}
