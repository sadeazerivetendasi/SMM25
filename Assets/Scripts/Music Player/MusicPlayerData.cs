using UnityEngine;

[CreateAssetMenu(fileName = "NewMusicData", menuName = "MusicPlayerData")]
public class MusicPlayerData : ScriptableObject {
    public string TrackName, ArtistName;
    public AudioClip audioClip;
}
