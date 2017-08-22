using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public interface IWeaponItem : IItemData,IDamage
{
    WeaponBaseData GetWeaponBase { get; }
}

[System.Serializable]
public class WeaponData : ItemData ,IWeaponItem
{
   
    public WeaponData( WeaponBaseData _weaponBaseData, ItemRarity _rarity) : base(_rarity)
    {
        m_itemBaseData = _weaponBaseData;        
    }
    
    public WeaponBaseData GetWeaponBase { get { return (WeaponBaseData)m_itemBaseData; } }

    public Vector2 GetPhysicalDamage
    {
        get
        {
            return GetWeaponBase.GetDamage.GetPhysicalDamage;
        }
    }
    public Vector2 GetColdDamage
    {
        get
        {
            return GetWeaponBase.GetDamage.GetColdDamage;
        }
    }
    public Vector2 GetFireDamage
    {
        get
        {
            return GetWeaponBase.GetDamage.GetFireDamage;
        }
    }
    public Vector2 GetLightningDamage
    {
        get
        {
            return GetWeaponBase.GetDamage.GetLightningDamage;
        }
    }
    public Vector2 GetChaosDamage
    {
        get
        {
            return GetWeaponBase.GetDamage.GetChaosDamage;
        }
    }
    public float GetAverageDamage
    {
        get
        {
            return GetWeaponBase.GetDamage.GetAverageDamage;
        }
    }

}
