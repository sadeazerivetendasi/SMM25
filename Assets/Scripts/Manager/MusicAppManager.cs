using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

[System.Serializable]
public class NamedClip
{
    public string key;
    public AudioClip clip;
}
public class MusicAppManager : MonoBehaviour
{
    public static MusicAppManager Instance;

    public AudioSource musicSource;
    public float fadeDuration = 1f;

    [Header("Music List")]
    public List<NamedClip> musicList;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void PlayMusicWithFade(string key)
    {
        AudioClip newClip = GetClipByKey(key);
        if (newClip == null) return;

        if (musicSource.clip == newClip && musicSource.isPlaying)
            return;

        Sequence seq = DOTween.Sequence();

        if (musicSource.isPlaying)
        {
            seq.Append(musicSource.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                musicSource.Stop();
                musicSource.clip = newClip;
                musicSource.volume = 0f;
                musicSource.Play();
            }));

            seq.Append(musicSource.DOFade(1f, fadeDuration));
        }
        else
        {
            musicSource.clip = newClip;
            musicSource.volume = 0f;
            musicSource.Play();

            musicSource.DOFade(1f, fadeDuration);
        }
    }

    private AudioClip GetClipByKey(string key)
    {
        foreach (var item in musicList)
        {
            if (item.key == key)
                return item.clip;
        }
        return null;
    }
}
