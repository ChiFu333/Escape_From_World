using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Helper", menuName = "ScriptableObjects/Helper")]
public class HelpSO : ScriptableObject
{
    public void PlaySound(AudioClip clip)
    {
        AudioManager.inst.Play(clip);
    }
    public void StartMusic(AudioClip clip)
    {
        AudioManager.inst.PlayMusic(clip);
    }
    public void SetMusicVolume(float volume) => AudioManager.inst.SetMusicVolume(volume);
    public void SetSoundVolume(float volume) => AudioManager.inst.SetSoundVolume(volume);
    public void RemoveMusic(bool a)
    {
        AudioManager.inst.StopMusic(a);
    }
    public void printStuff(string v)
    {
        Debug.Log(v);
    }
    public void PlayDialogue(DialogueSO dialogue)
    {
        DialogueSystem.inst.StartDialogue(dialogue);
    }
    public void LoadScene(string n)
    {
        SceneLoader.inst.LoadScene(n);
    }
    public void ExitGame()
    {
        SceneLoader.inst.ExitGame();
    }
    public void PauseDialoge(bool b)
    {
        DialogueSystem.inst.Pause(b);
    }
    //public void SetTrigger(string n) => ScenesLogicHandler.inst.triggers[n] = true;
    public void EndDialog() => DialogueSystem.inst.ForceEndDialog();   
    public void ChangeTrigger(string n) => GameManager.inst.triggers[n] = true;
    public void DoQuest(string n)
    {
        ChangeTrigger(n);
        QuestSystem.inst.NextQuest();
    }
    public void ChangeSprite(Sprite n) => FindFirstObjectByType<EngingEngine>().UpdateSprite(n);
}
