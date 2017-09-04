using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IItemBaseData
{
    int GetID { get; }
    ItemUpperClass GetItemUpperClass { get; }

    int GetItemLevel { get; }    
    Attribute GetBaseItemRequiredAttribute { get; }
    string GetItemName { get; }
    ModData GetImplicitMod1 { get; set; }
    ModData GetImplicitMod2 { get; set; }

    void Currupt(ModData _data);

}
[System.Serializable]
public class ItemBaseData : IItemBaseData
{
    [SerializeField] private int m_id;
    [SerializeField] protected ItemUpperClass m_upperClassName;
    [SerializeField] protected int m_itemLevel;
    [SerializeField] protected Attribute m_requiredAttribute;
    [SerializeField] protected string m_name;
    [SerializeField] protected ModData m_implicitMod1;
    [SerializeField] protected ModData m_implicitMod2;

    protected ItemBaseData(int _id, string _name, int _itemLevel, Attribute _requiredAttribute,
        ModData _implicitMod1, ModData _implicitMod2)
    {
        m_id = _id;
        m_name = _name;
        m_itemLevel = _itemLevel;
        m_requiredAttribute = _requiredAttribute;

        if (_implicitMod1 != null)
            m_implicitMod1 = new ModData(_implicitMod1);

        if (_implicitMod2 != null)
            m_implicitMod2 = new ModData(_implicitMod2);
    }

    public int GetID { get { return m_id; } }
    public ItemUpperClass GetItemUpperClass { get { return m_upperClassName; } }
    public int GetItemLevel { get { return m_itemLevel; } }
    public Attribute GetBaseItemRequiredAttribute
    {
        get
        {
            return m_requiredAttribute;
        }
    }
    public string GetItemName { get { return m_name; } }
    public ModData GetImplicitMod1
    {
        get
        {
            return m_implicitMod1;
        }

        set
        {
            m_implicitMod1 = new ModData(value);
        }
    }
    public ModData GetImplicitMod2
    {
        get { return m_implicitMod2; }
        set { m_implicitMod2 = new ModData(value); }
    }
    public void Currupt(ModData _data)
    {
        m_implicitMod1 = new ModData(_data);
        m_implicitMod2 = null;
    }
}
[System.Serializable]
public class WeaponBaseData : ItemBaseData
{
    [SerializeField] private WeaponLowerClass m_lowerClassName;
    [SerializeField] int m_physicalMinDamage;
    [SerializeField] int m_physicalMaxDamage;
    [SerializeField] int m_attackSpeed;
    

    public WeaponBaseData(WeaponLowerClass _lowerClassName, int _id,
        string _name, int _itemLevel,  int _minDamage,int _maxDamage,
        int _attackSpeed, Attribute _requiredAttribute, ModData _implicitMod1 = null,
        ModData _implicitMod2 = null) : 
        base(_id,_name, _itemLevel, _requiredAttribute, _implicitMod1, _implicitMod2)
    {
        m_upperClassName = ItemUpperClass.Weapon;
        m_lowerClassName = _lowerClassName;
       
        PhysicalMinDamage = _minDamage;
        PhysicalMaxDamage = _maxDamage;
        AttackSpeed = _attackSpeed;
    }

    public WeaponLowerClass LowerClassName
    {
        get
        {
            return m_lowerClassName;
        }
    }

    public int PhysicalMinDamage
    {
        get
        {
            return m_physicalMinDamage;
        }

        set
        {
            m_physicalMinDamage = value;
        }
    }
    public int PhysicalMaxDamage
    {
        get
        {
            return m_physicalMaxDamage;
        }

        set
        {
            m_physicalMaxDamage = value;
        }
    }
    public int AttackSpeed
    {
        get
        {
            return m_attackSpeed;
        }

        set
        {
            m_attackSpeed = value;
        }
    }
}

[System.Serializable]
public class ArmorBaseData : ItemBaseData
{
    [SerializeField] private ArmorLowerClass m_lowerClassName;
    [SerializeField] private int m_armor;
    [SerializeField] private int m_evasionRating;
    [SerializeField] private int m_energyShield;

    public ArmorBaseData(ArmorLowerClass _lowerClassName, int _id, string _name,
        int _itemLevel, Attribute _requiredAttribute,int _armor,
        int _evasionRating, int _energyShield, ModData _implicitMod1 = null,
        ModData _implicitMod2 = null) :
        base(_id,_name, _itemLevel, _requiredAttribute, _implicitMod1, _implicitMod2)
    {
        m_upperClassName = ItemUpperClass.Armor;
        m_lowerClassName = _lowerClassName;

        m_armor = _armor;
        m_evasionRating = _evasionRating;
        m_energyShield = _energyShield;
    }

    public int Armor
    {
        get
        {
            return m_armor;
        }

        set
        {
            m_armor = value;
        }
    }

    public int EvasionRating
    {
        get
        {
            return m_evasionRating;
        }

        set
        {
            m_evasionRating = value;
        }
    }

    public int EnergyShield
    {
        get
        {
            return m_energyShield;
        }

        set
        {
            m_energyShield = value;
        }
    }

    public ArmorLowerClass LowerClassName
    {
        get
        {
            return m_lowerClassName;
        }
        
    }
}

[System.Serializable]
public class MiscBaseData : ItemBaseData
{
    [SerializeField] private MiscLowerClass m_lowerClassName;

    public MiscBaseData(MiscLowerClass _lowerClassName,int _id, string _name, int _itemLevel,
        ModData _implicitMod1,ModData _implicitMod2) 
        : base(_id, _name, _itemLevel, new Attribute(),_implicitMod1,_implicitMod2)
    {
        m_upperClassName = ItemUpperClass.Misc;
        m_lowerClassName = _lowerClassName;
    }

    public MiscLowerClass LowerClassName
    {
        get
        {
            return m_lowerClassName;
        }
    }
}








