using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;

public class NoteData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector] public bool isEdited, hasClicked;
    public TMP_Text nameText, dayText, noteText;
    public RectTransform borderItem;
    public LocalizedString defaultNoteName;
    public string NoteName;
    [TextArea(5, 10)]
    public string Note;

    Tween transformElement, shadowElement, borderElement;

    RectTransform thisRect;
    Vector2 originalVector;
    void Awake()
    {
        thisRect = GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hasClicked)
        {
            AnimButton(1f, 1.2f);
        }
        /*shadowElement = DOTween.To(() => shadowValue, x =>
            {
                shadowValue = x;
                shadowObject.Size = shadowValue;
            }, 6.05f, 0.2f);*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!hasClicked)
        {
            Deactivate();
        }
        /*shadowElement = DOTween.To(() => shadowValue, x =>
        {
            shadowValue = x;
            shadowObject.Size = shadowValue;
        }, 1.5f, 0.2f);*/
    }
    public void Activate()
    {
        AnimButton(1f, 1.2f);
        hasClicked = true;
    }
    public void Deactivate()
    {
        AnimButton(-1f, 0f);
        hasClicked = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        hasClicked = NotepadManager.Instance.SetNoteData(this);
    }
    public void AnimButton(float y, float borderScaleY)
    {
        transformElement = thisRect.DOAnchorPosY(y, 0.2f);
        borderElement = borderItem.DOScaleY(borderScaleY, 0.2f);
    }
    void OnEnable()
    {
        if (!isEdited)
            defaultNoteName.StringChanged += OnLocalizedNameChanged;
    }
    void OnDisable()
    {
        if (NotepadManager.Instance.activeNoteData == this) return;
        shadowElement?.Kill();
        transformElement?.Kill();
        borderElement?.Kill();
        //shadowObject.Size = 0;
        thisRect.anchoredPosition = new Vector2(thisRect.anchoredPosition.x, -1);
        borderItem.localScale = new Vector2(borderItem.localScale.x, 0f);
    }
    private void OnLocalizedNameChanged(string newName)
    {
        if (!isEdited)
        {
            NoteName = newName;
            nameText.text = newName;
        }
    }

    public void UserChangedName(string newName)
    {
        isEdited = true;

        // Dəyişiklik oldu, artıq lokalizasiya dəyişikliyinə qulaq asmırıq
        defaultNoteName.StringChanged -= OnLocalizedNameChanged;

        NoteName = newName;
        nameText.text = newName;
    }
}
