using UnityEngine;

public class HistorySystem : MonoBehaviour
{
    public Transform historyContainer;
    public GameObject historyPrefab;
    public void CreateHistory(string text)
    {
        HistoryRedirect newHistory = Instantiate(historyPrefab, historyContainer).GetComponent<HistoryRedirect>();
        newHistory.SetRedirectText(text);
    }
}
