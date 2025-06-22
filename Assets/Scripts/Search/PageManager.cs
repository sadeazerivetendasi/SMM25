using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using DG.Tweening;
using NUnit.Framework.Constraints;
using LeTai.TrueShadow;

public class PageManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SearchData searchData;
    public TrueShadow shadowObject;
    float shadowValue;
    RectTransform thisRect;
    Tween shadowElement, transformElement;
    Vector2 originalVector;
    public VisitBookmarksTransition visitBookmarksTransition;
    public TMP_Text TitleText, LinkText, SourceText, InfoText;
    public LocalizedString Visit, Bookmark, Bookmarked;
    void Start()
    {
        thisRect = GetComponent<RectTransform>();
        originalVector = thisRect.anchoredPosition;
    }
    public bool CreateBookmark()
    {
        return SearchManager.Instance.CreateBookmarksFunction(searchData, this);
    }
    public void ConnectWeb()
    {
        SearchManager.Instance.WebSystem(searchData, this);
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
        }, 1.59f, 0.2f);
    }
    void OnDisable()
    {
        shadowElement?.Kill();
        transformElement?.Kill();
        shadowObject.Size = 1.59f;
        thisRect.anchoredPosition = originalVector;
    }
}
