using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerModel : IModel
{

}

public class PlayerModel : MonoBehaviour , IPlayerModel{

    [SerializeField] private List<HeroData> m_heroList;
    [SerializeField] private int m_heroOwnedLimit;
    [SerializeField] private int[] m_numOfOwnedRune;

    public List<HeroData> HeroList
    {
        get
        {
            return m_heroList;
        }
    }
    public int HeroOwnedLimit
    {
        get
        {
            return m_heroOwnedLimit;
        }

        set
        {
            m_heroOwnedLimit = value;
        }
    }

    public int[] NumOfOwnedRune
    {
        get
        {
            return m_numOfOwnedRune;
        }
    }

    public void InitModel()
    {
        m_heroOwnedLimit = 3;
        m_heroList = new List<HeroData>();

        int numOfRuneType = System.Enum.GetNames(typeof(RuneType)).Length;
        m_numOfOwnedRune = new int[numOfRuneType];
        for (int i = 0; i < numOfRuneType; i++)
            m_numOfOwnedRune[i] = 100;
    }
    public bool AddHeroData(HeroData _data)
    {
        if (m_heroList.Count >= m_heroOwnedLimit)
            return false;


        m_heroList.Add(_data);
        return true;
    }
    public int GetNumOfOwnedRune(RuneType _type)
    {
        return m_numOfOwnedRune[(int)_type];
    }

}
