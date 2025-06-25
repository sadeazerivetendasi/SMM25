using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization;

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
    [Expandable]
    public List<WikipediaData> encyclopediaWebsites;
    [ShowIf(nameof(IsForum))]
    public LocalizedString forumTitle, forumCreated, forumInfo, forumCatalogue;
    [ShowIf(nameof(IsForum))]
    [Expandable]
    public List<ForumData> forumDatas;
    [ShowIf(nameof(IsNews))]
    [Expandable]
    public NewsData newsData;
    [SerializeField] private bool _isBookmark;
    public bool IsBookmark { get => _isBookmark; set => _isBookmark = value; }
    private bool IsEncyclopedia() => saytNovu == SaytNovu.Encyclopedia;
    private bool IsNews() => saytNovu == SaytNovu.News;
    private bool IsForum() => saytNovu == SaytNovu.Forum;
}