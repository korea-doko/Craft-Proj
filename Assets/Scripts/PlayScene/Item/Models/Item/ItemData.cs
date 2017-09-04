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

    bool IsCurrupted { get; }

    void AddPrefix(ModData _data);
    void AddSuffix(ModData _data);

    void RollAllMod();
    void RemoveOneRandomPrefix();
    void RemoveOneRandomSuffix();

    void Currupt(ModData _data);
    

    string GetItemInfo();
}

[System.Serializable]
public class ItemData : IItemData
{
    [SerializeField] protected ItemBaseData m_itemBaseData;
    [SerializeField] protected ItemRarity m_rarity;
    [SerializeField] protected List<ModData> m_prefixList;
    [SerializeField] protected List<ModData> m_suffixList;
    [SerializeField] protected bool m_isCurrupted;
        
    public ItemData(ItemBaseData _baseData,ItemRarity _rarity)
    {
        m_itemBaseData = _baseData;
        m_rarity = _rarity;
        m_suffixList = new List<ModData>();
        m_prefixList = new List<ModData>();
        m_isCurrupted = false;
    }    
    public ItemRarity GetItemRarity { get { return m_rarity; } set { m_rarity = value; } }
    public List<ModData> GetPrefixes { get { return m_prefixList; } }
    public List<ModData> GetSuffixes { get { return m_suffixList; } }
    public int GetNumOfPrefix { get { return m_prefixList.Count; } }
    public int GetNumOfSuffix { get { return m_suffixList.Count; } }

    public ItemBaseData GetItemBaseData { get { return m_itemBaseData; } }

    public bool IsCurrupted
    {
        get
        {
            return m_isCurrupted;
        }

        set
        {
            m_isCurrupted = value;
        }
    }
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
    public void Currupt(ModData _data)
    {
        m_isCurrupted = true;
        m_itemBaseData.Currupt(_data);
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

        info += GetImplicitItemInfo();
    
        info += "-------------\n";

        info += GetPrefixInfo();

        info += "-------------\n";

        info += GetSuffixInfo();
        


        return info;
    }


    private int GetAllModValueInList(ModType _modType)
    {
        int value = 0;

        for(int i = 0; i < m_prefixList.Count;i++)
        {
            ModData data = m_prefixList[i];

            if (data.GetModType == _modType)
                value = data.GetSetValue;
        }

        for(int i = 0; i < m_suffixList.Count;i++)
        {
            ModData data = m_suffixList[i];

            if (data.GetModType == _modType)
                value = data.GetSetValue;
        }



        return value;
    }
    private string GetImplicitItemInfo()
    {
        string info = "";

        ModData implicitMod1 = m_itemBaseData.GetImplicitMod1;
        ModData implicitMod2 = m_itemBaseData.GetImplicitMod2;

        if (implicitMod1 != null)
            info += implicitMod1.GetName + " " + implicitMod1.GetSetValue + "\n";

        if ( implicitMod2 != null)
            info += implicitMod2.GetName + " " + implicitMod2.GetSetValue + "\n";

        return info;
    }
    private string GetWeaponItemInfo()
    {
        WeaponBaseData wbd = (WeaponBaseData)m_itemBaseData;

        string info = "";

        int physicalMin = wbd.PhysicalMinDamage;
        int physicalMax = wbd.PhysicalMaxDamage;

        int addedPhysicalMinDamage = GetAllModValueInList(ModType.AddedPhysicalMinDamage);
        int addedPhysicalMaxDamage = GetAllModValueInList(ModType.AddedPhysicalMaxDamage);
        int increasedPhysicalMinDamage = GetAllModValueInList(ModType.IncreasedPhysicalMinDamage);
        int increasedPhysicalMaxDamage = GetAllModValueInList(ModType.IncreasedPhysicalMaxDamage);

        if (increasedPhysicalMinDamage == 0) increasedPhysicalMinDamage = 1;
        if (increasedPhysicalMaxDamage == 0) increasedPhysicalMaxDamage = 1;

        physicalMin = (physicalMin + addedPhysicalMinDamage) * increasedPhysicalMinDamage;
        physicalMax = (physicalMax + addedPhysicalMaxDamage) * increasedPhysicalMaxDamage;

        info += "PhysicalDamage = " + physicalMin + " ~ " + physicalMax + "\n";

        int addedfireMinDamage = GetAllModValueInList(ModType.AddedFireMinDamage);
        int addedfireMaxDamage = GetAllModValueInList(ModType.AddedFireMaxDamage);
        int increasedfireMinDamage = GetAllModValueInList(ModType.IncreasedFireMinDamge);
        int increasedfireMaxDamage = GetAllModValueInList(ModType.IncreasedFireMaxDamage);

        if (increasedfireMinDamage == 0) increasedfireMinDamage = 1;
        if (increasedfireMaxDamage == 0) increasedfireMaxDamage = 1;

        int fireMin = addedfireMinDamage * increasedPhysicalMinDamage;
        int fireMax = addedfireMaxDamage * increasedfireMaxDamage;

        info += "<color=red>FireDamage = " + fireMin + " ~ " + fireMax + "</color>\n";

        int addedAttackSpeed = GetAllModValueInList(ModType.AddedAttackSpeed);
        int increasedAttackSpeed = GetAllModValueInList(ModType.IncreasedAttackSpeed);

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
        //int addedArmor = GetAllModValueInList(ModType.armor)

        info += "Armor = " + armor + "\n";

        int evasion = abd.EvasionRating;

        info += "Evasion = " + evasion + "\n";

        int energy = abd.EnergyShield;

        info += "EnergyShield = " + energy + "\n";

        return info;
    }
    private string GetPrefixInfo()
    {
        string info = "";

        for(int i = 0; i < m_prefixList.Count;i++)
        {
            ModData data = m_prefixList[i];

            info += data.GetModType.ToString() + " " + data.GetSetValue + "\n";
        }

        return info;
    }
    private string GetSuffixInfo()
    {
        string info = "";

        for (int i = 0; i < m_suffixList.Count; i++)
        {
            ModData data = m_suffixList[i];

            info += data.GetModType.ToString() + " " + data.GetSetValue + "\n";
        }

        return info;
    }
 }
