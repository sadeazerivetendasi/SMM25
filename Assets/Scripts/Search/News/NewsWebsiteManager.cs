using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsWebsiteManager : MonoBehaviour
{
    public Image adImage;
    public TMP_Text titleText, creatorText, contentText;
    NewsData newsData;
    public void SetNewsWeb(SearchData searchData)
    {
        newsData = searchData.newsData;
        adImage.sprite = newsData.Image;
        newsData.Title.StringChanged += (localizedtext) => titleText.text = localizedtext;
        newsData.Creator.StringChanged += (localizedtext) => creatorText.text = localizedtext;
        newsData.Content.StringChanged += (localizedtext) => contentText.text = localizedtext;
        SearchManager.Instance.Invoke("LoadingFinish", 0.5f);
    }
}
