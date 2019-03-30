using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override bool Interact(GameObject user)
    {
        return PickUp(user);
    }

    public bool PickUp(GameObject user)
    {
        var character = user.GetComponent<Character>();
        if(character != null && character.inventory.Add(item))
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
