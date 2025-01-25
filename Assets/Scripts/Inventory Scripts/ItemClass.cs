using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This makes a class of different scriptable objects
// We only had one, but could have more
public abstract class ItemClass : ScriptableObject
{
    [Header("Item")]
    public string itemName;
    public Sprite itemIcon;

    public abstract WeaponClass GetItem();
}
