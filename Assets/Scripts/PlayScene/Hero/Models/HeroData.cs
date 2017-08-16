using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HeroClass
{
    Berserker,
    Ranger
}
[System.Serializable]
public class HeroStatus
{
    [SerializeField] private int m_str;
    [SerializeField] private int m_dex;
    [SerializeField] private int m_int;

    public HeroStatus()
    {
        m_str = 1;
        m_dex = 1;
        m_int = 1;
    }

    public int Str
    {
        get
        {
            return m_str;
        }

        set
        {
            m_str = value;
        }
    }

    public int Dex
    {
        get
        {
            return m_dex;
        }

        set
        {
            m_dex = value;
        }
    }

    public int Int
    {
        get
        {
            return m_int;
        }

        set
        {
            m_int = value;
        }
    }
}

[System.Serializable]
public class HeroData
{
    [SerializeField] private HeroClass m_class;
    [SerializeField] private HeroStatus m_status;

    public HeroData(HeroClass _class, HeroStatus _status)
    {
        m_class = _class;
        m_status = _status;
    }
    public HeroData(HeroData _data)
    {
        m_class = _data.Class;
        m_status = new HeroStatus();
        m_status.Str = _data.m_status.Str;
        m_status.Dex = _data.m_status.Dex;
        m_status.Int = _data.m_status.Int;
    }
    public HeroClass Class
    {
        get
        {
            return m_class;
        }
    }
    public HeroStatus Status
    {
        get
        {
            return m_status;
        }
    }
}
