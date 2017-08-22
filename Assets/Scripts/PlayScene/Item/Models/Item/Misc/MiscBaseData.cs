using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public interface IMiscBaseItem
{
    ItemLowerClassMisc GetItemLowerClassMisc { get; }
}

[System.Serializable]
public class MiscBaseData : ItemBaseData, IMiscBaseItem
{
    [SerializeField] private ItemLowerClassMisc m_lowerClassName;

    public MiscBaseData(int _id,int _itemLevel,Status _requiredStatus ,
        ItemLowerClassMisc _type) : base(_id,_itemLevel,_requiredStatus)
    {
        m_upperClassName = ItemUpperClassType.Misc;
        m_lowerClassName = _type;
    }
    public ItemLowerClassMisc GetItemLowerClassMisc
    {
        get
        {
            return m_lowerClassName;
        }
    }
}