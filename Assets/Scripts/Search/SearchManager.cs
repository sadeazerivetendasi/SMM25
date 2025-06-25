using System;
using System.Collections;
using System.Collections.Generic;
using Flexalon;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;

public class SearchManager : MonoBehaviour
{
    public static SearchManager Instance;
    [SerializeField] private BookmarkSystem bookmarkSystem;
    [SerializeField] private HistorySystem historySystem;
    [SerializeField] private GameObject SearchPanel;
    [SerializeField] private FlexalonFlexibleLayout SearchPanelFlexibleLayout;
    [SerializeField] private GameObject[] Pages;
    [SerializeField] private SearchData[] searchDatas;

    [Header("Web")]
    [SerializeField] private SearchWebManager searchWebManager;
    [SerializeField] private GameObject WebPanel;
    [SerializeField] private GameObject[] WebPages;
    [SerializeField] private TMP_InputField searchField;

    [Header("Page")]
    [SerializeField] private GameObject pageContainer;
    [SerializeField] private GameObject pagePrefab;

    [Header("NoResultsPage")]
    [SerializeField] private LocalizedString noResults;
    [SerializeField] private TMP_Text noResultsText;

    [Header("FoundPage")]
    [SerializeField] private TMP_Text foundResultsText;
    [SerializeField] private LocalizedString foundResults;
    string oldSearchText;
    bool Opening;
    List<SearchData> founds;
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
    public void CloseSearchTab()
    {
        Opening = false;
        gameObject.SetActive(false);
    }
    public void SearchSystem(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText) || searchText == oldSearchText) return;
        SearchPanelFlexibleLayout.enabled = true;
        oldSearchText = searchText;
        searchField.text = searchText;
        historySystem.CreateHistory(searchText);
        OpenSearchTab("SearchPage");
        foreach (Transform child in pageContainer.transform)
        {
            Destroy(child.gameObject);
        }
        founds = new List<SearchData>();
        foreach (var item in searchDatas)
        {
            foreach (var keywords in item.keywords)
            {
                if (founds.Count != 0)
                {
                    break;
                }
                keywords.StringChanged += (localizedText) =>
                {
                    if (string.Equals(localizedText, searchText, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(localizedText.Replace(" ", ""), searchText.Replace(" ", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        founds.Add(item);
                    }
                };
            }
        }
        if (founds.Count == 0)
        {
            noResults.Arguments = new object[] { searchText };
            noResults.StringChanged += (localizedText) => noResultsText.text = localizedText;
            noResults.RefreshString();
            StartCoroutine(PageOpenerCoroutine("NoResultsPage", 0.5f));
        }
        else
        {
            foreach (var item in founds)
            {
                foundResults.Arguments = new object[] { founds.Count, searchText };
                foundResults.StringChanged += (localizedtext) => foundResultsText.text = localizedtext;
                foundResults.RefreshString();
                PageManager pageManager = Instantiate(pagePrefab, pageContainer.transform).GetComponent<PageManager>();
                pageManager.Initialize(item);
                // if (bookmarksSearchDataList.IndexOf(item) != -1)
                // {
                //     pageManager.visitBookmarksTransition.BookmarksSpriteChange(true);
                // }
            }
            StartCoroutine(PageOpenerCoroutine("FoundPage", 0.5f));
        }
    }
    public void WebSystem(SearchData searchData)
    {
        WebPanel.SetActive(true);
        searchWebManager.Initialize(searchData);
    }
    public bool CreateBookmark(SearchData searchData)
    {
        return bookmarkSystem.CreateBookmarkFunc(searchData);
    }
    IEnumerator PageOpenerCoroutine(string page, float secondTime)
    {
        yield return new WaitForSeconds(secondTime);
        OpenSearchTab(page);
    }
    void OnEnable()
    {
        if (!Opening)
        {
            Opening = true;
            oldSearchText = null;
            searchField.text = null;
            OpenSearchTab("FirstPage");
        }
    }
}
