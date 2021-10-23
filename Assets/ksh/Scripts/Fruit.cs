using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Item
{
    public override void Use()
    {
        Character.S.CurrentState = CharacterState.PowerMode;
        ItemManager.S.fruitCount--;
        Destroy(this.gameObject);
    }
}