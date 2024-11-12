using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(this);
        } else {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public string lastSceneName;
    public Dictionary<string, bool> triggers = new Dictionary<string, bool>()
    {
        {"Day1", false},
        
        {"WatchOven1", false},
        {"SpeakWithLibrary1", false},
        {"Work1", false},
        {"GotBook", false},
        {"PlacedBook", false},
        
        {"Day2", false},

        {"Work2", false},
        {"TryGive", false},
        {"GotShamp", false},
        {"GotHammer", false},
        {"BuildOven", false},
        
        {"Day3", false},

        {"TakeReport", false},
        {"GotTiket", false},
        {"BeReadyToDeath", false},
    };
}