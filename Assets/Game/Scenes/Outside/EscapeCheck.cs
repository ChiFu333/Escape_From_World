using UnityEngine;

public class EscapeCheck : MonoBehaviour
{
    public DialogueSO dont, dogo;
    public void Check()
    {
       if(GameManager.inst.triggers["BeReadyToDeath"]) 
       {
            DialogueSystem.inst.StartDialogue(dogo);
       }
       else
       {
            DialogueSystem.inst.StartDialogue(dont);
       }
    }

}
