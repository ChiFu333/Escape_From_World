using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;
public class DialogueSystem : MonoBehaviour {
    public static DialogueSystem inst { get; private set; }
    
    private float symbolDelay;
    private DialogueSO currentDialogue;
    private int phraseIndex;
    private Phrase currentPhrase;
    private bool isTextAnimationPlaying, isPaused, wasUnpaused;
    public bool isDialogueOngoing { get; private set; }
    public bool isPhraseOngoing { get; private set; }
    //View
    private Canvas dialogueCanvas;
    private GameObject textPanel;
    private TMP_Text text;
    private GameObject leftImageBox, rightImageBox;
    private Image leftImage, rightImage;

    public bool IsFrozen() => isDialogueOngoing || isPhraseOngoing;

    public void Setup(float _symbolDelay, Canvas _dialogueCanvas, GameObject _textPanel, GameObject _leftImageBox, GameObject _rightImageBox) {
        symbolDelay = _symbolDelay;
        dialogueCanvas = _dialogueCanvas;
        textPanel = _textPanel;
        text = textPanel.GetComponentInChildren<TMP_Text>();
        leftImageBox = _leftImageBox; rightImageBox = _rightImageBox;
        leftImage = leftImageBox.transform.GetChild(0).GetComponent<Image>();
        rightImage = rightImageBox.transform.GetChild(0).GetComponent<Image>();

        if (wasUnpaused) {
            ResumeDialogue();
            wasUnpaused = false;
        }
    }

    public void Pause(bool _isPaused) {
        isPaused = _isPaused;
        if (!isPaused) wasUnpaused = true;
    }

    public void SayPhrase(Phrase phrase) {
        if (isDialogueOngoing) return;
        SetUIVisibility(true);
        currentPhrase = phrase;
        isPhraseOngoing = true;
        PlayPhrase();
    }

    public void StartDialogue(DialogueSO dialogue) {
        if(FindFirstObjectByType<Player>() != null) FindFirstObjectByType<Player>().inDialoge = true;
        currentDialogue = dialogue;
        isDialogueOngoing = true;
        SetUIVisibility(true);
        //Next phrase
        phraseIndex = 0;
        PlayNextPhrase();
    }

    public void ResumeDialogue() {
        SetUIVisibility(true);
        PlayNextPhrase();
    }

    private void PlayNextPhrase() {
        if(currentDialogue != null)
        {
            currentPhrase = currentDialogue.phrases[phraseIndex];
        phraseIndex++;
        PlayPhrase();
        }
        else
        {
            SetUIVisibility(false);
        }
    }

    private void PlayPhrase() {
        currentPhrase.precallback.Invoke();
        //Update visuals
        if (currentPhrase.character.onRightSide) {
            rightImage.sprite = currentPhrase.character.face;
        } else {
            leftImage.sprite = currentPhrase.character.face;
        }
        leftImageBox.SetActive(!currentPhrase.character.onRightSide);
        rightImageBox.SetActive(currentPhrase.character.onRightSide);

        text.color = currentPhrase.character.textColor;
        StopAllCoroutines();
        StartCoroutine(WritePhraseText(currentPhrase.text));
    }

    private void SetUIVisibility(bool isVisible) {
        if(dialogueCanvas != null) dialogueCanvas.gameObject.SetActive(isVisible);
        if (!isVisible) {
            
        }
    }

    private void Update() {
        //Single phrase
        if (!isDialogueOngoing && Input.GetMouseButtonDown(0) && currentDialogue == null) {
            if (!isTextAnimationPlaying) {
                //End phrase
                SetUIVisibility(false);
                isTextAnimationPlaying = false;
                isPhraseOngoing = false;
            }
        }
        //Whole dialogue
        if (isDialogueOngoing && Input.GetMouseButtonDown(0) && currentDialogue != null) {
            if (phraseIndex == currentDialogue.phrases.Count) {
                currentPhrase.callback.Invoke();
                if (isPaused) return;
                //End dialogue
                StopAllCoroutines();
                SetUIVisibility(false);
                isDialogueOngoing = false;
                currentDialogue = null;
                if(FindFirstObjectByType<Player>() != null) FindFirstObjectByType<Player>().inDialoge = false;
            } else {
                if (isTextAnimationPlaying) {
                    if (isPaused) return;
                    //Skip text animation
                    StopAllCoroutines();
                    text.text = currentPhrase.text;
                    isTextAnimationPlaying = false;
                } else {
                    currentPhrase.callback.Invoke();
                    if (isPaused) return;
                    PlayNextPhrase();
                }
            }
        }
    }

    public IEnumerator WritePhraseText(string message) {
        int count = 0;
        Timer timer = new Timer();
        isTextAnimationPlaying = true;
        text.gameObject.GetComponent<TextAnimatorPlayer>().ShowText(message);
        while (count < message.Length) {
            count++;
            if (count % 3 == 0) {
                AudioManager.inst.Play(currentPhrase.character.voice);
            }
            timer.SetTime(symbolDelay);
            while (!timer.Execute()) {
                yield return null;
            }
        }
        isTextAnimationPlaying = false;
        isPhraseOngoing = false;
    }

    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(this);
        } else {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ForceEndDialog()
    {
        StopAllCoroutines();
        SetUIVisibility(false);
        isDialogueOngoing = false;
        currentDialogue = null;
        FindFirstObjectByType<Player>().inDialoge = false;
    }
}
