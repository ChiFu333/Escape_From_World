using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Profiling.LowLevel;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    private float movement = 0;
    public bool trispeed = false;
    private Rigidbody2D body;
    Vector3 gravityVelocity = Vector3.zero;
    public bool inDialoge = false;
    public bool inMenu = false;
    public AudioClip inHouse, inSnow;
    public bool insideHouse = true;
    private Timer walkTimer = new Timer();
    public LayerMask ground;
    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
        walkTimer.SetFrequency(1.2f);
        if(trispeed) speed *= 3;
    }
    private void Update()
    {
        Vector3 gravityDown = -transform.up * gravity;

        Vector3 offset = speed * movement * transform.right;
        gravityVelocity = (Vector3)gravityDown;
        if(!inDialoge && !inMenu)
        {
            body.MovePosition(transform.position + offset + gravityVelocity);

            if(walkTimer.Execute())
            {
                if(offset != Vector3.zero)
                {
                    walkTimer.SetFrequency(1.2f);
                    AudioManager.inst.Play(insideHouse ? inHouse : inSnow);
                }
            }
        }
    }
    void FixedUpdate()
    {
        movement = Input.GetAxisRaw("Horizontal"); 
    }
}