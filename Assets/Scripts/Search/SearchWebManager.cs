using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SearchWebManager : MonoBehaviour
{
    PageManager pageManager;
    [SerializeField] private TMP_Text linkText;
    [SerializeField] private GameObject[] Pages;
    [SerializeField] private Image bookmarkImage;
    [SerializeField] private Sprite normalBookmark, selectBookmark;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private WikipediaWebsiteManager wikipediaWebsiteManager;
    [SerializeField] private ForumWebsiteManager forumWebsiteManager;
    [SerializeField] private NewsWebsiteManager newsWebsiteManager;
    public PageManager PageManager { get => pageManager; set => pageManager = value; }
    public void Initialize(SearchData searchData)
    {
        loadingPanel.SetActive(true);
        searchData.Link.StringChanged += SetLinkText;
        switch (searchData.saytNovu)
        {
            case SearchData.SaytNovu.Encyclopedia:
                wikipediaWebsiteManager.SetWikipediaWeb(searchData);
                break;
            case SearchData.SaytNovu.Forum:
                forumWebsiteManager.SetForumWeb(searchData);
                break;
            default:
                newsWebsiteManager.SetNewsWeb(searchData);
                break;
        }
    }
    private void SetLinkText(string localizedtext)
    {
        linkText.text = localizedtext;
    }
    public void OpenWebTab(string tabName)
    {
        foreach (GameObject item in Pages)
        {
            item.SetActive(item.name == tabName);
        }
    }
    public void CreateBookmarks()
    {
        // if (SearchManager.Instance.CreateBookmarksFunction(pageManager.SearchData))
        // {
        //     BookmarksSpriteChange(true);
        //     //pageManager.visitBookmarksTransition.BookmarksSpriteChange(true);
        // }
        // else
        // {
        //     BookmarksSpriteChange(false);
        //     //pageManager.visitBookmarksTransition.BookmarksSpriteChange(false);
        // }
    }
    public void BookmarksSpriteChange(bool Bookmarks)
    {
        if (Bookmarks)
        {
            bookmarkImage.sprite = selectBookmark;
        }
        else bookmarkImage.sprite = normalBookmark;
    }
}
