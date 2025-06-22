using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NoteNameChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        nameChangerField.text = nameText.text;
        nameChangerField.gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        nameText.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        nameText.fontStyle = FontStyles.Normal;
    }
    TMP_Text nameText;
    public TMP_InputField nameChangerField;
    void Awake()
    {
        nameText = GetComponent<TMP_Text>();
        nameChangerField.onSubmit.AddListener(NameChange);
        nameChangerField.onEndEdit.AddListener(NameChange);
    }

    public void NameChange(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return;
        NotepadManager.Instance.NoteNameChange(name);
        nameChangerField.gameObject.SetActive(false);
    }
    void Reset()
    {
        nameChangerField.gameObject.SetActive(false);
        nameChangerField.text = null;
    }
    void OnDisable()
    {
        Reset();
    }
}
