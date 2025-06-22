using TMPro;
using UnityEngine;

public class CommentController : MonoBehaviour
{
    public TMP_Text customerText, commentText, dateText;
    public GameObject[] starObjects;
    public void StarSystem(int requiredStar)
    {
        int b = 0;
        foreach (GameObject item in starObjects)
        {
            item.SetActive(b < requiredStar);
            b += 1;
        }
    }
}
