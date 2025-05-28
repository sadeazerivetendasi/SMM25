using UnityEngine;
using System.Collections.Generic;

public class ChatManager : MonoBehaviour
{
    public static ChatManager Instance;

    [Header("User Panel Settings")]
    public Transform userButtonParent;
    public GameObject userButtonPrefab;
    [Header("User Message Panel Settings")]
    public Transform userMessagePageParent;
    public GameObject userMessagePagePrefab;

    [Header("Message Panels")]
    public List<GameObject> pageList;

    private PersonData currentSelected;

    private void Awake()
    {
        Instance = this;
        ChangePage("FirstPage");
    }
    public void SpawnUserAndAutomaticOpener(ChatData chatData)
    {
        GameObject userGO = Instantiate(userButtonPrefab, userButtonParent);
        PersonData pd = userGO.GetComponent<PersonData>();
        userGO.name = chatData.ID;
        pd.Initialize(chatData);
        GameObject userMessagePage = Instantiate(userMessagePagePrefab, userMessagePageParent);
        userMessagePage.name = chatData.ID;
        pd.messagePanel = userMessagePage;
        SelectUser(pd);
    }
    public void SpawnUser(ChatData chatData)
    {
        SpawnUserFunction(chatData);
    }
    void SpawnUserFunction(ChatData chatData)
    {
        GameObject userGO = Instantiate(userButtonPrefab, userButtonParent);
        PersonData pd = userGO.GetComponent<PersonData>();
        userGO.name = chatData.ID;
        pd.Initialize(chatData);
        GameObject userMessagePage = Instantiate(userMessagePagePrefab, userMessagePageParent);
        userMessagePage.name = chatData.ID;
        pd.messagePanel = userMessagePage;
        pd.messagePanel.SetActive(false);
    }
    public void SelectUser(PersonData user)
    {
        if (currentSelected == user)
        {
            currentSelected.Deactivate();
            currentSelected = null;
            ChangePage("FirstPage");
        }
        else
        {
            if (currentSelected != null) currentSelected.Deactivate();
            ChangePage("MessagesPage");
            currentSelected = user;
            currentSelected.Activate();
        }
    }

    public void ChangePage(string pageName)
    {
        foreach (GameObject item in pageList)
        {
            bool isFound = (item.name == pageName);
            item.SetActive(isFound);
        }
    }

}
