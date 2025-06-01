using TMPro;
using UnityEngine;
using DG.Tweening;

public class TerminalCommandScript : MonoBehaviour
{
    public TMP_Text userCommandText, commandResponseText;
    CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.2f);
    }
    public void SetText(string one, string two)
    {
        userCommandText.text = one;
        commandResponseText.text = two;    
    }
}
