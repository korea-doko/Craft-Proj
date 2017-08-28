using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamage
{

}

[System.Serializable]
public class Damage : IDamage
{
    int m_minDamage;                      //최소 데미지
    int m_maxDamage;                      //최대 데미지

    int m_increasedMinDamage;
    int m_increasedMaxDamage;

    public Damage()
    {
        m_minDamage = 0;
        MaxDamage = 0;

        m_increasedMinDamage = 100;
        m_increasedMaxDamage = 100;
    }
    public Damage(int _min,int _max)
    {
        MaxDamage = _max;
        m_minDamage = _min;
    }

    public int MaxDamage
    {
        get
        {
            return m_maxDamage;
        }

        set
        {
            m_maxDamage = value;
        }
    }

    public int MinDamage
    {
        get
        {
            return m_minDamage;
        }

        set
        {
            m_minDamage = value;
        }
    }

    public int IncreasedMinDamage
    {
        get
        {
            return m_increasedMinDamage;
        }

        set
        {
            m_increasedMinDamage = value;
        }
    }

    public int IncreasedMaxDamage
    {
        get
        {
            return m_increasedMaxDamage;
        }

        set
        {
            m_increasedMaxDamage = value;
        }
    }
}
