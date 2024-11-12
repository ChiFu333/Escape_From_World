using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    public string triggerName;
    public bool compareToStay = true;
    void Start()
    {
        if(!compareToStay == GameManager.inst.triggers[triggerName]) gameObject.SetActive(false);
    }
}
