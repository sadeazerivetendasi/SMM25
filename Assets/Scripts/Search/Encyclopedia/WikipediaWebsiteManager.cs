using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class WikipediaWebsiteManager : MonoBehaviour
{
    public Transform bodyContentContainer;
    public GameObject headerPrefab, sectionPrefab;
    List<WikipediaData> encyclopediaWebsites;
    public void SetWikipediaWeb(SearchData searchData)
    {
        foreach (Transform child in bodyContentContainer.transform)
        {
            Destroy(child.gameObject);
        }
        encyclopediaWebsites = searchData.encyclopediaWebsites;
        foreach (var item in encyclopediaWebsites)
        {
            AboutController aboutController;
            if (item.basliqNovu == WikipediaData.BasliqNovu.Header)
            {
                aboutController = Instantiate(headerPrefab, bodyContentContainer).GetComponent<AboutController>();
            }
            else
            {
                aboutController = Instantiate(sectionPrefab, bodyContentContainer).GetComponent<AboutController>();
            }
            item.SectionInfo.StringChanged += (localizedtext) =>
            {
                aboutController.infoText.text = localizedtext;
            };
            item.SectionTitle.StringChanged += (localizedtext) =>
            {
                aboutController.titleText.text = localizedtext;
            };
            if (item.isFlex)
            {
                item.flexTitle.StringChanged += (localizedtext) =>
                {
                    aboutController.flexTitleText.text = localizedtext;
                };
                item.flexInfo.StringChanged += (localizedtext) =>
                {
                    aboutController.flexInfoText.text = localizedtext;
                };
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)aboutController.transform);
        }
        SearchManager.Instance.Invoke("LoadingFinish",0.5f);
    }
}
