using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IGuildModel : IModel
{

}
public class GuildModel : MonoBehaviour ,IGuildModel{

    [SerializeField] private HeroData m_selectedHeroData;

    public HeroData SelectedHeroData
    {
        get
        {
            return m_selectedHeroData;
        }

        set
        {
            m_selectedHeroData = value;
        }
    }

    public void InitModel()
    {
        m_selectedHeroData = null;
    }
}
