using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[System.Serializable]
public class BaseHeroData
{
    [SerializeField] private EHeroClass m_heroClass;
    [SerializeField] private Attribute m_attribute;

    public EHeroClass HeroClass
    {
        get
        {
            return m_heroClass;
        }

        set
        {
            m_heroClass = value;
        }
    }

    public Attribute Attribute
    {
        get
        {
            return m_attribute;
        }

        set
        {
            m_attribute = value;
        }
    }

    public BaseHeroData(EHeroClass _class,Attribute _attribute)
    {
        m_heroClass = _class;
        m_attribute = _attribute;      
    }
    public BaseHeroData(BaseHeroData _data)
    {
        m_heroClass = _data.m_heroClass;
        m_attribute = new Attribute(_data.m_attribute);
    }
}
