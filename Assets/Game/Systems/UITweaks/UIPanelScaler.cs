using UnityEngine;
using DG.Tweening;
public class UIPanelScaler : MonoBehaviour
{
    [SerializeField] private AudioClip showSound,disappearSound;
    private void OnEnable()
    {
        AudioManager.inst.Play(showSound);
        GetComponent<RectTransform>().GetChild(0).localScale = Vector3.zero;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(GetComponent<RectTransform>().GetChild(0).DOScale(Vector3.one * 1.2f,0.35f))
            .Append(GetComponent<RectTransform>().GetChild(0).DOScale(Vector3.one,0.2f));
    }

    public void Close()
    {
        AudioManager.inst.Play(disappearSound);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(GetComponent<RectTransform>().GetChild(0).DOScale(Vector3.one * 0f,0.3f));
        mySequence.OnComplete(Disable);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
