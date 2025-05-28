using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HistoryBookmarksRedirect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public TMP_Text redirectText;
    public Color normalColor, hoverColor;
    Image bg;
    void Start()
    {
        bg = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bg.DOColor(hoverColor,0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.DOColor(normalColor,0.2f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Redirect();
    }


    public void Redirect()
    {
        SearchManager.Instance.SearchSystem(redirectText.text);
    }
}
