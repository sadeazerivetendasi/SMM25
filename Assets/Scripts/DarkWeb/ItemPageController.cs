using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class ItemPageController : MonoBehaviour
{
    [Foldout("Title")]
    public TMP_Text nameText, typeText, priceText;
    [Foldout("List")]
    public TMP_Text vendorText, categoryText, ratingText, transactionsText, listedText;
    public TMP_Text descriptionText;
}
