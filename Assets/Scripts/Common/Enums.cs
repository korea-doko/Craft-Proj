using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemUpperClassType
{
    Weapons,
    Armors,
    Misc,
}

public enum ItemLowerClassArmors
{
    Armor,
    Boots,
    Helmet,
    Gloves,
    Belt,
    Shield,
    Quiver
}
public enum ItemLowerClassMisc
{
    Ring,
    Amulet
}
public enum ItemLowerClassWeapons
{
    OneHandedSword,
    TwoHandedSword,
    Claw,
    Dagger,
    Staff,
    OneHandedMace,
    TwoHandedMace,
    OneHandedAxe,
    TwoHandedAxe,
    Wand,
    Bow
}

public enum ItemRarity
{
    Normal,
    Magic,
    Rare,
    Unique
}

public enum ModType
{
    Prefix,
    Suffix
}

public enum MenuName
{
    Quest,
    Hero,
    Battle,
    Upgrade,
    Inventory,    
}