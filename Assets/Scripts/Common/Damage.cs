using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamage
{
    Vector2 GetPhysicalDamage { get; }
    Vector2 GetColdDamage { get; }
    Vector2 GetFireDamage { get; }
    Vector2 GetLightningDamage { get; }
    Vector2 GetChaosDamage { get; }
    float GetAverageDamage { get; }
}

[System.Serializable]
public class Damage : IDamage
{
    [SerializeField] private Vector2 m_physicalDamage;
    [SerializeField] private Vector2 m_coldDamage;
    [SerializeField] private Vector2 m_fireDamage;
    [SerializeField] private Vector2 m_lightningDamage;
    [SerializeField] private Vector2 m_chaosDamage;

    public Damage(Vector2 _physicalDamage, Vector2 _coldDamage,
        Vector2 _fireDamage, Vector2 _lightningDamage, Vector2 _chaosDamage)
    {
        m_physicalDamage = _physicalDamage;
        m_coldDamage = _coldDamage;
        m_fireDamage = _fireDamage;
        m_lightningDamage = _lightningDamage;
        m_chaosDamage = _chaosDamage;
    }

    public Vector2 GetPhysicalDamage
    {
        get
        {
            return m_physicalDamage;
        }
    }

    public Vector2 GetColdDamage
    {
        get
        {
            return m_coldDamage;
        }
    }
   
    public Vector2 GetFireDamage
    {
        get
        {
            return m_fireDamage;
        }
    }

    public Vector2 GetLightningDamage
    {
        get
        {
            return m_lightningDamage;
        }
    }

    public Vector2 GetChaosDamage
    {
        get
        {
            return m_chaosDamage;
        }
    }

    public float GetAverageDamage
    {
        get
        {
            float allDamage=
                m_chaosDamage.x + m_chaosDamage.y +
                m_coldDamage.x + m_coldDamage.y +
            m_fireDamage.x + m_fireDamage.y +
            m_lightningDamage.x + m_lightningDamage.y +
            m_physicalDamage.x + m_physicalDamage.y;

            float average = allDamage * 0.5f;

            return allDamage;
        }
    }
}
