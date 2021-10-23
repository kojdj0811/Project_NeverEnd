using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petal : Item
{
    [SerializeField]
    private List<Sprite> images = new List<Sprite>();

    void Start()
    {
        spriteRenderer.sprite = images[Random.Range(0, images.Count)];
        Debug.Log(spriteRenderer.sprite.name);
    }

    public override void Use()
    {
        Character.S.shield.Life = 2;
        Destroy(this.gameObject);
    }
}