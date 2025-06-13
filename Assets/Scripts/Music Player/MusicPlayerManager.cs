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
    float[] spectrum = new float[64]; // Minimum 64 required by Unity
    public float updateSpeed = 0.1f;
    
    [Header("Visualizer Tuning")]
    public float responseSpeed = 25f;  // Frame skip kompanzasyonu için artırdık
    public float sensitivity = 35f;    // Daha güçlü tepki için artırdık
    public bool enableVisualizer = true; // Visualizer'ı tamamen aç/kapat
    
    // Ultra optimized - minimize everything
    private float[] targetValues;
    private float[] currentValues;
    private Vector2[] sizeDeltaCache;
    private int barCount;
    private float heightDiff;
    
    // Aggressive throttling for maximum performance
    private int frameSkipCounter = 0;
    private const int FRAME_SKIP = 4; // Her 5. frame'de güncelle (12 FPS visualizer)
    
    [HideInInspector] public MusicPlayerRedirect musicPlayerRedirectActive;
    
    void Awake()
    {
        Instance = this;
        SetupVisualizerArrays();
    }
    
    void SetupVisualizerArrays()
    {
        if (visualizerBar == null || visualizerBar.Count == 0) return;
        
        barCount = visualizerBar.Count;
        targetValues = new float[barCount];
        currentValues = new float[barCount];
        sizeDeltaCache = new Vector2[barCount];
        heightDiff = maxLimit - minLimit;
        
        // Cache original sizes
        for (int i = 0; i < barCount; i++)
        {
            if (visualizerBar[i] != null)
            {
                sizeDeltaCache[i] = visualizerBar[i].sizeDelta;
                currentValues[i] = minLimit;
            }
        }
    }
    
    void Update()
    {
        if (musicPlayerSource.isPlaying)
        {
            // Smart frame skipping with visualizer toggle
            if (enableVisualizer)
            {
                frameSkipCounter++;
                if (frameSkipCounter >= FRAME_SKIP)
                {
                    frameSkipCounter = 0;
                    AudioSpectrumUltraFast();
                }
            }
            GetActiveTime();
        }
    }

    void AudioSpectrumUltraFast()
    {
        if (spectrum == null || barCount == 0) return;
        
        // Get spectrum data - bu satır en ağır işlem!
        musicPlayerSource.GetSpectrumData(spectrum, 0, fftWindow);
        
        // Faster compensation for less frequent updates
        float deltaTime = Time.deltaTime * responseSpeed;
        
        // Process only every 2nd element for even more performance
        int step = 1; // Her element için (1) veya her 2. element için (2)
        int barsToUpdate = Mathf.Min(barCount, 32);
        
        for (int i = 0; i < barsToUpdate; i += step)
        {
            // Use public sensitivity for easy adjustment
            float specValue = spectrum[i] * sensitivity;
            
            // Minimal processing
            specValue = specValue > 1f ? 1f : (specValue < 0.01f ? 0f : specValue);
            
            targetValues[i] = minLimit + heightDiff * specValue;
            
            // Fast lerp
            currentValues[i] += (targetValues[i] - currentValues[i]) * deltaTime;
            
            // Direct assignment
            sizeDeltaCache[i].y = currentValues[i];
            
            if (visualizerBar[i] != null)
                visualizerBar[i].sizeDelta = sizeDeltaCache[i];
        }
    }

    // Original function for compatibility (but optimized)
    public void AudioSpectrum()
    {
        AudioSpectrumUltraFast();
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