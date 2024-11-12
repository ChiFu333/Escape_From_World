using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem inst; 
    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(gameObject);
        } else {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public int currentQuest = 0;
    public string GetCurrentQuest()
    {
        return quests[currentQuest];
    }
    public void NextQuest()
    {
        currentQuest++;
        FindFirstObjectByType<QuestShower>().UpdateUI();
    }
    public string[] quests = new string[]
    {
        "Проснись и пой",
        "Осмотреть печь",
        "Найти библиотеку",
        "Поработать",
        "Обменять книгу",
        "Отнести домой",
        "Пора спать",
        "Проснись и пой",
        "Поработать",
        "Обменять билет",
        "Утвердить билет",
        "Обменять билет",
        "Оборудовать печку",
        "Пора спать",
        "Проснись и пой",
        "Получить прогноз погоды",
        "Получить посылку",
        "Принять свою судьбу"
    };
}
