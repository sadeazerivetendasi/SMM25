using System;
using Coffee.UIEffects;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.UI;

public class BookmarkButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    PageManager pageManager;
    RectTransform thisRect;
    Action action;
    [SerializeField] private LocalizedString bookmarkLoc, bookmarkedLoc;
    [SerializeField] private Sprite normalBookmark, selectBookmark;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text iconText;
    [SerializeField] private RectTransform iconBorder;
    [SerializeField] private Color normalColor, hoverColor;
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
        if (pageManager.CreateBookmark()) BookmarksSpriteChange(true);
        else BookmarksSpriteChange(false);
    }
    private void AnimButton(float transformY, Color color, float time, float borderWidth)
    {
        moveTween = thisRect.DOAnchorPosY(transformY, time);
        colorImageTween = iconImage.DOColor(color, time);
        colorTextTween = iconText.DOColor(color, time);
        borderTween = iconBorder.DOScaleX(borderWidth, time);
    }
    public void BookmarksSpriteChange(bool Bookmarks)
    {
        bookmarkLoc.StringChanged -= SetBookmarkText;
        bookmarkedLoc.StringChanged -= SetBookmarkText;
        if (Bookmarks)
        {
            iconImage.sprite = selectBookmark;
            bookmarkedLoc.StringChanged += SetBookmarkText;
        }
        else
        {
            iconImage.sprite = normalBookmark;
            bookmarkLoc.StringChanged += SetBookmarkText;
        }
    }
    private void SetBookmarkText(string localizedtext)
    {
        iconText.text = localizedtext;
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
    void OnDestroy()
    {
        bookmarkLoc.StringChanged -= SetBookmarkText;
        bookmarkedLoc.StringChanged -= SetBookmarkText;
    }
}
