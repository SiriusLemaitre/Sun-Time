using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextJuicer : MonoBehaviour
{
    [SerializeField] private float punchScale = 0.2f;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private int elasticity = 5;

    private TextMeshProUGUI textComponent;
    private Vector3 originalScale;
    private Tween juiceTween; 

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;
    }

    public void JuiceText()
    {
        if (juiceTween != null) juiceTween.Kill(true);

        transform.localScale = originalScale;
        juiceTween = transform.DOPunchScale(new Vector3(punchScale, punchScale, punchScale), duration, elasticity, 0.5f);
    }

    void OnDestroy()
    {
        transform.DOKill();
    }
}