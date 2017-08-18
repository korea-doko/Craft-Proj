using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IItemBaseData
{
    int GetID { get; }
    ItemUpperClassType GetItemUpperClass { get; }
    int GetItemLevel { get; }

    int GetRequiredStr { get; }
    int GetRequiredDex { get; }
    int GetRequiredInt { get; }
}
[System.Serializable]
public class ItemBaseData : IItemBaseData
{
    public ItemBaseData(int _id)
    {
        m_id = _id;
    }

    [SerializeField] protected ItemUpperClassType m_upperClassName;
    [SerializeField] protected int m_id;
    [SerializeField] protected int m_itemLevel;

    [SerializeField] protected int m_requiredStr;
    [SerializeField] protected int m_requiredDex;
    [SerializeField] protected int m_requiredInt;

    public int GetID { get { return m_id; } }
    public ItemUpperClassType GetItemUpperClass { get { return m_upperClassName; } }
    public int GetItemLevel { get { return m_itemLevel; } }
    public int GetRequiredStr { get { return m_requiredStr; } }
    public int GetRequiredDex { get { return m_requiredDex; } }
    public int GetRequiredInt { get { return m_requiredInt; } }
    
}







