using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class ButtonJuicer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Scale Punch")]
    [SerializeField] private float punchScaleAmount = -0.1f;
    [SerializeField] private float duration = 0.1f; 

    [Header("Color Flash (Optional)")]
    [SerializeField] private Image targetImage;
    [SerializeField] private Color flashColor = Color.lightGray;
    
    private Vector3 originalScale;
    private Color originalColor;
    private Tween punchTween;
    private Tween colorTween;

    void Awake()
    {
        originalScale = transform.localScale;
        if (targetImage == null) targetImage = GetComponent<Image>();
        if (targetImage != null) originalColor = targetImage.color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (punchTween != null) punchTween.Kill(true);
        if (colorTween != null) colorTween.Kill(true);

        transform.localScale = originalScale;
        punchTween = transform.DOPunchScale(new Vector3(punchScaleAmount, punchScaleAmount, punchScaleAmount), duration, 0, 0);

        if (targetImage != null)
        {
            colorTween = targetImage.DOColor(flashColor, duration).SetLoops(2, LoopType.Yoyo);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(originalScale, 0.1f);
    }

    void OnDestroy()
    {
        transform.DOKill();
        if (targetImage != null) targetImage.DOKill();
    }
}