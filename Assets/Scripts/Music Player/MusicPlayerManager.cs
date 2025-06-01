using System.Collections.Generic;
using DG.Tweening;
using NUnit.Framework;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class MusicPlayerManager : MonoBehaviour
{
    public static MusicPlayerManager Instance;
    [Header("Music Player")]
    public AudioSource musicPlayerSource;
    public MusicSlider musicSlider;
    public PlayPauseButton playPauseButton;
    public TMP_Text TrackNameText, ArtistNameText;
    public TMP_Text activeTime, endTime;
    [SerializeField] public float minLimit, maxLimit;
    public List<PlaylistMusicButton> musicPlayerDatas;
    public PlaylistMusicButton activeMusicPlayerData;
    int activeIndex;
    [Header("Visualizer")]
    public List<RectTransform> visualizerBar;
    public FFTWindow fftWindow = FFTWindow.Blackman;
    float[] spectrum = new float[64];
    public float updateSpeed = 0.1f;
    [HideInInspector] public MusicPlayerRedirect musicPlayerRedirectActive;
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (musicPlayerSource.isPlaying)
        {
            AudioSpectrum();
            GetActiveTime();
        }
    }

    #region Next,Previos,MusicChange
    public void MusicChange(PlaylistMusicButton playlistMusicButton)
    {
        if (activeMusicPlayerData != playlistMusicButton)
        {
            for (int i = 0; i < musicPlayerDatas.Count; i++)
            {
                var item = musicPlayerDatas[i];
                if (item == playlistMusicButton)
                {
                    MusicChangeIndex(i);
                    break;
                }
            }
        }
    }
    public void NextMusic()
    {
        int nextIndex = activeIndex + 1;
        if (nextIndex > musicPlayerDatas.Count - 1)
        {
            nextIndex = 0;
        }
        MusicChangeIndex(nextIndex);
    }
    public void PreviousMusic()
    {
        int prevIndex = activeIndex - 1;
        if (prevIndex < 0)
        {
            prevIndex = musicPlayerDatas.Count - 1;
        }
        MusicChangeIndex(prevIndex);
    }
    public void MusicChangeIndex(int Index)
    {
        var item = musicPlayerDatas[Index];
        activeIndex = Index;
        if (!musicPlayerSource.isPlaying)
        {
            if (activeMusicPlayerData != null)
            {
                activeMusicPlayerData.Deactivate();
            }
            activeMusicPlayerData = item;
            activeMusicPlayerData.Activate();
            TrackNameText.text = item.musicPlayerData.TrackName;
            ArtistNameText.text = item.musicPlayerData.ArtistName;
            musicPlayerSource.clip = item.musicPlayerData.audioClip;
            musicSlider.SetMinMax(item.musicPlayerData.audioClip);
            musicPlayerSource.volume = 0;
            musicPlayerSource.Play();
            playPauseButton.icon.sprite = playPauseButton.playIcon;
            GetLength();
            musicPlayerSource.DOFade(1f, 0.5f);
        }
        else
        {
            activeMusicPlayerData.Deactivate();
            musicPlayerSource.DOFade(0f, 0.5f).OnComplete(() =>
            {
                activeMusicPlayerData = item;
                activeMusicPlayerData.Activate();
                TrackNameText.text = item.musicPlayerData.TrackName;
                ArtistNameText.text = item.musicPlayerData.ArtistName;
                musicSlider.SetMinMax(item.musicPlayerData.audioClip);
                float length = musicPlayerSource.clip.length;
                musicPlayerSource.Stop();
                musicPlayerSource.clip = item.musicPlayerData.audioClip;
                musicPlayerSource.Play();
                playPauseButton.icon.sprite = playPauseButton.playIcon;
                GetLength();
                musicPlayerSource.DOFade(1f, 0.5f);
            });
        }
    }
    #endregion
    public void AudioSpectrum()
    {
        musicPlayerSource.GetSpectrumData(spectrum, 0, fftWindow);

        for (int i = 0; i < visualizerBar.Count; i++)
        {
            float value = Mathf.Clamp01(spectrum[i] * 20);
            float targetHeight = Mathf.Lerp(minLimit, maxLimit, value);

            RectTransform bar = visualizerBar[i];
            float currentHeight = bar.sizeDelta.y;
            float smoothHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * 10f);

            bar.sizeDelta = new Vector2(bar.sizeDelta.x, smoothHeight);
        }
    }
    public void ChangePage(MusicPlayerRedirect musicPlayerRedirect)
    {
        if (musicPlayerRedirect != musicPlayerRedirectActive)
        {
            if(musicPlayerRedirectActive != null) musicPlayerRedirectActive.Deactivate();
            musicPlayerRedirectActive = musicPlayerRedirect;
            musicPlayerRedirectActive.Activate();
        }
    }
    #region GetTime
    public void GetActiveTime()
    {
        float length = musicPlayerSource.time;
        int minutes = Mathf.FloorToInt(length / 60f);
        int second = Mathf.FloorToInt(length % 60f);
        activeTime.text = string.Format("{0:00}:{1:00}", minutes, second);
    }
    public void GetLength()
    {
        float length = musicPlayerSource.clip.length;
        int minutes = Mathf.FloorToInt(length / 60f);
        int second = Mathf.FloorToInt(length % 60f);
        endTime.text = string.Format("{0:00}:{1:00}", minutes, second);
    }
    #endregion
}