using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IItemBaseData
{
    int GetID { get; }
    ItemUpperClassType GetItemUpperClass { get; }
    int GetItemLevel { get; }
    
    Status GetRequiredStatus { get; }
}
[System.Serializable]
public class ItemBaseData : IItemBaseData
{
    protected ItemBaseData(int _id,int _itemLevel,Status _requiredStatus)
    {
        m_id = _id;
        m_itemLevel = _itemLevel;
        m_requiredStatus = _requiredStatus;
    }

    [SerializeField] protected int m_id;
    [SerializeField] protected ItemUpperClassType m_upperClassName;    
    [SerializeField] protected int m_itemLevel;
    [SerializeField] protected Status m_requiredStatus;

    public int GetID { get { return m_id; } }
    public ItemUpperClassType GetItemUpperClass { get { return m_upperClassName; } }
    public int GetItemLevel { get { return m_itemLevel; } }
    public Status GetRequiredStatus
    {
        get
        {
            return m_requiredStatus;
        }
    }
}







