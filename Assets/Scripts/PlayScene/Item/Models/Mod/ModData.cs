using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IModData
{
    int GetID { get; }
    int GetLevel { get; }
    ModType GetModType { get; }
    string GetName { get; }
    ModStat GetStat { get; }
    int GetMinValue { get; }
    int GetMaxValue { get; }   
}
public interface IWeaponMod : IModData
{
    void Attach(WeaponData _data);
    void Detach(WeaponData _data);
}


[System.Serializable]
public class ModData : IModData
{
    [SerializeField] protected int m_id;
    [SerializeField] protected int m_level;
    [SerializeField] protected ModType m_type;
    [SerializeField] protected string m_name;
    [SerializeField] protected ModStat m_stat;
    [SerializeField] protected int m_minValue;
    [SerializeField] protected int m_maxValue;

    protected ModData(int _id, int _level, ModType _type, string _name,
        ModStat _stat, int _minValue, int _maxValue)
    {
        m_id = _id;
        m_level = _level;
        m_type = _type;
        m_name = _name;
        m_stat = _stat;
        m_minValue = _minValue;
        m_maxValue = _maxValue;
    }

    public int GetID { get { return m_id; } }
    public int GetLevel { get { return m_level; } }
    public int GetMinValue { get { return m_minValue; } }
    public int GetMaxValue { get { return m_maxValue; } }
    public string GetName { get { return m_name; } }
    public ModStat GetStat { get { return m_stat; } }
    public ModType GetModType { get { return m_type; } }
    
}


public enum ModStat
{
    AttackSpeed,
    AttackDamage
}
public enum WeaponModName
{
    Flame,
    Frosted,
    Lightening,
    Chaotic,
    Shapen
}

[System.Serializable]
public class WeaponModData : ModData ,IWeaponMod
{
    public WeaponModName m_weaponNameMod;

    public WeaponModData(int _id, int _level, ModType _type, string _name,
        ModStat _stat, int _minValue, int _maxValue,WeaponModName _weaponModName) :
        base(_id, _level, _type, _name, _stat, _minValue, _maxValue)
    {
        m_weaponNameMod = _weaponModName;
    }

    public void Attach(WeaponData _data)
    {
        
    }

    public void Detach(WeaponData _data)
    {

    }
}




