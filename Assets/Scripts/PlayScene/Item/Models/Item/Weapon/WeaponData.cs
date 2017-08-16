using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IWeaponItem : IItemData
{
    WeaponBaseData GetWeaponBase { get; }
}

[System.Serializable]
public class WeaponData : ItemData ,IWeaponItem
{
    [SerializeField] private WeaponBaseData m_baseWeaponData;
    

    public WeaponData(WeaponBaseData _baseData, ItemRarity _rarity) : base(_rarity)
    {
        m_baseWeaponData = _baseData;            
    }
    public WeaponBaseData GetWeaponBase { get { return m_baseWeaponData; } }
  
    
}
