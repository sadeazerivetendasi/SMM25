using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.UI;

public class BookmarkRedirect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector] public SearchData searchData;
    [HideInInspector] public PageManager pageManager;
    
    [SerializeField] private TMP_Text redirectText;
    [SerializeField] private Color normalColor, hoverColor;
    private Tweener colorTween;
    LocalizedString localizedString;
    Image bg;
    void Start()
    {
        bg = GetComponent<Image>();
    }
    public void SetRedirectText(LocalizedString _localizedString)
    {
        localizedString = _localizedString;
        localizedString.StringChanged += SetText;
    }
    public void SetText(string text)
    {
        redirectText.text = text;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        colorTween = bg.DOColor(hoverColor, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        colorTween = bg.DOColor(normalColor, 0.2f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SearchManager.Instance.WebSystem(searchData);
    }
    void OnDisable()
    {
        colorTween?.Kill();
        bg.color = normalColor;
    }
}
