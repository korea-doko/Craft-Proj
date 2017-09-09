using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IModData
{
    int GetID { get; }
    int GetGivenID { get; }
    int GetLevel { get; }
    AffixType GetAffixType { get; }
    string GetName { get; }  
    int GetMinValue { get; }
    int GetMaxValue { get; }


    int GetSetValue { get; }
    ModType GetModType { get; }
    
}

[System.Serializable]
public class ModData : IModData
{
    [SerializeField] protected int m_id;
    [SerializeField] protected int m_givenID;
    [SerializeField] protected int m_level;
    [SerializeField] protected AffixType m_affixType;
    [SerializeField] protected string m_name;
    [SerializeField] protected int m_minValue;
    [SerializeField] protected int m_maxValue;
    [SerializeField] protected ModType m_modType;
    [SerializeField] protected int m_setValue;
    [SerializeField] protected bool m_isInit;


    public ModData(int _givenID,int _id,int _level, AffixType _affixType, string _name
        , int _minValue, int _maxValue,ModType _modType)
    {
        m_id = _id;
        m_givenID = _givenID;
        m_level = _level;
        m_affixType = _affixType;
        m_name = _name;
        m_minValue = _minValue;
        m_maxValue = _maxValue;
        m_modType = _modType;
        m_setValue = 0;
        m_isInit = false;
    }
    public ModData( ModData _data)
    {
        m_level = _data.m_level;
        m_affixType = _data.m_affixType;
        m_name = _data.m_name;
        m_minValue = _data.m_minValue;
        m_maxValue = _data.m_maxValue;
        m_modType = _data.m_modType;
        m_setValue = UnityEngine.Random.Range(m_minValue, m_maxValue);

        if (m_modType != ModType.None)
            m_isInit = true;
    }

    public int GetLevel { get { return m_level; } }
    public int GetMinValue { get { return m_minValue; } }
    public int GetMaxValue { get { return m_maxValue; } }
    public string GetName { get { return m_name; } }
    public AffixType GetAffixType { get { return m_affixType; } }
    public ModType GetModType
    {
        get
        {
            return m_modType;
        }
    }
    public int GetSetValue
    {
        get
        {
            return m_setValue;
        }
    }
    public int GetGivenID
    {
        get
        {
            return m_givenID;
        }
    }
    public int GetID
    {
        get
        {
            return m_id;
        }
    }
    
    public void ChangeSetValue()
    {
        m_setValue = UnityEngine.Random.Range(m_minValue, m_maxValue);
    }
}







