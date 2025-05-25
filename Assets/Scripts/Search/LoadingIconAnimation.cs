using DG.Tweening;
using UnityEngine;

public class LoadingIconAnimation : MonoBehaviour
{
    public float transitionTime;
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 360), transitionTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
    }
}
