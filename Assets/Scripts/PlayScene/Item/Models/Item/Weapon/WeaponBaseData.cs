using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IWeaponBaseItem : IItemBaseData
{
    ItemLowerClassWeapons GetItemLowerClassWeapon { get; }
    Damage GetDamage { get; }
    float GetAttackSpeed { get; }

    float GetAllDPS { get; }
    float GetPhysicalDPS { get; }
    float GetFireDPS { get; }
    float GetColdDPS { get; }
    float GetLightningDPS { get; }
}

[System.Serializable]
public class WeaponBaseData : ItemBaseData, IWeaponBaseItem
{
    [SerializeField] private ItemLowerClassWeapons m_lowerClassName;
    [SerializeField] private Damage m_damage;
    [SerializeField] private float m_attackSpeed;

    public WeaponBaseData(int _id, ItemLowerClassWeapons _type, int _minDamage, int _maxDamage,
        float _attackSpeed) : base(_id)
    {
        m_upperClassName = ItemUpperClassType.Weapons;
        m_lowerClassName = _type;
        m_damage = new Damage(new Vector2(_minDamage, _maxDamage), Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);
        m_attackSpeed = _attackSpeed;
    }

    public ItemLowerClassWeapons GetItemLowerClassWeapon
    {
        get
        {
            return m_lowerClassName;
        }
    }
    public float GetAttackSpeed
    {
        get { return m_attackSpeed; }
    }
    public Damage GetDamage
    {
        get
        {
            return m_damage;
        }
    }
    public float GetAllDPS
    {
        get
        {
            float dps = m_damage.GetAllDamage * m_attackSpeed;
            return dps;
        }
    }
    public float GetPhysicalDPS
    {
        get
        {
            float dps = m_damage.GetPhysicalDamage * m_attackSpeed;
            return dps;
        }
    }
    public float GetFireDPS
    {
        get
        {
            float dps = m_damage.GetFireDamage * m_attackSpeed;
            return dps;
        }
    }    
    public float GetColdDPS
    {
        get
        {
            float dps = m_damage.GetColdDamage* m_attackSpeed;
            return dps;
        }
    }
    public float GetLightningDPS
    {
        get
        {
            float dps = m_damage.GetLightningDamage* m_attackSpeed;
            return dps;
        }
    }
}