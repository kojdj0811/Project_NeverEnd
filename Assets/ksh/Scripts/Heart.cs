using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item
{
    public override void Use()
    {
        ItemManager.S.heartCount--;
        Destroy(this.gameObject);
    }
}