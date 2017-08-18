using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IItemData
{
    ItemRarity GetItemRarity { get; }
    
}

[System.Serializable]
public class ItemData : IItemData {

    [SerializeField] protected ItemRarity m_rarity;

    protected ItemData(ItemRarity _rarity)
    {
        m_rarity = _rarity;
    }
    public ItemRarity GetItemRarity
    {
        get { return m_rarity; }
    }

 }
