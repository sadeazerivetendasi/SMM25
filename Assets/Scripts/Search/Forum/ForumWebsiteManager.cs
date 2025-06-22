using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ForumWebsiteManager : MonoBehaviour
{
    public Transform bodyContentContainer;
    public TMP_Text forumTitle, forumCreated, forumInfo, forumCatalogue;
    public GameObject chatPrefab;
    List<ForumData> forumDatas;
    SearchData activeSearchData;
    public void SetForumWeb(SearchData searchData)
    {
        foreach (Transform child in bodyContentContainer.transform)
        {
            Destroy(child.gameObject);
        }
        activeSearchData = searchData;
        forumDatas = activeSearchData.forumDatas;
        foreach (var item in forumDatas)
        {
            ForumChatController forumChatController = Instantiate(chatPrefab, bodyContentContainer).GetComponent<ForumChatController>();
            forumChatController.personLogo.sprite = item.personLogo;
            forumChatController.personName.text = item.personName.ToString();
            #region 
            item.personDescription.StringChanged += (localizedtext) =>
            {
                forumChatController.personDescription.text = localizedtext;
            };
            item.personInfo.StringChanged += (localizedtext) =>
            {
                forumChatController.personInfo.text = localizedtext;
            };
            item.messageData.StringChanged += (localizedtext) =>
            {
                forumChatController.messageData.text = localizedtext;
            };
            item.messageBox.StringChanged += (localizedtext) =>
            {
                forumChatController.messageBox.text = localizedtext;
            };
            #endregion
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)forumChatController.transform);
        }
        #region 
        activeSearchData.forumTitle.StringChanged += (localizedtext) =>
        {
            forumTitle.text = localizedtext;
        };
        activeSearchData.forumCreated.StringChanged += (localizedtext) =>
        {
            forumCreated.text = localizedtext;
        };
        activeSearchData.forumInfo.Arguments = new object[] { forumDatas.Count, 25 };
        activeSearchData.forumInfo.StringChanged += (localizedtext) =>
        {
            forumInfo.text = localizedtext;
        };
        activeSearchData.forumInfo.RefreshString();
        activeSearchData.forumCatalogue.StringChanged += (localizedtext) =>
        {
            forumCatalogue.text = localizedtext;
        };
        #endregion
        SearchManager.Instance.Invoke("LoadingFinish",0.5f);
    }
}
