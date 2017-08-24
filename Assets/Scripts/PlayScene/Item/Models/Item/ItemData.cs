using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IItemData
{
    ItemRarity GetItemRarity { get; set; }
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
    [SerializeField] protected List<ModData> m_prefixList;
    [SerializeField] protected List<ModData> m_suffixList;
    [SerializeField] protected Status m_totalStatus;


    public ItemData(ItemBaseData _data, ItemRarity _rarity)
    {
        m_itemBaseData = _data;
        m_rarity = _rarity;
        m_suffixList = new List<ModData>();
        m_prefixList = new List<ModData>();

        m_totalStatus = new Status();
        m_totalStatus.AddStatus(_data.GetStatus);
    }
    
    public ItemRarity GetItemRarity
    {
        get { return m_rarity; }
        set { m_rarity = value; }
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
        m_totalStatus.AddStatusParameter(_data.GetAffectedParameterName, _data.GetSetValue);
        m_prefixList.Add(_data);
    }
    public void AddSuffix(ModData _data)
    {
        m_totalStatus.AddStatusParameter(_data.GetAffectedParameterName, _data.GetSetValue);
        m_suffixList.Add(_data);
    }

    public void RemoveOneRandomPrefix()
    {
        int rand = UnityEngine.Random.Range(0, m_prefixList.Count);
        ModData mod = m_prefixList[rand];
        m_totalStatus.MinusStatusParameter(mod.GetAffectedParameterName, mod.GetSetValue);
        m_prefixList.RemoveAt(rand);
    }
    public void RemoveOneRandomSuffix()
    {
        int rand = UnityEngine.Random.Range(0, m_suffixList.Count);
        ModData mod = m_suffixList[rand];
        m_totalStatus.MinusStatusParameter(mod.GetAffectedParameterName, mod.GetSetValue);
        m_suffixList.RemoveAt(rand);
    }
  
    public void RollAllMod()
    {
        for(int i = 0; i< GetNumOfPrefix;i++)
        {
            ModData prefix = m_prefixList[i];

            m_totalStatus.MinusStatusParameter(prefix.GetAffectedParameterName, prefix.GetSetValue);

            prefix.ChangeSetValue();

            m_totalStatus.AddStatusParameter(prefix.GetAffectedParameterName, prefix.GetSetValue);
        }

        for(int i= 0;i<GetNumOfSuffix;i++)
        {
            ModData suffix = m_suffixList[i];

            m_totalStatus.MinusStatusParameter(suffix.GetAffectedParameterName, suffix.GetSetValue);

            suffix.ChangeSetValue();

            m_totalStatus.AddStatusParameter(suffix.GetAffectedParameterName, suffix.GetSetValue);
        }
    }
   
    public string GetItemInfo()
    {
        string str ="등급 = "+ m_rarity.ToString() +"\n" + m_totalStatus.GetStatusInfo();

        switch (m_itemBaseData.GetItemUpperClass)
        {
            case ItemUpperClass.Armor:
                break;
            case ItemUpperClass.Weapon:
                //WeaponBaseData wbd = (WeaponBaseData)m_itemBaseData;
                
                break;
            case ItemUpperClass.Boots:
                break;
            case ItemUpperClass.Helmet:
                break;
            case ItemUpperClass.Ring:
                break;
            case ItemUpperClass.Amulet:
                break;
            default:
                break;
        }

        //WeaponData wData = (WeaponData)this;
        //WeaponBaseData bData = (WeaponBaseData)m_itemBaseData;

        //str += bData.GetItemLowerClassWeapon;

        return str;
    }    
}
public class WeaponData : ItemData
{

}
