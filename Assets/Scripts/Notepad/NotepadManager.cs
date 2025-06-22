using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotepadManager : MonoBehaviour
{
    public static NotepadManager Instance;
    [Header("Note Page")]
    [SerializeField]private Transform noteTransformContent;
    [SerializeField]private GameObject notePrefab;
    [SerializeField]private TMP_Text noteName;
    [SerializeField]private TMP_InputField noteText;
    public List<NoteData> noteDatas;

    [Header("Left/Right Page")]
    [SerializeField]private GameObject leftPanel;
    [SerializeField]private Sprite openSprite, closeSprite;
    [SerializeField]private Image openCloseElement;

    [HideInInspector]public NoteData activeNoteData;
    public GameObject[] Pages;
    void Awake()
    {
        Instance = this;
    }

    public bool SetNoteData(NoteData noteData)
    {
        if (activeNoteData != noteData)
        {
            OpenTab("FirstPage");
            if (activeNoteData != null)
            {
                activeNoteData.Deactivate();
            }
            activeNoteData = noteData;
            activeNoteData.Activate();
            noteName.text = activeNoteData.NoteName;
            noteText.text = activeNoteData.Note;
            OpenTab("NotePage");
            return true;
        }
        else
        {
            activeNoteData.AnimButton(0f,0f);
            activeNoteData = null;
            OpenTab("FirstPage");
            return false;
        }
    }
    public void NoteNameChange(string text)
    {
        activeNoteData.UserChangedName(text);
        noteName.text = text;
    }
    public void NoteContentChange(string text)
    {
        activeNoteData.Note = text;
        activeNoteData.noteText.text = text;
    }
    public void OpenTab(string tabName)
    {
        foreach (GameObject item in Pages)
        {
            item.SetActive(item.name == tabName);
        }
    }
    public void CreateNewNoteData(string text)
    {
        if (noteDatas.Count >= 15) return;
        NoteData noteData = Instantiate(notePrefab, noteTransformContent).GetComponentInChildren<NoteData>();
        if(text != null) noteData.noteText.text = text;
        else noteData.noteText.text = null; 
        noteDatas.Add(noteData);
        SetNoteData(noteData);
    }
    public void RemoveNoteData(string name)
    {
        if (name == "Button")
        {
            noteDatas.Remove(activeNoteData);
            Destroy(activeNoteData.transform.parent.gameObject);
            OpenTab("FirstPage");
        }
        else
        {
            foreach (NoteData item in noteDatas)
            {
                if (item.NoteName == name)
                {
                    noteDatas.Remove(item);
                    Destroy(item.transform.parent.gameObject);
                    OpenTab("FirstPage");
                    break;
                }
            }
        }
    }
    public void RightPanelActivator()
    {
        leftPanel.SetActive(FC());
    }
    private bool FC()
    {
        if (leftPanel.activeSelf)
        {
            openCloseElement.sprite = openSprite;
            return false;
        }
        else
        {
            openCloseElement.sprite = closeSprite;
            return true;
        }
    }
    void OnEnable()
    {
        OpenTab("FirstPage");
    }
}
