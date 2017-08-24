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
    Status GetStatus { get; }
}
[System.Serializable]
public class ItemBaseData : IItemBaseData
{
    [SerializeField] private int m_id;
    [SerializeField] protected ItemUpperClass m_upperClassName;
    [SerializeField] protected int m_itemLevel;
    [SerializeField] protected Attribute m_requiredAttribute;
    [SerializeField] protected string m_name;
    [SerializeField] protected Status m_status;



    protected ItemBaseData(int _id,string _name,int _itemLevel, Attribute _requiredAttribute)
    {
        m_id = _id;
        m_name = _name;
        m_itemLevel = _itemLevel;
        m_requiredAttribute = _requiredAttribute;
        m_status = new Status();
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
    public Status GetStatus
    {
        get
        {
            return m_status;
        }
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
        int _attackSpeed, Attribute _requiredAttribute) : 
        base(_id,_name, _itemLevel, _requiredAttribute)
    {
        m_upperClassName = ItemUpperClass.Weapon;
        m_lowerClassName = _lowerClassName;
        m_status.ChangeStatusParameterTo(StatusParameterName.AddedPhysicalMinDamage, _minDamage);
        m_status.ChangeStatusParameterTo(StatusParameterName.AddedPhysicalMaxDamage, _maxDamage);
        m_status.ChangeStatusParameterTo(StatusParameterName.AddedAttackSpeed, _attackSpeed);

        m_physicalMinDamage = _minDamage;
        m_physicalMaxDamage = _maxDamage;
        m_attackSpeed = _attackSpeed;
    }
}
public class BootsBaseData : ItemBaseData
{
    [SerializeField] private BootsLowerClass m_lowerClassName;

    public BootsBaseData(BootsLowerClass _lowerClassName, int _id, string _name,
        int _itemLevel, Attribute _requiredAttribute) :
        base(_id,_name, _itemLevel, _requiredAttribute)
    {
        m_upperClassName = ItemUpperClass.Boots;
        m_lowerClassName = _lowerClassName;
    }
}
public class HelmetBaseData : ItemBaseData
{
    [SerializeField] private HelmetLowerClass m_lowerClassName;

    public HelmetBaseData(HelmetLowerClass _lowerClassName,int _id, string _name,
        int _itemLevel, Attribute _requiredAttribute) : 
        base(_id,_name, _itemLevel, _requiredAttribute)
    {
        m_upperClassName = ItemUpperClass.Helmet;
        m_lowerClassName = _lowerClassName;
    }
}
public class RingBaseData : ItemBaseData
{
    [SerializeField] private RingLowerClass m_lowerClassName;

    public RingBaseData(RingLowerClass _lowerClassName,int _id, string _name,
        int _itemLevel, Attribute _requiredAttribute) :
        base(_id,_name, _itemLevel, _requiredAttribute)
    {
        m_upperClassName = ItemUpperClass.Ring;
        m_lowerClassName = _lowerClassName;
    }
}
public class AmuletBaseData : ItemBaseData
{
    [SerializeField] private AmuletLowerClass m_lowerClassName;

    public AmuletBaseData(AmuletLowerClass _lowerClassName,int _id, string _name,
        int _itemLevel, Attribute _requiredAttribute) :
        base(_id,_name, _itemLevel, _requiredAttribute)
    {
        m_upperClassName = ItemUpperClass.Amulet;
        m_lowerClassName = _lowerClassName;
    }
}







