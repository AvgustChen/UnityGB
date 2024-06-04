using DG.Tweening;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    public Vector3 originalScale;

    void OnEnable()
    {
        originalScale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(originalScale * 1.2f, 0.5f)
            .SetUpdate(true)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                transform.DOScale(originalScale, 0.5f)
                .SetUpdate(true)
                .SetEase(Ease.OutBounce);
            });
    }
}
