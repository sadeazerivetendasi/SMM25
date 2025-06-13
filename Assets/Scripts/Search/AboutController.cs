using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class AboutController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text infoText;
    [Foldout("Flex")]
    public TMP_Text flexTitleText;
    [Foldout("Flex")]
    public TMP_Text flexInfoText;
}
