using System;
using DG.Tweening;
using LeTai.TrueShadow;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DesktopappBarAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject appBar;
    [Header("Shadow")]
    float shadowValue;
    public float shadowStartValue, shadowEndValue, secondTime;
    public UnityEvent hadiseIcra;
    [Header("iconColor")]
    public Image appIcon;
    public Color normalColor;
    public Color hoverColor;
    public float colorAnimationTime;
    Vector3 originalVector;
    Tween shadowElement;
    TrueShadow trueShadow;
    bool isHovered, isClicking;
    void Start()
    {
        trueShadow = GetComponent<TrueShadow>();
        originalVector = appBar.transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        if (isClicking) return;
        AnimateScale(originalVector * 1.1f, 0.2f, shadowEndValue);
        AnimateIcon(hoverColor,colorAnimationTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        if (isClicking) return;
        AnimateScale(originalVector, 0.2f, shadowStartValue);
        AnimateIcon(normalColor,colorAnimationTime);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        isClicking = true;
        Sequence clickSequence = DOTween.Sequence();
        clickSequence.Append(appBar.transform.DOScale(originalVector * 0.9f, 0.1f));
        clickSequence.AppendCallback(() =>
        {
            hadiseIcra.Invoke();
        });
        clickSequence.Append(appBar.transform.DOScale(isHovered ? originalVector * 1.1f : originalVector, 0.1f)).OnComplete(() =>
        {
            isClicking = false;
            if (!isHovered)
            {
                AnimateScale(originalVector, 0.2f, shadowStartValue);
                AnimateIcon(normalColor,colorAnimationTime);
            }
        });
    }
    void AnimateScale(Vector3 targetScale, float duration, float targetShadowValue)
    {
        appBar.transform.DOKill();
        shadowElement?.Kill();
        appBar.transform.DOScale(targetScale, duration).SetEase(Ease.OutQuad);
        shadowElement = DOTween.To(() => shadowValue, x =>
        {
            shadowValue = x;
            trueShadow.Size = shadowValue;
        }, targetShadowValue, secondTime);
    }
    void AnimateIcon(Color color,float time)
    {
        appIcon.DOColor(color,time);
    }
}
