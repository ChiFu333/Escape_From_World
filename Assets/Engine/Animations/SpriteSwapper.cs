using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwapper : MonoBehaviour
{
    [SerializeField] private static float timeToSwap = 0.8f;
    public Sprite[] sprites = new Sprite[2];
    private SpriteRenderer SR;
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        StartCoroutine(Swapper());
    }

    // Update is called once per frame
    public void UpdateSprite(Sprite one, Sprite two)
    {
        StopAllCoroutines();
        sprites[0] = one;
        sprites[1] = two;
        if(SR == null) SR = GetComponent<SpriteRenderer>();
        SR.sprite = one;
        StartCoroutine(Swapper());
    }
    public IEnumerator Swapper()
    {
        SR.sprite = sprites[0];
        yield return new WaitForSeconds(timeToSwap);
        SR.sprite = sprites[1];
        yield return new WaitForSeconds(timeToSwap);
        StartCoroutine(Swapper());
    }
}
