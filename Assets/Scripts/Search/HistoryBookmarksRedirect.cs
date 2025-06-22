using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HistoryBookmarksRedirect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum ButtonType
    {
        History, Bookmarks
    }
    [HideInInspector] public SearchData searchData;
    [HideInInspector] public PageManager pageManager;
    public ButtonType buttonType;
    public TMP_Text redirectText;
    public Color normalColor, hoverColor;
    private Tweener colorTween;
    Image bg;
    void Start()
    {
        bg = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        colorTween?.Kill();
        colorTween = bg.DOColor(hoverColor, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        colorTween?.Kill();
        colorTween = bg.DOColor(normalColor, 0.2f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.History:
                SearchManager.Instance.SearchSystem(redirectText.text);
                break;
            case ButtonType.Bookmarks:
                SearchManager.Instance.WebSystem(searchData, pageManager);
                break;
        }
    }
    void OnDisable()
    {
        colorTween?.Kill();
        bg.color = normalColor;
    }
}
