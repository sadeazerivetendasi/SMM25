using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class ChatDocDownloader : MonoBehaviour
{
    private LocalizedString fileName;
    [Header("Color")]
    [SerializeField] private Color disabledColor;
    [SerializeField] private Color activeColor, hoverColor;

    [Header("Image")]
    [SerializeField] private Image downloadImage;
    [SerializeField] private Image loadingSlider;

    [Header("Text")]
    [SerializeField] private TMP_Text fileNameText;

    [Header("Objects")]
    [SerializeField] private GameObject downloadObject;
    [SerializeField] private GameObject loadingObject, verifiedImage;

    [Header("Buttons")]
    [SerializeField] private Button downloadButton;
    private Sequence activeSequence;
    void Start()
    {
        activeSequence = DOTween.Sequence();
        downloadImage.color = disabledColor;
        fileNameText.color = disabledColor;
        downloadButton.onClick.AddListener(DownloadFile);
    }
    private void DownloadFile()
    {
        if (activeSequence != null && activeSequence.IsActive()) activeSequence.Kill();
        downloadObject.SetActive(false);
        verifiedImage.SetActive(false);
        loadingObject.SetActive(true);
        activeSequence = DOTween.Sequence(); // Əgər əvvəldən yaradılıbsa reset etmək vacibdir
        activeSequence.Append(loadingSlider.DOFillAmount(Random.Range(0.0f, 0.9f), 1f));
        activeSequence.Append(loadingSlider.DOFillAmount(1f, 0.5f));
        activeSequence.AppendCallback(() => verifiedImage.SetActive(true));
        activeSequence.AppendInterval(1f);
        activeSequence.AppendCallback(() =>
        {
            downloadObject.SetActive(false);
            loadingObject.SetActive(false);
            downloadImage.color = activeColor;
            fileNameText.color = activeColor;
        });
    }
    public void SetFileName(LocalizedString localizedString)
    {
        fileName = localizedString;
        fileName.StringChanged += SetText;
    }
    private void SetText(string localizedtext)
    {
        fileNameText.text = localizedtext;
    }
    void OnDisable()
    {
        activeSequence?.Kill();
    }
    void OnDestroy()
    {
        activeSequence?.Kill();
        fileName.StringChanged -= SetText;
    }
}
