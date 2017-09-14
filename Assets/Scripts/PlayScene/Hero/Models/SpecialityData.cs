using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecialityData
{
    [SerializeField] private int m_id;
    [SerializeField] private string m_name;
    [SerializeField] private int m_modValue;
    [SerializeField] private ModType m_modType;
    [SerializeField] private string m_desc;
    [SerializeField] private EHeroClass m_ownedClass;

    public string Name
    {
        get
        {
            return m_name;
        }

        set
        {
            m_name = value;
        }
    }

    public int ModValue
    {
        get
        {
            return m_modValue;
        }

        set
        {
            m_modValue = value;
        }
    }

    public ModType ModType
    {
        get
        {
            return m_modType;
        }

        set
        {
            m_modType = value;
        }
    }

    public string Desc
    {
        get
        {
            return m_desc;
        }

        set
        {
            m_desc = value;
        }
    }

    public EHeroClass OwnedClass
    {
        get
        {
            return m_ownedClass;
        }

        set
        {
            m_ownedClass = value;
        }
    }

    public SpecialityData(int _id, string _name,int _modValue,
        ModType _modType,string _desc, EHeroClass _ownedClass)
    {
        m_id = _id;
        m_name = _name;
        m_modValue = _modValue;
        m_modType = _modType;
        m_desc = _desc;
        m_ownedClass = _ownedClass;
    }
    public SpecialityData(SpecialityData _data)
    {
        m_id = _data.m_id;
        m_name = _data.m_name;
        m_modValue = _data.m_modValue;
        m_modType = _data.m_modType;
        m_desc = _data.m_desc;
        m_ownedClass = _data.m_ownedClass;
    }


}
