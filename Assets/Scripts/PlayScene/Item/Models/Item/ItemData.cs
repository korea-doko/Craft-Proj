using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IItemData
{
    ItemRarity GetItemRarity { get; }
    ItemBaseData GetItemBaseData { get; }
    List<ModData> GetPrefixes { get; }
    List<ModData> GetSuffixes { get; }
    void AddPrefix(ModData _data);
    void AddSuffix(ModData _data);
    int GetNumOfPrefix { get; }
    int GetNumOfSuffix { get; }
}

[System.Serializable]
public class ItemData : IItemData {

    [SerializeField] protected ItemRarity m_rarity;
    [SerializeField] protected ItemBaseData m_itemBaseData;
    [SerializeField] protected List<ModData> m_suffixList;
    [SerializeField] protected List<ModData> m_prefixList;


    protected ItemData(ItemRarity _rarity)
    {
        m_rarity = _rarity;
        m_suffixList = new List<ModData>();
        m_prefixList = new List<ModData>();
    }

    public ItemRarity GetItemRarity
    {
        get { return m_rarity; }
    }
    public ItemBaseData GetItemBaseData
    {
        get
        {
            return m_itemBaseData;
        }
    }

    public List<ModData> GetPrefixes
    {
        get
        {
            return m_prefixList;
        }
    }
    public List<ModData> GetSuffixes { get { return m_suffixList; } }

    public int GetNumOfPrefix { get { return m_prefixList.Count; } }
    public int GetNumOfSuffix { get { return m_suffixList.Count; } }

    public void AddPrefix(ModData _data)
    {
        m_prefixList.Add(_data);
    }
    public void AddSuffix(ModData _data)
    {
        m_suffixList.Add(_data);
    }

    public string GetItemInfo()
    {
        string str = "";
        WeaponData wData = (WeaponData)this;
        WeaponBaseData bData = (WeaponBaseData)m_itemBaseData;

        str += bData.GetItemLowerClassWeapon;

        return str;
    }

}
