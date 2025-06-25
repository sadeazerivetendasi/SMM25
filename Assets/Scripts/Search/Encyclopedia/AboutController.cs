using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class AboutController : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text infoText;
    [Foldout("Flex")]
    [SerializeField] private TMP_Text flexTitleText;
    [Foldout("Flex")]
    [SerializeField] private TMP_Text flexInfoText;

    public void SetTitleText(string localizedtext)
    {
        titleText.text = localizedtext;
    }
    public void SetInfoText(string localizedtext)
    {
        infoText.text = localizedtext;
    }
    public void SetFlexTitleText(string localizedtext)
    {
        flexTitleText.text = localizedtext;
    }
    public void SetFlexInfoText(string localizedtext)
    {
        flexInfoText.text = localizedtext;
    }
}
