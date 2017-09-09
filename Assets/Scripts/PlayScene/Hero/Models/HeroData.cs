using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeroData
{
    EHeroClass GetHeroClass { get; }
    string GetName { get; }
    ItemData[] GetEquipDataAry { get; set; }
    Attribute GetBaseAttribute { get; }
    Attribute GetOffsetAttribute { get; }
    int GetLevel { get; }

    string GetHeroInfos();

    void EquipItemWith(ItemData _data);
}
[System.Serializable]
public class HeroData : IHeroData
{

    [SerializeField] private string m_name;
    [SerializeField] EHeroClass m_heroClass;
    [SerializeField] private Attribute m_baseAttr;
    [SerializeField] private Attribute m_offsetAttr;
    [SerializeField] private int m_level;
    [SerializeField] private int[] m_modTypeAry;   
    [SerializeField] private ItemData[] m_equipDataAry;
    [SerializeField] private int[] m_modValueAry;

    [SerializeField] private PersonalityData m_personality;
    [SerializeField] private SpecialityData m_speciality;
    [SerializeField] private List<TraitData> m_traitList;

    public HeroData(string _name,EHeroClass _class, Attribute _baseAttr,Attribute _offsetAttr
        ,PersonalityData _personalityData,SpecialityData _specialityData, TraitData _traitData)
    {
        m_name = _name;
        m_heroClass = _class;
        m_baseAttr = _baseAttr;
        m_offsetAttr = _offsetAttr;

        int numOfModType = System.Enum.GetNames(typeof(ModType)).Length;
        m_modTypeAry = new int[numOfModType];

        int numOfEquipParts = System.Enum.GetNames(typeof(EEquipParts)).Length;
        m_equipDataAry = new ItemData[numOfEquipParts];

        int numOfModtype = System.Enum.GetNames(typeof(ModType)).Length;
        m_modValueAry = new int[numOfModType];

        m_personality = new PersonalityData(_personalityData);
        AddModValue(m_personality.ModType, m_personality.ModValue);

        m_speciality = new SpecialityData(_specialityData);
        AddModValue(m_speciality.ModType, m_speciality.ModValue);

        m_traitList = new List<TraitData>();

        TraitData traitData = new TraitData(_traitData);
        AddTraitData(traitData);

    }
    
    public EHeroClass GetHeroClass
    {
        get
        {
            return m_heroClass;
        }
    }
    public string GetName
    {
        get
        {
            return m_name;
        }
    }

    public Attribute GetBaseAttribute
    {
        get
        {
            return m_baseAttr;
        }
    }
    public Attribute GetOffsetAttribute
    {
        get
        {
            return m_offsetAttr;
        }
    }
    public int GetLevel
    {
        get
        {
            return m_level;
        }
    }
    public ItemData[] GetEquipDataAry
    {
        get
        {
            return m_equipDataAry;
        }

        set
        {
            m_equipDataAry = value;
        }
    }

    public void EquipItemWith(ItemData _data)
    {
        switch (_data.GetItemBaseData.GetItemUpperClass)
        {
            case ItemUpperClass.Armor:

                ArmorBaseData abd = (ArmorBaseData)_data.GetItemBaseData;
                
                switch (abd.LowerClassName)
                {
                    case ArmorLowerClass.Helmet:
                        GetEquipDataAry[(int)EEquipParts.Head] = _data;
                        break;
                    case ArmorLowerClass.BodyArmor:
                        GetEquipDataAry[(int)EEquipParts.Body] = _data;
                        break;
                    case ArmorLowerClass.Boots:
                        GetEquipDataAry[(int)EEquipParts.Foot] = _data;
                        break;
                    case ArmorLowerClass.Gloves:
                        GetEquipDataAry[(int)EEquipParts.GloveHand] = _data;
                        break;
                    default:
                        break;
                }
                break;
            case ItemUpperClass.Weapon:
                GetEquipDataAry[(int)EEquipParts.WeaponHand] = _data;
                break;
            case ItemUpperClass.Misc:

                MiscBaseData mbd = (MiscBaseData)_data.GetItemBaseData;

                switch (mbd.LowerClassName)
                {
                    case MiscLowerClass.Ring:

                        GetEquipDataAry[(int)EEquipParts.Finger] = _data;
                        break;
                    case MiscLowerClass.Amulet:
                        GetEquipDataAry[(int)EEquipParts.Neck] = _data;
                        break;
                    default:
                        break;
                }

                break;
            default:
                break;
        }

        AddModTypeValueInItemData(_data);
    }
    public void RemoveItemParts(EEquipParts _parts)
    {
        RemoveModTypeValueInItemData(m_equipDataAry[(int)_parts]);
        m_equipDataAry[(int)_parts] = null;
    }
    public void AddModTypeValueInItemData(ItemData _item)
    {
        ModData implicitMod1 = _item.GetImplicitMod1;
        ModData implicitMod2 = _item.GetImplicitMod2;
        List<ModData> prefixMods = _item.GetPrefixes;
        List<ModData> suffixMods = _item.GetSuffixes;

        AddModValue(implicitMod1);
        AddModValue(implicitMod2);

        foreach (ModData data in prefixMods)
            AddModValue(data);

        foreach (ModData data in suffixMods)
            AddModValue(data);

    }
    public void RemoveModTypeValueInItemData(ItemData _item)
    {
        ModData implicitMod1 = _item.GetImplicitMod1;
        ModData implicitMod2 = _item.GetImplicitMod2;
        List<ModData> prefixMods = _item.GetPrefixes;
        List<ModData> suffixMods = _item.GetSuffixes;

        RemoveModValue(implicitMod1);
        RemoveModValue(implicitMod2);

        foreach (ModData data in prefixMods)
            RemoveModValue(data);

        foreach (ModData data in suffixMods)
            RemoveModValue(data);
    }

    public void AddTraitData(TraitData _data)
    {
        if (m_traitList.Count == 3)
            RemoveRandomTraitDataInList();

        AddModValue(_data.ModType, _data.ModValue);
        m_traitList.Add(_data);
    }
    public void RemoveRandomTraitDataInList()
    {
        int count = m_traitList.Count;
        int rand = UnityEngine.Random.Range(0, count);

        TraitData traitData = m_traitList[rand];
        RemoveModValue(traitData.ModType, traitData.ModValue);
        m_traitList.RemoveAt(rand);
    }

    public string GetHeroInfos()
    {
        string str = "";

        int count = m_modValueAry.Length;

        for (int i = 0; i < count; i++)
        {
            int value = m_modValueAry[i];

            if (value != 0)
                str += ((ModType)i).ToString() + " = " + value.ToString() + "\n";

        }



        return str;
    }

    private void AddModValue(ModData _data)
    {
        if (_data != null)
            AddModValue(_data.GetModType, _data.GetSetValue);                
    }
    private void AddModValue(ModType _type, int _value)
    {
        if (_type != ModType.None)
            m_modValueAry[(int)_type] += _value;
    }
    private void RemoveModValue(ModData _data)
    {
        if (_data != null)
            RemoveModValue(_data.GetModType, _data.GetSetValue);
    }
    private void RemoveModValue(ModType _type, int _value)
    {
        if (_type != ModType.None)
            m_modValueAry[(int)_type] -= _value;
    }


}
