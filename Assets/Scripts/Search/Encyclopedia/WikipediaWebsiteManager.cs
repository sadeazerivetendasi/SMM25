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
            AboutController aboutController = item.basliqNovu == WikipediaData.BasliqNovu.Header ?
                Instantiate(headerPrefab, bodyContentContainer).GetComponent<AboutController>() :
                    Instantiate(sectionPrefab, bodyContentContainer).GetComponent<AboutController>();
            item.SectionInfo.StringChanged += aboutController.SetInfoText;
            item.SectionTitle.StringChanged += aboutController.SetTitleText;
            if (item.isFlex)
            {
                item.flexTitle.StringChanged += aboutController.SetFlexTitleText;
                item.flexInfo.StringChanged += aboutController.SetFlexInfoText;
            }
        }
        SearchManager.Instance.Invoke("LoadingFinish", 0.5f);
    }
}
