using UnityEngine;
using UnityEngine.Events;

public class PlayerSpawner : MonoBehaviour
{
    public string sceneName;
    public UnityEvent doAfter;
    void Start()
    {
        if(GameManager.inst.lastSceneName == sceneName)
        {
            Player p = FindFirstObjectByType<Player>();
            p.transform.position = transform.position;
            doAfter.Invoke();
        }        
    }
}
