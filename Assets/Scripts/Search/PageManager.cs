using TMPro;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public SearchData searchData;
    public TMP_Text TitleText, LinkText, SourceText, InfoText;
    public void ConnectWeb()
    {
        SearchManager.Instance.WebSystem(searchData);
    }
}
