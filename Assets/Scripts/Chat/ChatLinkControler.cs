using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;

public class ChatLinkControler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    TMP_Text linkText;
    private LocalizedString linkLocalize;

    void Start()
    {
        linkText = GetComponent<TMP_Text>();
    }
    public void SetTextLink(LocalizedString localizedString)
    {
        linkLocalize = localizedString;
        linkLocalize.StringChanged += SetText;
    }
    public void SetText(string localizedString)
    {
        linkText.text = localizedString;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        linkText.fontStyle = FontStyles.Underline; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        linkText.fontStyle = FontStyles.Normal; 
    }
    void OnDestroy()
    {
        linkLocalize.StringChanged -= SetText;
    }
}
