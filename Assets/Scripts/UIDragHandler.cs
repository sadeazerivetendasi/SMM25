using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public Canvas canvas;

    public RectTransform canvasRectTransform;
    private Vector3[] canvasPanel = new Vector3[4];
    private Vector3[] windowPanel = new Vector3[4];
    RectTransform dragRectTransform;
    void Awake()
    {
        if (dragRectTransform == null)
            dragRectTransform = transform.parent.GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        dragRectTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        ClampToWindow();
    }

    void ClampToWindow()
    {
        canvasRectTransform.GetWorldCorners(canvasPanel);
        dragRectTransform.GetWorldCorners(windowPanel);
        Vector3 offset = Vector3.zero;
        if (canvasPanel[0].x > windowPanel[0].x)
        {
            offset.x = canvasPanel[0].x - windowPanel[0].x;
        }
        if (canvasPanel[0].y > windowPanel[0].y)
        {
            offset.y = canvasPanel[0].y - windowPanel[0].y;
        }
        if (canvasPanel[2].x < windowPanel[2].x)
        {
            offset.x = canvasPanel[2].x - windowPanel[2].x;
        }
        if (canvasPanel[1].y < windowPanel[1].y)
        {
            offset.y = canvasPanel[1].y - windowPanel[1].y;
        }
        dragRectTransform.position += offset;
    }
}