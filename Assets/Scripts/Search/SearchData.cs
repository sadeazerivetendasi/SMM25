using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class Website
{
    public LocalizedString SectionTitle;
    public LocalizedString SectionInfo;
}


[CreateAssetMenu(fileName = "SearchData", menuName = "Search/SearchData")]
public class SearchData : ScriptableObject
{
    public List<LocalizedString> keywords;
    public LocalizedString Title, Link, Source, Info;
    public List<Website> H;
}