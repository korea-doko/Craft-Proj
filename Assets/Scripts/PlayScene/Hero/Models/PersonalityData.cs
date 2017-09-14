using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class PersonalityData
{
    [SerializeField] private int m_id;
    [SerializeField] private int m_givenID;
    [SerializeField] private string m_name;
    [SerializeField] private ModType m_modType;
    [SerializeField] private int m_modValue;
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

    public PersonalityData(int _id, int _givenID, string _name,
        ModType _type, int _modValue, string _desc)
    {
        m_id = _id;
        m_givenID = _givenID;
        m_name = _name;
        m_modType = _type;
        m_modValue = _modValue;
        m_desc = _desc;
    }
    public PersonalityData(PersonalityData _data)
    {
        m_id = _data.m_id;
        m_givenID = _data.m_givenID;
        m_name = _data.m_name;
        m_modType = _data.m_modType;
        m_modValue = _data.m_modValue;
        m_desc = _data.m_desc;
    }


}
