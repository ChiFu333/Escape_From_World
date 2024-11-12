using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestShower : MonoBehaviour
{
    void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        GetComponent<TMP_Text>().text = QuestSystem.inst.GetCurrentQuest();
    }
}
