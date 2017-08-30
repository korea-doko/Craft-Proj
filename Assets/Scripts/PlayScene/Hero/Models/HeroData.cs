using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroData {

    [SerializeField] private int m_givenID;
    [SerializeField] private BaseHeroData m_baseHeroData;
    [SerializeField] private ItemData[] m_equipDataAry;

    public HeroData(int _givenID,BaseHeroData _baseData)
    {
        m_givenID = _givenID;
        m_baseHeroData = _baseData;

        int numOfEquipParts = System.Enum.GetNames(typeof(EEquipParts)).Length;
        m_equipDataAry = new ItemData[numOfEquipParts];
    }
    public BaseHeroData BaseHeroData
    {
        get
        {
            return m_baseHeroData;
        }
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
