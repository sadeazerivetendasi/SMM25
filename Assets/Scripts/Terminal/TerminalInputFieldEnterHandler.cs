using TMPro;
using UnityEngine;

public class TerminalInputFieldEnterHandler : MonoBehaviour
{
    public TMP_Text fileDirectory;
    TMP_InputField tMP_InputField;
    void Start()
    {
        tMP_InputField = GetComponent<TMP_InputField>();
        tMP_InputField.onSubmit.AddListener(OnSubmit);
    }
    public void OnSubmit(string text)
    {
        if (text == null) return;
        TerminalManager.Instance.NewCommand(fileDirectory + text);
    }
}
