using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneOrganizer : MonoBehaviour {
    [Header("Holders")]
    [SerializeField] private Transform managerHolder;
    [SerializeField] private Transform uiHolder;
    [Header("Dialogue")]
    [SerializeField] private float symbolDelay = 0.02f;
    [SerializeField] private Canvas inventoryCanvasPrefab;
    [SerializeField] private GameObject dialogueCanvasPrefab;
    [SerializeField] private GameObject textPanelPrefab;
    [Header("SceneTransition")]
    [SerializeField] private GameObject transitionPrefab;
    [Header("Music Manager")]
    [SerializeField] private GameObject audioManager;
    [SerializeField] private AudioClip startMusic;
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject questText;
    [SerializeField] private GameObject itemHolder;
    private void Awake()
    {
        InstantiateManager("Game manager", typeof(GameManager),false);
        InstantiateManager("Quest system", typeof(QuestSystem),false);
    }
    private void Start() {
        
        SetupDialogueSystem();
        Instantiate(audioManager);
        AudioManager.inst.PlayMusic(startMusic);
        
        SetupPauseMenu();
        SetupSceneTransition();
    }


    private void SetupSceneTransition()
    {
        Canvas c = Instantiate(transitionPrefab, uiHolder).GetComponent<Canvas>();
        c.worldCamera = Camera.main;       
    }
    private void SetupDialogueSystem() {
        //Create UI
        Canvas dialogueCanvas = CreateCanvas("Dialogue canvas", inventoryCanvasPrefab.gameObject);
        Instantiate(dialogueCanvasPrefab, dialogueCanvas.transform);
        //Hide created UI
        dialogueCanvas.gameObject.SetActive(false);
        //Create manager
        DialogueSystem dialogueSystem;
        if (DialogueSystem.inst == null) {
            dialogueSystem = InstantiateManager("Dialogue system", typeof(DialogueSystem),false).GetComponent<DialogueSystem>();
        } else {
            dialogueSystem = DialogueSystem.inst;
        }
        GameObject textPanel = dialogueCanvas.transform.GetChild(0).GetChild(0).gameObject;
        
        GameObject leftImageBox = dialogueCanvas.transform.GetChild(0).GetChild(1).gameObject;
        GameObject rightImageBox = dialogueCanvas.transform.GetChild(0).GetChild(2).gameObject;
        dialogueSystem.Setup(symbolDelay, dialogueCanvas, textPanel, leftImageBox, rightImageBox);
    }
    private void SetupPauseMenu()
    {
        if(!menuButton.GetComponent<PauseMenuLogic>().scenesToNotAddMenu.Contains(SceneManager.GetActiveScene().name))
        {
        GameObject buttonToMenu = Instantiate(menuButton, uiHolder.GetChild(0));
        GameObject pausePanel = Instantiate(pauseMenu, uiHolder.GetChild(0));
        Instantiate(questText, uiHolder.GetChild(0));
        Instantiate(itemHolder, uiHolder.GetChild(0));
        buttonToMenu.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => pausePanel.SetActive(true)));
        buttonToMenu.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(
            () => 
            {
                if(FindFirstObjectByType<Player>() != null) FindFirstObjectByType<Player>().inMenu = true;

            }
            ));
        pausePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => {if(FindFirstObjectByType<Player>() != null) FindFirstObjectByType<Player>().inMenu = false;}));
        }
    }
    private Canvas CreateCanvas(string name, GameObject canvasPrefab) {
        Canvas canvas = Instantiate(canvasPrefab, uiHolder).GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.gameObject.name = name;
        return canvas;
    }

    private GameObject InstantiateManager(string name, Type componentType, bool doParent = true) {
        GameObject newObject = new GameObject(name);
        if (doParent) newObject.transform.parent = managerHolder;
        newObject.AddComponent(componentType);
        return newObject;
    }
}
