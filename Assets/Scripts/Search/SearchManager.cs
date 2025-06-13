using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;

public class SearchManager : MonoBehaviour
{
    public static SearchManager Instance;
    public GameObject SearchPanel;
    public GameObject[] Pages;
    public SearchData[] searchDatas;
    [Header("Web")]
    public GameObject WebPanel,WebLoadingPanel;
    public GameObject[] WebPages;

    [Header("History")]
    public Transform historyContainer;
    public GameObject historyPrefab;
    [Header("Bookmarks")]
    public Transform bookmarksContainer;
    public GameObject bookmarksPrefab;
    [Header("History")]
    public TMP_InputField searchField;
    [Header("Page")]
    public GameObject pageContainer;
    public GameObject pagePrefab;
    [Header("NoResultsPage")]
    public LocalizedString noResults;
    public TMP_Text noResultsText;
    [Header("FoundPage")]
    public TMP_Text foundResultsText;
    public LocalizedString foundResults;
    List<string> bookmarksList = new List<string>();
    string oldSearchText;
    bool Opening;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Opening = true;
    }
    public void OpenSearchTab(string tabName)
    {
        WebPanel.SetActive(false);
        SearchPanel.SetActive(true);
        foreach (GameObject item in Pages)
        {
            item.SetActive(item.name == tabName);
        }
    }
    public void OpenWebTab(string tabName)
    {
        SearchPanel.SetActive(false);
        WebPanel.SetActive(true);
        foreach (GameObject item in WebPages)
        {
            item.SetActive(item.name == tabName);
        }
    }
    public void CloseSearchTab()
    {
        Opening = false;
        gameObject.SetActive(false);
    }
    public void SearchSystem(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText) || searchText == oldSearchText) return;
        oldSearchText = searchText;
        searchField.text = searchText;
        CreateHistory(searchText);
        OpenSearchTab("SearchPage");
        foreach (Transform child in pageContainer.transform)
        {
            Destroy(child.gameObject);
        }
        List<SearchData> founds = new List<SearchData>();
        foreach (var item in searchDatas)
        {
            int foundCount = 0;
            foreach (var keywords in item.keywords)
            {
                if (foundCount != 0)
                {
                    break;
                }
                keywords.StringChanged += (localizedText) =>
                {
                    if (string.Equals(localizedText, searchText, StringComparison.OrdinalIgnoreCase))
                    {
                        founds.Add(item);
                        foundCount += 1;
                    }
                };
            }
        }
        if (founds.Count == 0)
        {
            noResults.Arguments = new object[] {searchText};
            noResults.StringChanged += (localizedText) =>
            {
                noResultsText.text = localizedText;
            };
            noResults.RefreshString();
            StartCoroutine(PageOpenerCoroutine("NoResultsPage",0.5f));
        }
        else
        {
            foreach (var item in founds)
            {
                foundResults.Arguments = new object[] { founds.Count, searchText };
                foundResults.StringChanged += (localizedtext) =>
                {
                    foundResultsText.text = localizedtext;
                };
                foundResults.RefreshString();
                PageManager pageManager = Instantiate(pagePrefab, pageContainer.transform).GetComponent<PageManager>();
                pageManager.searchData = item;
                item.Title.StringChanged += (localizedText) =>
                {
                    pageManager.TitleText.text = localizedText;
                };
                item.Link.StringChanged += (localizedText) =>
                {
                    pageManager.LinkText.text = localizedText;
                };
                item.Source.StringChanged += (localizedText) =>
                {
                    pageManager.SourceText.text = localizedText;
                };
                item.Info.StringChanged += (localizedText) =>
                {
                    pageManager.InfoText.text = localizedText;
                };
            }
            StartCoroutine(PageOpenerCoroutine("FoundPage",0.5f));
        }
    }
    public void WebSystem(SearchData searchData)
    {
        WebLoadingPanel.SetActive(true);
        if (searchData.saytNovu == SearchData.SaytNovu.Encyclopedia)
        {
            OpenWebTab("Encyclopedia");
            WebPages[0].GetComponent<WikipediaWebsite>().SetWikipediaWeb(searchData);
        }
        else if (searchData.saytNovu == SearchData.SaytNovu.News)
        {
            OpenWebTab("News");
        }
        else
        {
            OpenWebTab("Forum");
        }
    }
    public void LoadingFinish()
    {
        WebLoadingPanel.SetActive(false);
    }
    void CreateHistory(string text)
    {
        HistoryBookmarksRedirect newHistory = Instantiate(historyPrefab, historyContainer).GetComponent<HistoryBookmarksRedirect>();
        newHistory.redirectText.text = text;
    }
    void CreateBookmarks(string text)
    { 
        HistoryBookmarksRedirect newBookmarks = Instantiate(bookmarksPrefab, bookmarksContainer).GetComponent<HistoryBookmarksRedirect>();
        newBookmarks.redirectText.text = text;
        bookmarksList.Add(text);
    }
    public void CreateBookmarksFunction(string text)
    {
        bool isSame = false;
        if (bookmarksList.Count != 0)
        {
            foreach (var item in bookmarksList)
            {
                if (item == text)
                {
                    isSame = true;
                    break;
                }
            }
            if (!isSame)
            {
                CreateBookmarks(text);
            }
        }
        else
        {
            CreateBookmarks(text);
        }
    }
    IEnumerator PageOpenerCoroutine(string page,float secondTime)
    {
        yield return new WaitForSeconds(secondTime);
        OpenSearchTab(page);
    }
    void OnEnable()
    {
        if (!Opening)
        {
            Opening = true;
            OpenSearchTab("FirstPage");
        }
    }
}
