using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private AnimationDataSO standing, walking, thinking;
    private AnimationController playerAnim;
    private float movement = 0;
    private bool isWalking = false;
    public bool isThinking = false;
    private void Start() => playerAnim = GetComponentInChildren<AnimationController>();
    private void Update()
    {
        if(!FindFirstObjectByType<Player>().inDialoge && isThinking)
        {
            isThinking = false;
            playerAnim.SetAnimation(standing);
            isWalking = false;
        }
        if(!FindFirstObjectByType<Player>().inDialoge && !isThinking)
        {
            if(movement != 0) playerAnim.SetFlip(movement < 0);
            if(!isWalking && movement != 0)
            {
                playerAnim.SetAnimation(walking);
                isWalking = true;
                
            }
            else if(isWalking && movement == 0)
            {
                playerAnim.SetAnimation(standing);
                isWalking = false;
            }
        }
        else if(FindFirstObjectByType<Player>().inDialoge && !isThinking)
        {
            playerAnim.SetAnimation(thinking);
            isThinking = true;
        }
    }
    void FixedUpdate()
    {
        movement = Input.GetAxisRaw("Horizontal");
    }
}
