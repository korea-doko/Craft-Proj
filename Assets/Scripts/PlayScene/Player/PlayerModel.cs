using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerModel : IModel
{

}

public class PlayerModel : MonoBehaviour , IPlayerModel{

    [SerializeField] private List<HeroData> m_heroList;

    public List<HeroData> HeroList
    {
        get
        {
            return m_heroList;
        }
    }

    public void InitModel()
    {
        m_heroList = new List<HeroData>();
    }
    public void AddHeroData(HeroData _data)
    {
        m_heroList.Add(_data);
    }
}
