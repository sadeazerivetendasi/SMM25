using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using LeTai.TrueShadow;

public class PageManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SearchData searchData;
    [SerializeField] private TrueShadow shadowObject;
    [SerializeField] private TMP_Text TitleText, LinkText, SourceText, InfoText;
    public SearchData SearchData { get => searchData; set => searchData = value; }
    private float shadowValue;
    RectTransform thisRect;
    Tween shadowElement, transformElement;
    Vector2 originalVector;

    void Start()
    {
        thisRect = GetComponent<RectTransform>();
        originalVector = thisRect.anchoredPosition;
    }
    public void Initialize(SearchData _searchData)
    {
        searchData = _searchData;
        searchData.Title.StringChanged += SetTitleText;
        searchData.Link.StringChanged += SetLinkText;
        searchData.Source.StringChanged += SetSourceText;
        searchData.Info.StringChanged += SetInfoText;
    }
    #region LocalizeText
    private void SetTitleText(string localizedText)
    {
        TitleText.text = localizedText;
    }
    private void SetLinkText(string localizedText)
    {
        LinkText.text = localizedText;
    }
    private void SetSourceText(string localizedText)
    {
        SourceText.text = localizedText;
    }
    private void SetInfoText(string localizedText)
    {
        InfoText.text = localizedText;
    }
    #endregion
    public void ConnectWeb()
    {
        SearchManager.Instance.WebSystem(searchData);
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
    void OnDestroy()
    {
        if (searchData != null)
        {
            searchData.Title.StringChanged -= SetTitleText;
            searchData.Link.StringChanged -= SetLinkText;
            searchData.Source.StringChanged -= SetSourceText;
            searchData.Info.StringChanged -= SetInfoText;   
        }
    }
}
