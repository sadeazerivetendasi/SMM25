using TMPro;
using UnityEngine;

public class DarkWebEnterInputHandler : MonoBehaviour
{
    TMP_InputField tMP_InputField;
    void Start()
    {
        tMP_InputField = GetComponent<TMP_InputField>();
        tMP_InputField.onSubmit.AddListener(OnSubmit);
    }
    public void OnSubmit(string text)
    {
        if (text == null) return;
        DarkWebManager.Instance.SearchSystem(text);
    }
}
