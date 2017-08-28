using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroData {

    [SerializeField] private BaseHeroData m_baseHeroData;
    [SerializeField] private ItemData[] m_equipDataAry;

    public HeroData(BaseHeroData _baseData)
    {
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
}
