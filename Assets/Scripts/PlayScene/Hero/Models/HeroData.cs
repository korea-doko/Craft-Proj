using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeroData
{
    EHeroClass GetHeroClass { get; }
    string GetName { get; }
    ItemData[] EquipDataAry { get; set; }
    Attribute GetBaseAttribute { get; }
    Attribute GetOffsetAttribute { get; }

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

    public HeroData(string _name,EHeroClass _class, Attribute _baseAttr,Attribute _offsetAttr)
    {
        m_name = _name;
        m_heroClass = _class;
        m_baseAttr = _baseAttr;
        m_offsetAttr = _offsetAttr;

        int numOfModType = System.Enum.GetNames(typeof(ModType)).Length;
        m_modTypeAry = new int[numOfModType];

        int numOfEquipParts = System.Enum.GetNames(typeof(EEquipParts)).Length;
        m_equipDataAry = new ItemData[numOfEquipParts];
    }
    
    public ItemData[] EquipDataAry
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

    public void EquipItemWith(ItemData _data)
    {
        switch (_data.GetItemBaseData.GetItemUpperClass)
        {
            case ItemUpperClass.Armor:

                ArmorBaseData abd = (ArmorBaseData)_data.GetItemBaseData;
                
                switch (abd.LowerClassName)
                {
                    case ArmorLowerClass.Helmet:
                        EquipDataAry[(int)EEquipParts.Head] = _data;
                        break;
                    case ArmorLowerClass.BodyArmor:
                        EquipDataAry[(int)EEquipParts.Body] = _data;
                        break;
                    case ArmorLowerClass.Boots:
                        EquipDataAry[(int)EEquipParts.Foot] = _data;
                        break;
                    case ArmorLowerClass.Gloves:
                        EquipDataAry[(int)EEquipParts.GloveHand] = _data;
                        break;
                    default:
                        break;
                }
                break;
            case ItemUpperClass.Weapon:
                EquipDataAry[(int)EEquipParts.WeaponHand] = _data;
                break;
            case ItemUpperClass.Misc:

                MiscBaseData mbd = (MiscBaseData)_data.GetItemBaseData;

                switch (mbd.LowerClassName)
                {
                    case MiscLowerClass.Ring:

                        EquipDataAry[(int)EEquipParts.Finger] = _data;
                        break;
                    case MiscLowerClass.Amulet:
                        EquipDataAry[(int)EEquipParts.Neck] = _data;
                        break;
                    default:
                        break;
                }

                break;
            default:
                break;
        }
        
    }
}
