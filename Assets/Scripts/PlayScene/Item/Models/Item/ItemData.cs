using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IItemData
{
    ItemBaseData GetItemBaseData { get; }
    ItemRarity GetItemRarity { get; set; }

    List<ModData> GetPrefixes { get; }
    List<ModData> GetSuffixes { get; }

    int GetNumOfPrefix { get; }
    int GetNumOfSuffix { get; }

    void AddPrefix(ModData _data);
    void AddSuffix(ModData _data);

    void RollAllMod();
    void RemoveOneRandomPrefix();
    void RemoveOneRandomSuffix();

    string GetItemInfo();
}

[System.Serializable]
public class ItemData : IItemData
{
    [SerializeField] protected ItemBaseData m_itemBaseData;
    [SerializeField] protected ItemRarity m_rarity;
    [SerializeField] protected List<ModData> m_prefixList;
    [SerializeField] protected List<ModData> m_suffixList;
        
    public ItemData(ItemBaseData _baseData,ItemRarity _rarity)
    {
        m_itemBaseData = _baseData;
        m_rarity = _rarity;
        m_suffixList = new List<ModData>();
        m_prefixList = new List<ModData>();       
    }    
    public ItemRarity GetItemRarity { get { return m_rarity; } set { m_rarity = value; } }
    public List<ModData> GetPrefixes { get { return m_prefixList; } }
    public List<ModData> GetSuffixes { get { return m_suffixList; } }
    public int GetNumOfPrefix { get { return m_prefixList.Count; } }
    public int GetNumOfSuffix { get { return m_suffixList.Count; } }

    public ItemBaseData GetItemBaseData { get { return m_itemBaseData; } }
    
    public void AddPrefix(ModData _data)
    {
        m_prefixList.Add(_data);
    }
    public void AddSuffix(ModData _data)
    {
        m_suffixList.Add(_data);
    }

    

    public void RemoveOneRandomPrefix()
    {
        int rand = UnityEngine.Random.Range(0, m_prefixList.Count);
        m_prefixList.RemoveAt(rand);
    }
    public void RemoveOneRandomSuffix()
    {
        int rand = UnityEngine.Random.Range(0, m_suffixList.Count);
        m_suffixList.RemoveAt(rand);
    }
    public void RollAllMod()
    {
        for(int i = 0; i < m_prefixList.Count;i++)
        {
            ModData prefix = m_prefixList[i];
            prefix.ChangeSetValue();
        }
        for(int i = 0; i < m_suffixList.Count;i++)
        {
            ModData suffix = m_suffixList[i];
            suffix.ChangeSetValue();
        }
    }

    public string GetItemInfo()
    {
        string info = m_itemBaseData.GetItemName + "\n" +
            m_rarity.ToString() + "\n";


        switch (m_itemBaseData.GetItemUpperClass)
        {
            case ItemUpperClass.Armor:
                info += GetArmorItemInfo();
                break;
            case ItemUpperClass.Weapon:
                info += GetWeaponItemInfo();
                break;
            case ItemUpperClass.Misc:
                break;
            default:
                break;
        }
      
        info += "-------------\n";

        info += GetPrefixInfo();

        info += "-------------\n";

        info += GetSuffixInfo();
        


        return info;
    }


    private int GetAllParameterValueInList(StatusParameterName _name)
    {
        int value = 0;

        for(int i = 0; i < m_prefixList.Count;i++)
        {
            ModData data = m_prefixList[i];

            if (data.GetAffectedParameterName == _name)
                value = data.GetSetValue;
        }

        for(int i = 0; i < m_suffixList.Count;i++)
        {
            ModData data = m_suffixList[i];

            if (data.GetAffectedParameterName == _name)
                value = data.GetSetValue;
        }



        return value;
    }

    private string GetWeaponItemInfo()
    {
        WeaponBaseData wbd = (WeaponBaseData)m_itemBaseData;

        string info = "";

        int physicalMin = wbd.PhysicalMinDamage;
        int physicalMax = wbd.PhysicalMaxDamage;

        int addedPhysicalMinDamage = GetAllParameterValueInList(StatusParameterName.AddedPhysicalMinDamage);
        int addedPhysicalMaxDamage = GetAllParameterValueInList(StatusParameterName.AddedPhysicalMaxDamage);
        int increasedPhysicalMinDamage = GetAllParameterValueInList(StatusParameterName.IncreasedPhysicalMinDamage);
        int increasedPhysicalMaxDamage = GetAllParameterValueInList(StatusParameterName.IncreasedPhysicalMaxDamage);

        if (increasedPhysicalMinDamage == 0) increasedPhysicalMinDamage = 1;
        if (increasedPhysicalMaxDamage == 0) increasedPhysicalMaxDamage = 1;

        physicalMin = (physicalMin + addedPhysicalMinDamage) * increasedPhysicalMinDamage;
        physicalMax = (physicalMax + addedPhysicalMaxDamage) * increasedPhysicalMaxDamage;

        info += "PhysicalDamage = " + physicalMin + " ~ " + physicalMax + "\n";

        int addedfireMinDamage = GetAllParameterValueInList(StatusParameterName.AddedFireMinDamage);
        int addedfireMaxDamage = GetAllParameterValueInList(StatusParameterName.AddedFireMaxDamage);
        int increasedfireMinDamage = GetAllParameterValueInList(StatusParameterName.IncreasedFireMinDamge);
        int increasedfireMaxDamage = GetAllParameterValueInList(StatusParameterName.IncreasedFireMaxDamage);

        if (increasedfireMinDamage == 0) increasedfireMinDamage = 1;
        if (increasedfireMaxDamage == 0) increasedfireMaxDamage = 1;

        int fireMin = addedfireMinDamage * increasedPhysicalMinDamage;
        int fireMax = addedfireMaxDamage * increasedfireMaxDamage;

        info += "<color=red>FireDamage = " + fireMin + " ~ " + fireMax + "</color>\n";

        int addedAttackSpeed = GetAllParameterValueInList(StatusParameterName.AddedAttackSpeed);
        int increasedAttackSpeed = GetAllParameterValueInList(StatusParameterName.IncreasedAttackSpeed);

        if (increasedAttackSpeed == 0) increasedAttackSpeed = 1;

        int attackSpeed = wbd.AttackSpeed;
        attackSpeed = (attackSpeed + addedAttackSpeed) * increasedAttackSpeed;

        info += "Attack Speed = " + attackSpeed + "\n";


        return info;
    }

    private string GetArmorItemInfo()
    {
        ArmorBaseData abd = (ArmorBaseData)m_itemBaseData;
        string info = "";

        int armor = abd.Armor;
        //int addedArmor = GetAllParameterValueInList(StatusParameterName.armor)

        info += "Armor = " + armor + "\n";

        int evasion = abd.EvasionRating;

        info += "Evasion = " + evasion + "\n";

        int energy = abd.EnergyShield;

        info += "EnergyShield = " + energy + "\n";

        return info;
    }

    string GetPrefixInfo()
    {
        string info = "";

        for(int i = 0; i < m_prefixList.Count;i++)
        {
            ModData data = m_prefixList[i];

            info += data.GetAffectedParameterName.ToString() + " " + data.GetSetValue + "\n";
        }

        return info;
    }

    string GetSuffixInfo()
    {
        string info = "";

        for (int i = 0; i < m_suffixList.Count; i++)
        {
            ModData data = m_suffixList[i];

            info += data.GetAffectedParameterName.ToString() + " " + data.GetSetValue + "\n";
        }

        return info;
    }
}
