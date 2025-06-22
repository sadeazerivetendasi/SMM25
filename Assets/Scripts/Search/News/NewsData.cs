using UnityEngine;
using UnityEngine.Localization;


[CreateAssetMenu(fileName = "NewNewsData", menuName = "Search/NewsData", order = 0)]
public class NewsData : ScriptableObject {
    public Sprite Image;
    public LocalizedString Title, Creator, Content;
}
