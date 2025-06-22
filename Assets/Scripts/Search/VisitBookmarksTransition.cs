using Coffee.UIEffects;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisitBookmarksTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public PageManager pageManager;
    public enum ButtonType
    {
        Visit, Bookmarks
    }
    [ShowIf(nameof(IsBookmarks))]
    public Sprite normalBookmark, selectBookmark;
    public ButtonType buttonType;
    RectTransform thisRect;
    public Image iconImage;
    public TMP_Text iconText;
    public RectTransform iconBorder;
    public Color normalColor, hoverColor;
    private Tweener moveTween, colorImageTween, colorTextTween, borderTween;

    private bool IsBookmarks() => buttonType == ButtonType.Bookmarks;
    void Awake()
    {
        thisRect = GetComponent<RectTransform>();
    }
    void Start() {
        if(buttonType == ButtonType.Visit) pageManager.Visit.StringChanged += (localizedtext) => iconText.text = localizedtext;
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
        if (buttonType == ButtonType.Bookmarks)
        {
            if (pageManager.CreateBookmark()) BookmarksSpriteChange(true);
            else BookmarksSpriteChange(false);
        }
        else
        {
            pageManager.ConnectWeb();
        }
    }
    public void AnimButton(float transformY, Color color, float time, float borderWidth)
    {
        moveTween = thisRect.DOAnchorPosY(transformY, time);
        colorImageTween = iconImage.DOColor(color, time);
        colorTextTween = iconText.DOColor(color, time);
        borderTween = iconBorder.DOScaleX(borderWidth, time);
    }
    public void BookmarksSpriteChange(bool Bookmarks)
    {
        if (Bookmarks)
        {
            iconImage.sprite = selectBookmark;
            pageManager.Bookmarked.StringChanged += (localizedtext) => iconText.text = localizedtext;
        }
        else
        {
            iconImage.sprite = normalBookmark;
            pageManager.Bookmark.StringChanged += (localizedtext) => iconText.text = localizedtext;
        }
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
