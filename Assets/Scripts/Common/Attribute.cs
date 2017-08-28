using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Attribute
{
    [SerializeField] private int m_str;
    [SerializeField] private int m_dex;
    [SerializeField] private int m_int;

    public Attribute()
    {
        m_dex = m_str = m_int = 0;
    }

    public Attribute(int _str, int _dex, int _int)
    {
        m_str = _str;
        m_dex = _dex;
        m_int = _int;
    }
    public Attribute(Attribute _attr)
    {
        m_str = _attr.Str;
        m_dex = _attr.Dex;
        m_int = _attr.Int;
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
