using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "NewWikipediaData", menuName = "Search/WikipediaData", order = 0)]
public class WikipediaData : ScriptableObject {
    public enum BasliqNovu
    {
        Header, Section
    }
    public BasliqNovu basliqNovu;
    public LocalizedString SectionTitle;
    public LocalizedString SectionInfo;
    public bool isFlex;
    [ShowIf("isFlex")]
    public LocalizedString flexTitle;
    [ShowIf("isFlex")]
    public LocalizedString flexInfo;   
}
