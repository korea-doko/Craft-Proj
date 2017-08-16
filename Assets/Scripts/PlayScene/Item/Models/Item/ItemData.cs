using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IItemData
{
    ItemRarity GetItemRarity { get; }
    ModData[] GetPrefixes { get; }
    ModData[] GetSuffixes { get; }
    int GetNumOfPrefix { get; }
    int GetNumOfSuffix { get; }

    void AddPrefix(ModData _data);
    void AddSuffix(ModData _data);

    void RemoveRandomPrefix();
    void RemoveRandomSuffix();
}

[System.Serializable]
public class ItemData : IItemData {

    [SerializeField] protected ItemRarity m_rarity;

    [SerializeField] protected ModData[] m_prefixAry;
    [SerializeField] protected ModData[] m_suffixAry;

    [SerializeField] protected int m_numOfPrefix;
    [SerializeField] protected int m_numOfSuffix;

    public ItemData(ItemRarity _rarity)
    {
        m_prefixAry = new ModData[3];
        m_suffixAry = new ModData[3];

        m_numOfPrefix = 0;
        m_numOfSuffix = 0;
    }

    public ItemRarity GetItemRarity
    {
        get { return m_rarity; }
    }
    public ModData[] GetPrefixes { get { return m_prefixAry; } }
    public ModData[] GetSuffixes { get { return m_suffixAry; } }
    public int GetNumOfPrefix { get { return m_numOfPrefix; } }
    public int GetNumOfSuffix { get { return m_numOfSuffix; } }

    public void AddPrefix(ModData _data)
    {
        m_prefixAry[m_numOfPrefix] = _data;
        m_numOfPrefix++;
    }
    public void AddSuffix(ModData _data)
    {
        m_suffixAry[m_numOfSuffix] = _data;
        m_numOfSuffix++;
    }

    public void RemoveRandomPrefix()
    {
        Debug.Log("NOT YET");
    }
    public void RemoveRandomSuffix()
    {
        Debug.Log("NOT YET");
    }
}
