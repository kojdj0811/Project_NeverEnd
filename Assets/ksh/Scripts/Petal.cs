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
        Invoke("PetalDestroy", 5f);
    }

    public override void Use()
    {
        Character.S.shield.Life = 2;
        ItemManager.S.petalCount--;
        Destroy(this.gameObject);
    }

    private void PetalDestroy()
    {
        ItemManager.S.petalCount--;
        Destroy(this.gameObject);
    }
}