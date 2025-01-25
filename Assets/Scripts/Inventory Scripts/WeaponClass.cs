using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// sub class of the item class
// could have had more, like foods that give health

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Weapon")]

public class WeaponClass : ItemClass
{
    [Header("Weapon")]
    public WeaponType weaponType;

    public enum WeaponType
    {
        gun,
        knife,
        icePick
    }
    public override WeaponClass GetItem() {return this;}
}
