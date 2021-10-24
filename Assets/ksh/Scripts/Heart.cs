using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item
{
    public override void Use()
    {
        Character.S.Life++;
        ItemManager.S.heartCount--;
        //Destroy(this.gameObject);
    }
}