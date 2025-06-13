using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class EncyclopediaWebsite
{
    public enum BasliqNovu
    {
        Header, Section
    }
    public BasliqNovu basliqNovu;
    public LocalizedString SectionTitle;
    public LocalizedString SectionInfo;
    public bool isFlex;
    public LocalizedString flexTitle;
    public LocalizedString flexInfo;
}


[CreateAssetMenu(fileName = "SearchData", menuName = "Search/SearchData")]
public class SearchData : ScriptableObject
{
    public enum SaytNovu
    {
        Encyclopedia, News, Forum
    }
    public SaytNovu saytNovu;
    public List<LocalizedString> keywords;
    public LocalizedString Title, Link, Source, Info;
    [ShowIf(nameof(IsEncyclopedia))]
    public List<EncyclopediaWebsite> encyclopediaWebsites;

    private bool IsEncyclopedia() => saytNovu == SaytNovu.Encyclopedia;
    private bool IsNews() => saytNovu == SaytNovu.News;
    private bool IsForum() => saytNovu == SaytNovu.Forum;
}