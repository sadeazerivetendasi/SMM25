using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    AudioSource audioSource;
    Slider musicSlider;
    bool isDragging;
    void Start()
    {
        musicSlider = GetComponent<Slider>();
        audioSource = MusicPlayerManager.Instance.musicPlayerSource;
        musicSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    void Update()
    {
        if (!isDragging && audioSource.isPlaying)
        {
            musicSlider.value = audioSource.time;
        }
    }
    public void SetMinMax(AudioClip audioClip)
    {
        musicSlider.minValue = 0;
        musicSlider.maxValue = audioClip.length;
    }
    private void OnSliderValueChanged(float value)
    {
        if (isDragging)
        {
            audioSource.time = value;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

}
