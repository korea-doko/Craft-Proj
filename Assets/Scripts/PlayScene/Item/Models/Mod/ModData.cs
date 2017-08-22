using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IModData
{
    int GetID { get; }
    int GetLevel { get; }
    ModType GetModType { get; }
    ModFuncName GetModFuncName { get; }
    string GetName { get; }  
    int GetMinValue { get; }
    int GetMaxValue { get; }   
}

[System.Serializable]
public class ModData : IModData
{
    [SerializeField] protected int m_id;
    [SerializeField] protected int m_level;
    [SerializeField] protected ModType m_type;
    [SerializeField] protected string m_name;
    [SerializeField] protected int m_minValue;
    [SerializeField] protected int m_maxValue;
    [SerializeField] protected ModFuncName m_modFuncName;

    public ModData(int _id, int _level, ModType _type, string _name
        , int _minValue, int _maxValue,ModFuncName _funcName)
    {
        m_id = _id;
        m_level = _level;
        m_type = _type;
        m_name = _name;
        m_minValue = _minValue;
        m_maxValue = _maxValue;
        m_modFuncName = _funcName;
    }

    public int GetID { get { return m_id; } }
    public int GetLevel { get { return m_level; } }
    public int GetMinValue { get { return m_minValue; } }
    public int GetMaxValue { get { return m_maxValue; } }
    public string GetName { get { return m_name; } }
    public ModType GetModType { get { return m_type; } }

    public ModFuncName GetModFuncName
    {
        get
        {
            return m_modFuncName;
        }
    }
}







