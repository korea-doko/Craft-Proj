using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeroModel : IModel
{

}
public class HeroModel : MonoBehaviour, IHeroModel
{
    [SerializeField] private List<HeroData> m_baseHeroDataList;
    [SerializeField] private List<HeroData> m_availableHeroDataList;

    public List<HeroData> AvailableHeroDataList
    {
        get
        {
            return m_availableHeroDataList;
        }
    }

    public void InitModel()
    {
        m_baseHeroDataList = new List<HeroData>();
        m_availableHeroDataList = new List<HeroData>();

        for(int i = 0; i < 10; i++)
        {
            HeroData data = new HeroData((HeroClass)(i % 2), new HeroStatus());
            m_baseHeroDataList.Add(data);
        }
    }

    public void MakeAvailableHero()
    {
        int rand = UnityEngine.Random.Range(0, m_baseHeroDataList.Count);

        HeroData originData = m_baseHeroDataList[rand];


        HeroData copyData = new HeroData(originData);
        m_availableHeroDataList.Add(copyData);
    }
}
