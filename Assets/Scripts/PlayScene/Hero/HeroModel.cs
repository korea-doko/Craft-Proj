using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeroModel : IModel
{

}
public class HeroModel : MonoBehaviour, IHeroModel
{
    [SerializeField] private List<BaseHeroData> m_baseHeroDataList;

    [SerializeField] private List<HeroData> m_availableHeroDataList;

    [SerializeField] private int m_giveID;

    public List<HeroData> AvailableHeroDataList
    {
        get
        {
            return m_availableHeroDataList;
        }    
    }

    
    public void InitModel()
    {
        m_baseHeroDataList = new List<BaseHeroData>();
        m_availableHeroDataList = new List<HeroData>();

        m_giveID = 0;
        int numOfHero = System.Enum.GetNames(typeof(EHeroClass)).Length;

        for (int i = 0; i < numOfHero; i++)
        {
            BaseHeroData bhd = new BaseHeroData((EHeroClass)i, new Attribute());
            m_baseHeroDataList.Add(bhd);
        }

        for (int i = 0; i < 3; i++)
            MakeAvailableHeroData();

    }
    
    public void MakeAvailableHeroData()
    {

        BaseHeroData data = GetRandomBaseHeroData();

        HeroData heroData = new HeroData(GetIDGivenToHero(),data);

        m_availableHeroDataList.Add(heroData);
    }

    public HeroData GetHeroData(int _id)
    {
        return m_availableHeroDataList[_id];
    }
    public void RemoveAvailableHeroData(int _id)
    {
        m_availableHeroDataList.RemoveAt(_id);
    }

    BaseHeroData GetRandomBaseHeroData()
    {
        int rand = UnityEngine.Random.Range(0, m_baseHeroDataList.Count);

        BaseHeroData baseHeroData = m_baseHeroDataList[rand];

        return new BaseHeroData(baseHeroData);
    }
    int GetIDGivenToHero()
    {
        return m_giveID++;
    }
}
