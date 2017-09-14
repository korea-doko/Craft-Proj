using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Attribute
{
    [SerializeField] private int[] m_stats;
    
    public Attribute()
    {
        m_stats = new int[3];           
    }

    public Attribute(int _str, int _dex, int _int)
    {
        m_stats = new int[3];
        m_stats[0] = _str;
        m_stats[1] = _dex;
        m_stats[2] = _int;
    }
    public Attribute(Attribute _attr)
    {
        m_stats = new int[3];

        for (int i = 0; i < 3; i ++)
            m_stats[i] = _attr.m_stats[i];
    }

    public int Str
    {
        get
        {
            return m_stats[(int)AttributeType.Str];
        }

        set
        {
            m_stats[(int)AttributeType.Str] = value;
        }
    }

    public int Dex
    {
        get
        {
            return m_stats[(int)AttributeType.Dex];
        }

        set
        {
            m_stats[(int)AttributeType.Dex] = value;
        }
    }

    public int Int
    {
        get
        {
            return m_stats[(int)AttributeType.Int];
        }

        set
        {
            m_stats[(int)AttributeType.Int] = value;
        }
    }

    public int[] Stats
    {
        get { return m_stats; }
    }
}
