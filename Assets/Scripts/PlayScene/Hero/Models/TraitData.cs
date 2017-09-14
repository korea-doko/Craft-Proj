using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TraitData
{
    [SerializeField] private int m_id;
    [SerializeField] private string m_name;
    [SerializeField] private int m_modValue;
    [SerializeField] private ModType m_modType;
    [SerializeField] private string m_desc;

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

    public TraitData( int _id, string _name, int _modValue, ModType _modType, string _desc)
    {
        m_id = _id;
        m_name = _name;
        m_modValue = _modValue;
        m_modType = _modType;
        m_desc = _desc;
    }
    public TraitData (TraitData _data)
    {
        m_id = _data.m_id;
        m_name = _data.m_name;
        m_modValue = _data.m_modValue;
        m_modType = _data.m_modType;
        m_desc = _data.m_desc;
    }

}
