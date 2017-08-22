using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemUpperClassType
{
    Armors,
    Weapons,
    Misc
}

public enum DamageType
{
    Physical,
    Fire,
    Cold,
    Lightning,
    Chaos
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
public enum ModFuncName
{
    AddedPhysicalDamage,
    IncreasePhysicalDamage,

    AddedFireDamage,
    IncreaseFireDamage,

    AddedColdDamage,
    IncreaseColdDamage,

    AddedLightningDamage,
    IncreaseLightningDamage,
}
public enum ModCategory
{
    Weapon,
    Armor,
    Misc
}

public enum MenuName
{
    Upgrade,
    Traveller,
    Quest,
    Guild,
    Store,    
}

/// <summary>
///  Reinforcement/어규멘테이션 ||
///  MagicPower/리갈 ||
///  Unholy/엑잘 || 
///  BlackSmith/트랜스뮤트 ||
///  Luck/찬스 ||
///  Wizard/알케미 ||
///  Alteration/알터레이션 ||
///  Chaos/카오스 ||
///  Purification/스코어링 ||
///  Void/애널먼트 ||
///  Divine/디바인 ||
///  Curruption/바알
/// </summary>
public enum RuneName
{
    Reinforcement,  //어규멘테이션
    MagicPower,     //리갈
    Unholy,         //엑잘

    BlackSmith,     //트랜스뮤트
    Luck,           //찬스
    Wizard,         //알케미

    Alteration,     //알터레이션
    Chaos,          //카오스
    Purification,   //스코어링
    Void,           //애널먼트
    Divine,         //디바인
    Curruption      //바알
}