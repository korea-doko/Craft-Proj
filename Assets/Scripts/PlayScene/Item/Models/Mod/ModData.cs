using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModStat
{
    AccuracyRating,
    AttackSpeed
}

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

[System.Serializable]
public class ModData : IModData
{
    [SerializeField] private int m_id;
    [SerializeField] private int m_level;
    [SerializeField] private ModType m_type;
    [SerializeField] private string m_name;
    [SerializeField] private ModStat m_stat;
    [SerializeField] private int m_minValue;
    [SerializeField] private int m_maxValue;

    public ModData(int _id, int _level, ModType _type, string _name,
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
    public ModType GetModType { get { return m_type; } }
    public int GetMinValue { get { return m_minValue; } }
    public int GetMaxValue { get { return m_maxValue; } }
    public string GetName
    {
        get
        {
            return m_name;
        }
    }
    public ModStat GetStat { get { return m_stat; } }
}




