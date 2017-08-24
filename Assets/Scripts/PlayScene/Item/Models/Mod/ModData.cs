using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IModData
{
    int GetLevel { get; }
    AffixType GetModType { get; }
    string GetName { get; }  
    int GetMinValue { get; }
    int GetMaxValue { get; }


    int GetSetValue { get; }
    StatusParameterName GetAffectedParameterName { get; }
}

[System.Serializable]
public class ModData : IModData
{
    [SerializeField] protected int m_level;
    [SerializeField] protected AffixType m_type;
    [SerializeField] protected string m_name;
    [SerializeField] protected int m_minValue;
    [SerializeField] protected int m_maxValue;
    [SerializeField] protected StatusParameterName m_affectedParameterName;
    [SerializeField] protected int m_setValue;


    public ModData(int _level, AffixType _type, string _name
        , int _minValue, int _maxValue,StatusParameterName _affectedParameterName)
    {
        m_level = _level;
        m_type = _type;
        m_name = _name;
        m_minValue = _minValue;
        m_maxValue = _maxValue;
        m_affectedParameterName = _affectedParameterName;
        m_setValue = 0;
    }
    public ModData( ModData _data)
    {
        m_level = _data.m_level;
        m_type = _data.m_type;
        m_name = _data.m_name;
        m_minValue = _data.m_minValue;
        m_maxValue = _data.m_maxValue;
        m_affectedParameterName = _data.m_affectedParameterName;
        m_setValue = UnityEngine.Random.Range(m_minValue, m_maxValue);
    }

    public int GetLevel { get { return m_level; } }
    public int GetMinValue { get { return m_minValue; } }
    public int GetMaxValue { get { return m_maxValue; } }
    public string GetName { get { return m_name; } }
    public AffixType GetModType { get { return m_type; } }
    public StatusParameterName GetAffectedParameterName
    {
        get
        {
            return m_affectedParameterName;
        }
    }
    public int GetSetValue
    {
        get
        {
            return m_setValue;
        }
    }
    public void ChangeSetValue()
    {
        m_setValue = UnityEngine.Random.Range(m_minValue, m_maxValue);
    }
}







