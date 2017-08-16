using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IArmorBaseItem
{
    ItemLowerClassArmors GetItemLowerClassArmor { get; }
}

[System.Serializable]
public class ArmorBaseData : ItemBaseData, IArmorBaseItem
{
    [SerializeField] private ItemLowerClassArmors m_lowerClassName;


    public ArmorBaseData(int _id, ItemLowerClassArmors _type) : base(_id)
    {
        m_upperClassName = ItemUpperClassType.Armors;
        m_lowerClassName = _type;
    }

    public ItemLowerClassArmors GetItemLowerClassArmor
    {
        get
        {
            return m_lowerClassName;
        }
    }
}