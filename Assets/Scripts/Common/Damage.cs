using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Damage
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

    public float GetPhysicalDamage
    {
        get
        {
            float physical = (m_physicalDamage.x + m_physicalDamage.y)*0.5f;
            return physical;
        }
    }
    public float GetFireDamage
    {
        get
        {
            float fireDamage = (m_fireDamage.x + m_fireDamage.y) * 0.5f;
            return fireDamage;
        }
    }
    public float GetColdDamage
    {
        get
        {
            float coldDamage = (m_coldDamage.x + m_coldDamage.y) * 0.5f;
            return coldDamage;
        }
    }
    public float GetLightningDamage
    {
        get
        {
            float lightningDamage = (m_lightningDamage.x + m_lightningDamage.y) * 0.5f;
            return lightningDamage;
        }
    }
    public float GetChaosDamage
    {
        get
        {
            float chaosDamage = (m_chaosDamage.x + m_chaosDamage.y) * 0.5f;
            return chaosDamage;
        }
    }
    public float GetAllDamage
    {
        get { return GetPhysicalDamage + GetFireDamage + GetColdDamage + GetLightningDamage + GetChaosDamage; }    
    }
}
