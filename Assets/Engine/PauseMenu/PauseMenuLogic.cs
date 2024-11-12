using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PauseMenuLogic : MonoBehaviour
{
    public List<string> scenesToNotAddMenu = new List<string>()
    {
        "MainMenu",
        "Ending1",
        "Ending2",
        "EndMenu"
    };
    public bool canOpenMenu = true;
    private void Update()
    {
        if(canOpenMenu)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Transform menuPanel = transform.parent.GetChild(1);
                if(menuPanel.gameObject.activeSelf)
                {
                    menuPanel.GetComponent<UIPanelScaler>().Close();
                    if(FindFirstObjectByType<Player>() != null) FindFirstObjectByType<Player>().inMenu = false; 
                }
                else
                {
                    menuPanel.gameObject.SetActive(true);
                    if(FindFirstObjectByType<Player>() != null) FindFirstObjectByType<Player>().inMenu = true; 
                }
            }
        }
    }
}
