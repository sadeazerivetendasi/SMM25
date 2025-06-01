using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaylistMusicButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Image backgroundImage;
    public MusicPlayerData musicPlayerData;
    public TMP_Text TrackNameText, ArtistNameText, endingTimeText, orderText;
    public Color inActiveColor, hoverColor;
    public RectTransform Border;
    bool isClicked;
    void Start()
    {
        backgroundImage = GetComponent<Image>();
        MusicPlayerManager.Instance.musicPlayerDatas.Add(this);
        orderText.text = string.Format("{0:00}",MusicPlayerManager.Instance.musicPlayerDatas.IndexOf(this) + 1);
        GetLength();
    }
    public void Activate()
    {
        backgroundImage.DOColor(hoverColor, 0.2f);
        Border.DOSizeDelta(new Vector2(4, Border.sizeDelta.y), 0.5f).SetEase(Ease.InSine);
        isClicked = true;
    }
    public void Deactivate()
    {
        backgroundImage.DOColor(inActiveColor, 0.2f);
        Border.DOSizeDelta(new Vector2(0, Border.sizeDelta.y), 0.5f).SetEase(Ease.InSine);
        isClicked = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked) backgroundImage.DOColor(hoverColor, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked) backgroundImage.DOColor(inActiveColor, 0.2f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MusicPlayerManager.Instance.MusicChange(this);
    }
    public void GetLength()
    {
        TrackNameText.text = musicPlayerData.TrackName;
        ArtistNameText.text = musicPlayerData.ArtistName;
        #region GetLength
        float length = musicPlayerData.audioClip.length;
        int minutes = Mathf.FloorToInt(length / 60f);
        int second = Mathf.FloorToInt(length % 60f);
        #endregion
        endingTimeText.text = string.Format("{0:00}:{1:00}", minutes, second);
    }
}
