using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class InteractionComponent : MonoBehaviour
{
    public UnityEvent DoByClick;
    [SerializeField] private GameObject EIcon;
    private Vector3 myScale;
    private bool playerIn = false;
    public AudioClip clip;
    public void Awake() => myScale = EIcon.transform.localScale;
    public void Start()
    {
        DoByClick.AddListener(() => AudioManager.inst.Play(clip));
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && EIcon.activeInHierarchy) DoByClick.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")) 
        {
            playerIn = true;
            EIcon.SetActive(true);
            EIcon.GetComponent<Transform>().localScale = Vector3.zero;
            EIcon.GetComponent<Transform>().DOScale(myScale,0.2f);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player")) 
        {
            playerIn = false;
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(EIcon.GetComponent<Transform>().DOScale(Vector3.zero,0.2f));
            mySequence.OnComplete(Disable);
        }
    }
    private void Disable()
    {
        if(!playerIn) EIcon.SetActive(false);
    }
}
