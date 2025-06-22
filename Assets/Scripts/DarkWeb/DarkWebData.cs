using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "NewDarkWebData", menuName = "DarkWeb/DarkWebData", order = 0)]
public class DarkWebData : ScriptableObject
{
    public enum VendorType
    {
        GhostDoc
    };
    public VendorType vendorName;
    public LocalizedString Title, Info, Type, Price;
    public string ratingText, ratingTextTwo;
    public int priceNumber;
    public LocalizedString Category, Transactions, Listed, ItemDescription;
    [Expandable]
    public List<CommentData> customerReviews;
}
