using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IWeaponBaseItem : IItemBaseData
{
    ItemLowerClassWeapons GetItemLowerClassWeapon { get; }
    Damage GetDamage { get; }
    float GetAttackSpeed { get; }   
    float GetDPS { get; }
}

[System.Serializable]
public class WeaponBaseData : ItemBaseData, IWeaponBaseItem
{
    [SerializeField] private ItemLowerClassWeapons m_lowerClassName;
    [SerializeField] private Damage m_damage;
    [SerializeField] private float m_attackSpeed;

    public WeaponBaseData(int _id,int _itemLevel, Status _requiredStatus ,ItemLowerClassWeapons _type, float _minDamage, float _maxDamage,
        float _attackSpeed) : base(_id,_itemLevel,_requiredStatus)
    {
        m_upperClassName = ItemUpperClassType.Weapons;
        m_lowerClassName = _type;
        m_damage = new Damage(); //new Damage(new Vector2(_minDamage, _maxDamage), Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);
        m_attackSpeed = _attackSpeed;          
    }

    public ItemLowerClassWeapons GetItemLowerClassWeapon
    {
        get
        {
            return m_lowerClassName;
        }
    }
    public Damage GetDamage
    {
        get
        {
            return m_damage;
        }
    }
    public float GetAttackSpeed
    {
        get { return m_attackSpeed; }
    }
    public float GetDPS
    {
        get
        {
            return m_attackSpeed * m_damage.GetAverageDamage;
        }
    }
}