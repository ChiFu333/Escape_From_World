using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonScaling : MonoBehaviour
{
    [SerializeField] private AudioClip clickIn, normalClick;
    [SerializeField] public UnityEvent OnClick;
    void Start()
    {
        if(GetComponent<EventTrigger>() == null) gameObject.AddComponent<EventTrigger>();
        EventTrigger et = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((some) => {FadeInUI();});
        et.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((some) => {FadeOutUI();});
        et.triggers.Add(entry);
        
        Image im = gameObject.AddComponent<Image>();
        im.color = new Color(0, 0, 0, 0);

        Button but = GetComponent<RectTransform>().GetChild(0).GetComponent<Button>();
        if(but != null) 
        {
            but.onClick.AddListener(new UnityEngine.Events.UnityAction(() => {PlayClick();}));
            but.onClick.AddListener(OnClick.Invoke);
        }
    }
    public void PlayClick()
    {
        AudioManager.inst.Play(normalClick);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(GetComponent<RectTransform>().GetChild(0).DOScale(Vector3.one * 0.6f,0.1f))
        .Append(GetComponent<RectTransform>().GetChild(0).DOScale(Vector3.one * 0.8f,0.1f));
    }
    public void FadeInUI()
    {
        AudioManager.inst.Play(clickIn);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(GetComponent<RectTransform>().GetChild(0).DOScale(Vector3.one * 0.7f,0.1f))
        .Append(GetComponent<RectTransform>().GetChild(0).DOScale(Vector3.one * 0.8f,0.2f));
    }
    public void FadeOutUI()
    {
        GetComponent<RectTransform>().GetChild(0).DOScale(Vector3.one, 0.3f);
    }
}
