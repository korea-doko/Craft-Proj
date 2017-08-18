using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IWeaponItem : IItemData,IDamage
{
    WeaponBaseData GetWeaponBase { get; }

    List<WeaponModData> GetPrefixes { get; }
    List<WeaponModData> GetSuffixes { get; }

    int GetNumOfPrefix { get; }
    int GetNumOfSuffix { get; }

    void AddPrefix(WeaponModData _data);
    void AddSuffix(WeaponModData _data);

    void RemoveRandomPrefix();
    void RemoveRandomSuffix();
}

[System.Serializable]
public class WeaponData : ItemData ,IWeaponItem
{
    [SerializeField] private WeaponBaseData m_baseWeaponData;

    [SerializeField] protected List<WeaponModData> m_prefixList;
    [SerializeField] protected List<WeaponModData> m_suffixList;
   

    public WeaponData(WeaponBaseData _baseData, ItemRarity _rarity) : base(_rarity)
    {
        m_baseWeaponData = _baseData;

        m_prefixList = new List<WeaponModData>();
        m_suffixList = new List<WeaponModData>();
    }

    public WeaponBaseData GetWeaponBase { get { return m_baseWeaponData; } }
    public Vector2 GetPhysicalDamage { get { return m_baseWeaponData.GetDamage.GetPhysicalDamage; } }
    public Vector2 GetColdDamage { get { return m_baseWeaponData.GetDamage.GetColdDamage; } }
    public Vector2 GetFireDamage { get { return m_baseWeaponData.GetDamage.GetFireDamage; } }
    public Vector2 GetLightningDamage { get { return m_baseWeaponData.GetDamage.GetLightningDamage; } }
    public Vector2 GetChaosDamage { get { return m_baseWeaponData.GetDamage.GetChaosDamage; } }

    public float GetAverageDamage
    {
        get
        {
            return m_baseWeaponData.GetDPS;
        }
    }

    public List<WeaponModData> GetPrefixes { get { return m_prefixList; } }    
    public List<WeaponModData> GetSuffixes { get { return m_suffixList; } }

    public int GetNumOfPrefix { get { return m_prefixList.Count; } }
    public int GetNumOfSuffix { get { return m_suffixList.Count; } }

    public void AddPrefix(WeaponModData _data)
    {
        m_prefixList.Add(_data);
        _data.Attach(this);
    }
    public void AddSuffix(WeaponModData _data)
    {
        m_suffixList.Add(_data);
        _data.Attach(this);
    }

    public void RemoveRandomPrefix()
    {
        int ranIndex = UnityEngine.Random.Range(0, m_prefixList.Count);
        WeaponModData data = m_prefixList[ranIndex];
        data.Detach(this);
        m_prefixList.RemoveAt(ranIndex);
    }
    public void RemoveRandomSuffix()
    {
        int ranIndex = UnityEngine.Random.Range(0, m_suffixList.Count);
        WeaponModData data = m_suffixList[ranIndex];
        data.Detach(this);
        m_suffixList.RemoveAt(ranIndex);

    }
}
