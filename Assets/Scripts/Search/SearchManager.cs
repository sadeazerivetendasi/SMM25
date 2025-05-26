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
    public GameObject[] Pages;
    public SearchData[] searchDatas;
    [Header("History")]
    public Transform historyContainer;
    public GameObject historyPrefab;
    [Header("Bookmarks")]
    public Transform bookmarksContainer;
    public GameObject bookmarksPrefab;
    [Header("Page")]
    public GameObject pageContainer;
    public GameObject pagePrefab;
    [Header("NoResultsPage")]
    public LocalizedString noResults;
    public TMP_Text noResultsText;
    [Header("FoundPage")]
    public TMP_Text foundResultsText;
    public LocalizedString foundResults;
    bool Opening;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Opening = true;
    }
    public void OpenTab(string tabName)
    {
        foreach (GameObject item in Pages)
        {
            if (item.name == tabName)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }
    public void CloseSearchTab()
    {
        Opening = false;
        gameObject.SetActive(false);
    }
    public void SearchSystem(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText)) return;
        CreateHistory(searchText);
        OpenTab("SearchPage");
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
    void CreateHistory(string text)
    {
        GameObject newHistory = Instantiate(historyPrefab, historyContainer);
        newHistory.transform.GetChild(1).GetComponent<TMP_Text>().text = text;
    }
    IEnumerator PageOpenerCoroutine(string page,float secondTime)
    {
        yield return new WaitForSeconds(secondTime);
        OpenTab(page);
    }
    void OnEnable()
    {
        if (!Opening)
        {
            Opening = true;
            OpenTab("FirstPage");
        }
    }
}
