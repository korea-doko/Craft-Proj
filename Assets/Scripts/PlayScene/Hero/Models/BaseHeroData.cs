using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public interface IBaseHeroData
{
    Attribute GetBaseAttr { get; }
    Attribute GetOffsetAttr { get; }
    EHeroClass GetHeroClass { get; }
}

[System.Serializable]
public class BaseHeroData : IBaseHeroData
{

    [SerializeField] private EHeroClass m_heroClass;

    [SerializeField] private Attribute m_baseMinAttr;
    [SerializeField] private Attribute m_baseMaxAttr;

    [SerializeField] private Attribute m_offsetMinAttr;
    [SerializeField] private Attribute m_offsetMaxAttr;
     
    public BaseHeroData(EHeroClass _class,Attribute _baseMinAttr, Attribute _baseMaxAttr,
        Attribute _offsetMinAttr, Attribute _offsetMaxAttr)
    {
        m_heroClass = _class;

        m_baseMinAttr = _baseMinAttr;
        m_baseMaxAttr = _baseMaxAttr;

        m_offsetMinAttr = _offsetMinAttr;
        m_offsetMaxAttr = _offsetMaxAttr;
    }

    public Attribute GetBaseAttr
    {
        get
        {
            int str = UnityEngine.Random.Range(m_baseMinAttr.Str, m_baseMaxAttr.Str);
            int dex = UnityEngine.Random.Range(m_baseMinAttr.Dex, m_baseMaxAttr.Dex);
            int intel = UnityEngine.Random.Range(m_baseMinAttr.Int, m_baseMaxAttr.Int);

            return new Attribute(str, dex, intel);
        }
    }

    public Attribute GetOffsetAttr
    {
        get
        {
            int str = UnityEngine.Random.Range(m_offsetMinAttr.Str, m_offsetMaxAttr.Str);
            int dex = UnityEngine.Random.Range(m_offsetMinAttr.Dex, m_offsetMaxAttr.Dex);
            int intel = UnityEngine.Random.Range(m_offsetMinAttr.Int, m_offsetMaxAttr.Int);

            return new Attribute(str, dex, intel);
        }
    }

    public EHeroClass GetHeroClass
    {
        get { return m_heroClass; }
    }
}
