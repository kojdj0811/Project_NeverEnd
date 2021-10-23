using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // public Sprite[] images;
    public string name;
    public float spawnTime;

    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D collider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
    }
}