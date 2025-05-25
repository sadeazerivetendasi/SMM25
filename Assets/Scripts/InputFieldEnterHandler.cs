using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InputFieldEnterHandler : MonoBehaviour
{
    TMP_InputField inputField;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(OnSubmit);
    }
    public void OnSubmit(string text)
    {
        if (text == null) return;    
        SearchManager.Instance.SearchSystem(text);
    }
    public void Submit()
    {
        if (inputField.text == null) return;    
        SearchManager.Instance.SearchSystem(inputField.text);
    }
}
