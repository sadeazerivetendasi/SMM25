using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayPauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector] public Image icon;
    RectTransform rectTransform;
    Vector2 originalVector;
    public Sprite playIcon, pauseIcon;
    void Start()
    {
        icon = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        originalVector = rectTransform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.DOScale(originalVector * 1.05f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.DOScale(originalVector * 1f, 0.2f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayPauseToggle();
    }
    public void PlayPauseToggle()
    {
        if (MusicPlayerManager.Instance.musicPlayerSource.clip == null)
        {
            MusicPlayerManager.Instance.MusicChange(MusicPlayerManager.Instance.musicPlayerDatas[0]);
            return;
        }
        if (MusicPlayerManager.Instance.musicPlayerSource.isPlaying)
        {
            MusicPlayerManager.Instance.musicPlayerSource.Pause();
            icon.sprite = pauseIcon;
        }
        else
        {
            MusicPlayerManager.Instance.musicPlayerSource.Play();
            icon.sprite = playIcon;
        }
    }


}
