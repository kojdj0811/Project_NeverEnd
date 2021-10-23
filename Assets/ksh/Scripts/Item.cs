using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string name;
    public float spawnTime;

    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D collider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bird")
            Use();
    }

    public virtual void Use() {}
}