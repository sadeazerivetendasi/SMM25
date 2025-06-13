using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class DarkWebManager : MonoBehaviour
{
    public static DarkWebManager Instance;
    public GameObject[] Pages;
    public GameObject ErrorMesage;
    GameObject activePage;
    void Awake()
    {
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
            OpenTab("LoadingPage");
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
            if (item.name == tabName)
            {
                item.SetActive(item.name == tabName);
                activePage = item;
            }
        }
    }
    public void LoadingScreen()
    {

    }
}
