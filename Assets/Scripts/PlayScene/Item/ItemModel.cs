using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IItemModel : IModel
{

}

[System.Serializable]
public class ItemModel : MonoBehaviour, IItemModel
{
    [SerializeField] private List<WeaponBaseData> m_weaponBaseList;
    [SerializeField] private List<ModData> m_suffixModList;
    [SerializeField] private List<ModData> m_prefixModList;

    public void InitModel()
    {
        m_weaponBaseList = new List<WeaponBaseData>();
        m_prefixModList = new List<ModData>();
        m_suffixModList = new List<ModData>();
        
        InitWeaponBaseData();
        InitPrefixModData();
        InitSuffixModData();
    }
    
    public WeaponBaseData GetWeaponBaseData()
    {
        int randIndex = UnityEngine.Random.Range(0, m_weaponBaseList.Count);

        return m_weaponBaseList[randIndex];
    }
    public ModData GetPrefixData()
    {
        int randIndex = UnityEngine.Random.Range(0, m_prefixModList.Count);
        return m_prefixModList[randIndex];
    }
    public ModData GetSuffixData()
    {
        int randIndex = UnityEngine.Random.Range(0, m_suffixModList.Count);
        return m_suffixModList[randIndex];
    }

    void InitWeaponBaseData()
    {
        // 나중에 db에서 가져온다.

        int numOfType = System.Enum.GetNames(typeof(ItemLowerClassWeapons)).Length;

        for (int i = 0; i < 50; i++)
        {
            int minDamage = UnityEngine.Random.Range(10, 100);
            int maxDamage = UnityEngine.Random.Range(10, 100);
            float attackSpeed = UnityEngine.Random.Range(1.0f, 1.8f);

            WeaponBaseData baseData = new WeaponBaseData(
             i,
             (ItemLowerClassWeapons)UnityEngine.Random.Range(0, numOfType),
             minDamage,
             maxDamage,
             attackSpeed);

            m_weaponBaseList.Add(baseData);
        }
    }
    void InitPrefixModData()
    {
        // 나중에 디비에서 가져오고 실제 구현은 스크립트마다 구현해놓기로
        for(int i = 0; i < 20;i++)
        {
            int id = i;
            int level = i;
            ModType type = ModType.Prefix;
            int minValue = UnityEngine.Random.Range(5, 20);
            int maxValue = UnityEngine.Random.Range(5, 20);
            ModData data = new ModData(id,level,type,"a",ModStat.AccuracyRating, minValue,maxValue);

            m_prefixModList.Add(data);
        }
    }
    void InitSuffixModData()
    {
        // 나중에 디비에서 가져오고 실제 구현은 스크립트마다 구현해놓기로
        for (int i = 0; i < 20; i++)
        {
            int id = i;
            int level = i;
            ModType type = ModType.Suffix;
            int minValue = UnityEngine.Random.Range(5, 20);
            int maxValue = UnityEngine.Random.Range(5, 20);
            ModData data = new ModData(id, level, type,"a",ModStat.AttackSpeed ,minValue, maxValue);

            m_suffixModList.Add(data);
        }
    }
}
