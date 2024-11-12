using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAndDo : MonoBehaviour
{
    public UnityEvent Do;
    private void OnTriggerEnter2D(Collider2D c)
    {
        Do.Invoke();
        Destroy(gameObject);
    }
}
