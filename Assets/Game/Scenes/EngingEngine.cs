using UnityEngine;

public class EngingEngine : MonoBehaviour
{
    public DialogueSO dialog;
    public SpriteRenderer render;
    void Start()
    {
        AudioManager.inst.StopMusic(true);
        DialogueSystem.inst.StartDialogue(dialog);
    }

    public void UpdateSprite(Sprite s) => render.sprite = s;
}
