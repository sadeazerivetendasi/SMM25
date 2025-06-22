using System;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using Random = UnityEngine.Random;

public class DarkWebManager : MonoBehaviour
{
    public static DarkWebManager Instance;
    public ItemPageController itemPageController;
    [Header("Transform")]
    public Transform bodyContentContainer, commentContentContainer;
    [Foldout("Ip and Connected Text")]
    public LocalizedString Ip;
    [Foldout("Ip and Connected Text")]
    public TMP_Text ipText;
    [Foldout("Ip and Connected Text")]
    public LocalizedString Connected;
    [Foldout("Ip and Connected Text")]
    public TMP_Text connectedText;

    [Foldout("Found Text")]
    public LocalizedString Found;
    [Foldout("Found Text")]
    public TMP_Text foundText;
    [Header("Prefab")]
    public GameObject elementPrefab;
    public GameObject commentPrefab;
    public List<DarkWebData> darkWebDatas;
    public GameObject[] Pages;
    public GameObject ErrorMesage;
    GameObject activePage;
    DarkWebData activeDarkWebData;
    void Awake()
    {
        activeDarkWebData = null;
        Instance = this;
    }
    void Start()
    {
        OpenTab(Pages[0].name);
    }

    public void SearchSystem(string searchText)
    {
        if (string.Equals("shadow.market", searchText, StringComparison.OrdinalIgnoreCase) && activePage == Pages[0])
        {
            ConnectShadowMarket();
        }
        else if (activePage == Pages[0])
        {
            ErrorMesage.SetActive(true);
        }
    }
    public void OpenTab(string tabName)
    {
        foreach (GameObject item in Pages)
        {
            item.SetActive(item.name == tabName);
            if (item.name == tabName)
            {
                activePage = item;
            }
        }
    }
    public void ConnectShadowMarket()
    {
        OpenTab("LoadingPage");
        foreach (Transform child in bodyContentContainer.transform)
        {
            Destroy(child.gameObject);
        }
        float ip1 = Random.Range(10, 128);
        float ip2 = Random.Range(10, 128);
        Ip.Arguments = new object[] { ip1, ip2 };
        Ip.StringChanged += (localizedtext) => ipText.text = localizedtext;
        foreach (DarkWebData item in darkWebDatas)
        {
            DarkWebElementController darkWebElementController = Instantiate(elementPrefab, bodyContentContainer).GetComponent<DarkWebElementController>();
            Found.Arguments = new object[] { darkWebDatas.Count };
            Found.StringChanged += (localizedtext) => foundText.text = localizedtext;
            item.Title.StringChanged += (localizedtext) => darkWebElementController.titleText.text = localizedtext;
            item.Info.StringChanged += (localizedtext) => darkWebElementController.infoText.text = localizedtext;
            item.Type.StringChanged += (localizedtext) => darkWebElementController.typeText.text = localizedtext;
            darkWebElementController.vendorText.text = item.vendorName.ToString();
            darkWebElementController.ratingOneText.text = item.ratingText;
            darkWebElementController.ratingTwoText.text = item.ratingTextTwo;
            darkWebElementController.priceText.text = $"₡ {item.priceNumber}";
            darkWebElementController.darkWebData = item;
        }

        OpenTab("ShadowMarket");
    }
    public void ConnectElement(DarkWebData darkWebData)
    {
        if (darkWebData != activeDarkWebData)
        {
            activeDarkWebData = darkWebData;
            foreach (Transform item in commentContentContainer.transform)
            {
                Destroy(item.gameObject);
            }
            darkWebData.Title.StringChanged += (localizedtext) => itemPageController.nameText.text = localizedtext;
            darkWebData.Type.StringChanged += (localizedtext) => itemPageController.typeText.text = localizedtext;
            darkWebData.Price.Arguments = new object[] { darkWebData.priceNumber };
            itemPageController.vendorText.text = darkWebData.vendorName.ToString();
            darkWebData.Category.StringChanged += (localizedtext) => itemPageController.categoryText.text = localizedtext;
            darkWebData.Transactions.StringChanged += (localizedtext) => itemPageController.transactionsText.text = localizedtext;
            darkWebData.Listed.StringChanged += (localizedtext) => itemPageController.listedText.text = localizedtext;
            darkWebData.Info.StringChanged += (localizedtext) => itemPageController.descriptionText.text = localizedtext;
            foreach (CommentData item in darkWebData.customerReviews)
            {
                CommentController commentController = Instantiate(commentPrefab, commentContentContainer).GetComponent<CommentController>();
                item.Comment.StringChanged += (localizedtext) => commentController.commentText.text = localizedtext;
                item.Customer.StringChanged += (localizedtext) => commentController.customerText.text = localizedtext;
                item.Data.StringChanged += (localizedtext) => commentController.dateText.text = localizedtext;
                commentController.StarSystem(item.Star);
            }
        }
        OpenTab("ItemPage");
    }
}
