using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HistoryRedirect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text redirectText;
    [SerializeField] private Color normalColor, hoverColor;
    private Tweener colorTween;
    Image bg;
    void Start()
    {
        bg = GetComponent<Image>();
    }
    public void SetRedirectText(string _text)
    {
        redirectText.text = _text;
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
        SearchManager.Instance.SearchSystem(redirectText.text);
    }
    void OnDisable()
    {
        colorTween?.Kill();
        bg.color = normalColor;
    }
}
