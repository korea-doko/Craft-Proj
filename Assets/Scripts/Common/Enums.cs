using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ModType
{
    None = -1

, AddedPhysicalMinDamage
, AddedPhysicalMaxDamage
, IncreasedPhysicalMinDamage
, IncreasedPhysicalMaxDamage
, AddedFireMinDamage
, AddedFireMaxDamage
, IncreasedFireMinDamge
, IncreasedFireMaxDamage
, AddedColdMinDamage
, AddedColdMaxDamage
, IncreasedColdMinDamage
, IncreasedColdMaxDamage
, AddedLightningMinDamage
, AddedLightningMaxDamage
, IncreasedLightningMinDamage
, IncreasedLightningMaxDamage
, AddedChaosMinDamage
, AddedChaosMaxDamage
, IncreasedChaosMinDamage
, IncreasedChaosMaxDamage
, AddedPhysicalPenetrateRating
, AddedFirePenetrateRating
, AddedColdPenetrateRating
, AddedLightningPenetrateRating
, AddedChaosPenetrateRating
, IncreasedPhysicalPenetrateRating
, IncreasedFirePenetrateRating
, IncreasedColdPenetrateRating
, IncreasedLightningPenetrateRating
, IncreasedChaosPenetrateRating
, IncreasedStunProbability
, IncreasedBleedingProbability
, IncreasedBurningProbability
, IncreasedFreezeProbability
, IncreasedShockProbability
, IncreasedPoisonProbability
, IncreasedStunDuration
, IncreasedBleedingDuration
, IncreasedBurningDuration
, IncreasedFreezeDuration
, IncreasedShockDuration
, IncreasedPoisonDuration
, AddedAttackSpeed
, IncreasedAttackSpeed
, AddedCriticalProbability
, IncreasedCriticalMultiplier
}

public enum ItemUpperClass
{
    Armor,
    Weapon,
    Misc
}

public enum ArmorLowerClass
{
    Helmet,
    BodyArmor,
    Boots,
    Gloves
}
public enum WeaponLowerClass
{
    Sword,
    Mace,
    Axe,
    
    Claw,
    Dagger,

    Bow,
    Wand,   
}
public enum MiscLowerClass
{
    Ring,
    Amulet
}



public enum DamageType
{
    Physical,
    Fire,
    Cold,
    Lightning,
    Chaos
}

public enum ItemRarity
{
    Normal,
    Magic,
    Rare,
    Unique
}

public enum AffixType
{
    Prefix,
    Suffix,
    Implicit,
}

public enum EHeroClass
{
    None = -1,
    Fighter,
    Ranger
}
public enum EEquipParts
{
    Head,               // Helmet
    Body,               // Armor
    WeaponHand,         // Weapon
    GloveHand,          // Gloves           
    Foot,               // Boots
    Neck,               // Amulet              
    Finger              // Ring          
}
public enum MenuName
{
    Upgrade,
    Hero,
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